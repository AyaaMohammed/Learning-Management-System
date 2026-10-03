using Application.DTOs.Quizzes;
using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserService;
using Application.Results;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Moq;
using System.Linq.Expressions;

namespace Application.Tests;

public class QuizServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IGenericRepositoryAsync<Quiz>> _quizRepositoryMock;
    private readonly Mock<IGenericRepositoryAsync<Question>> _questionRepositoryMock;
    private readonly Mock<IGenericRepositoryAsync<QuizQuestion>> _quizQuestionRepositoryMock;
    private readonly Mock<IGenericRepositoryAsync<QuizAttempt>> _quizAttemptRepositoryMock;

    private readonly QuizService _quizService;

    private readonly Guid _tenantId;
    private readonly Guid _userId;

    public QuizServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userServiceMock = new Mock<IUserService>();

        _quizRepositoryMock =
            new Mock<IGenericRepositoryAsync<Quiz>>();

        _questionRepositoryMock =
            new Mock<IGenericRepositoryAsync<Question>>();

        _quizQuestionRepositoryMock =
            new Mock<IGenericRepositoryAsync<QuizQuestion>>();

        _quizAttemptRepositoryMock =
            new Mock<IGenericRepositoryAsync<QuizAttempt>>();

        _tenantId = Guid.NewGuid();
        _userId = Guid.NewGuid();

        _userServiceMock
            .SetupGet(x => x.TenantId)
            .Returns(_tenantId);

        _userServiceMock
            .SetupGet(x => x.UserId)
            .Returns(_userId);

        _unitOfWorkMock
            .SetupGet(x => x.Quizzes)
            .Returns(_quizRepositoryMock.Object);

        _unitOfWorkMock
            .SetupGet(x => x.Questions)
            .Returns(_questionRepositoryMock.Object);

        _unitOfWorkMock
            .SetupGet(x => x.QuizQuestions)
            .Returns(_quizQuestionRepositoryMock.Object);

        _unitOfWorkMock
            .SetupGet(x => x.QuizAttempts)
            .Returns(_quizAttemptRepositoryMock.Object);

        _quizService = new QuizService(
            _unitOfWorkMock.Object,
            _userServiceMock.Object);
    }


    // =========================================================
    // 1. Create Quiz Successfully
    // =========================================================

    [Fact]
    public async Task CreateAsync_WithValidQuestions_ReturnsQuizId()
    {
        // Arrange

        var questionId1 = Guid.NewGuid();
        var questionId2 = Guid.NewGuid();

        var request = new QuizRequest
        {
            Title = "C# Basics",
            Description = "Basic C# Quiz",
            MaxAttempts = 2,
            QuestionIds = new List<Guid>
            {
                questionId1,
                questionId2
            }
        };

        var questions = new List<Question>
        {
            new Question
            {
                Id = questionId1,
                TenantId = _tenantId,
                IsDeleted = false,
                IsLocked = false
            },
            new Question
            {
                Id = questionId2,
                TenantId = _tenantId,
                IsDeleted = false,
                IsLocked = false
            }
        };

        _questionRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<Expression<Func<Question, bool>>>()))
            .ReturnsAsync(questions);

        _quizRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Quiz>()))
            .ReturnsAsync((Quiz quiz) => quiz);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        // Act

        var result = await _quizService.CreateAsync(request);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        _quizRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Quiz>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }


    // =========================================================
    // 2. Invalid Questions
    // =========================================================

    [Fact]
    public async Task CreateAsync_WithInvalidQuestions_ReturnsFailure()
    {
        // Arrange

        var questionId1 = Guid.NewGuid();
        var questionId2 = Guid.NewGuid();

        var request = new QuizRequest
        {
            Title = "C# Basics",
            Description = "Basic C# Quiz",
            MaxAttempts = 2,
            QuestionIds = new List<Guid>
            {
                questionId1,
                questionId2
            }
        };

        // Only one question is valid
        var questions = new List<Question>
        {
            new Question
            {
                Id = questionId1,
                TenantId = _tenantId,
                IsDeleted = false,
                IsLocked = false
            }
        };

        _questionRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<Expression<Func<Question, bool>>>()))
            .ReturnsAsync(questions);

        // Act

        var result = await _quizService.CreateAsync(request);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.NotFound,
            result.Error.Type);

        Assert.Equal(
            "One or more questions are invalid.",
            result.Error.Message);

        _quizRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Quiz>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }


    // =========================================================
    // 3. Deleted / Locked Question
    // =========================================================

    [Fact]
    public async Task CreateAsync_WithDeletedOrLockedQuestion_ReturnsFailure()
    {
        // Arrange

        var questionId = Guid.NewGuid();

        var request = new QuizRequest
        {
            Title = "C# Basics",
            Description = "Basic C# Quiz",
            MaxAttempts = 2,
            QuestionIds = new List<Guid>
            {
                questionId
            }
        };

        // Repository should return nothing because
        // the service filters IsDeleted and IsLocked.
        _questionRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<Expression<Func<Question, bool>>>()))
            .ReturnsAsync(new List<Question>());

        // Act

        var result = await _quizService.CreateAsync(request);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.NotFound,
            result.Error.Type);

        Assert.Equal(
            "One or more questions are invalid.",
            result.Error.Message);
    }


    // =========================================================
    // 4. Start Quiz Successfully
    // =========================================================

    [Fact]
    public async Task StartQuizAsync_WithValidQuiz_ReturnsAttempt()
    {
        // Arrange

        var quizId = Guid.NewGuid();
        var questionId = Guid.NewGuid();

        var quiz = new Quiz
        {
            Id = quizId,
            TenantId = _tenantId,
            CreatedByUserId = Guid.NewGuid(),
            Title = "C# Basics",
            Description = "Basic C# Quiz",
            MaxAttempts = 2,
            IsActive = true,

            QuizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    QuizId = quizId,
                    QuestionId = questionId,
                    DisplayOrder = 1
                }
            }
        };

        var questions = new List<Question>
        {
            new Question
            {
                Id = questionId,
                TenantId = _tenantId,
                Text = "What is C#?",
                IsDeleted = false,
                IsLocked = false,
                Choices = new List<QuestionChoice>
                {
                    new QuestionChoice
                    {
                        Id = Guid.NewGuid(),
                        QuestionId = questionId,
                        Text = "Programming Language",
                        IsCorrect = true
                    }
                }
            }
        };

        _quizRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Quiz, bool>>>(),
                It.IsAny<Expression<Func<Quiz, object>>[]>()))
            .ReturnsAsync(quiz);

        _quizAttemptRepositoryMock
            .Setup(x => x.CountAsync(
                It.IsAny<Expression<Func<QuizAttempt, bool>>>()))
            .ReturnsAsync(0);

        _questionRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<Expression<Func<Question, bool>>>(),
                It.IsAny<Expression<Func<Question, object>>[]>()))
            .ReturnsAsync(questions);

        _quizAttemptRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<QuizAttempt>()))
            .ReturnsAsync((QuizAttempt attempt) => attempt);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        // Act

        var result = await _quizService.StartQuizAsync(quizId);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.NotEqual(
            Guid.Empty,
            result.Value.AttemptId);

        Assert.Equal(
            quizId,
            result.Value.QuizId);

        Assert.Equal(
            "C# Basics",
            result.Value.Title);

        Assert.Equal(
            1,
            result.Value.TotalQuestions);

        _quizAttemptRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<QuizAttempt>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }


    // =========================================================
    // 5. Quiz Not Found
    // =========================================================

    [Fact]
    public async Task StartQuizAsync_WhenQuizNotFound_ReturnsNotFound()
    {
        // Arrange

        _quizRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Quiz, bool>>>(),
                It.IsAny<Expression<Func<Quiz, object>>[]>()))
            .ReturnsAsync((Quiz?)null);

        // Act

        var result = await _quizService.StartQuizAsync(
            Guid.NewGuid());

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.NotFound,
            result.Error.Type);

        Assert.Equal(
            "Quiz was not found.",
            result.Error.Message);
    }


    // =========================================================
    // 6. Maximum Attempts Exceeded
    // =========================================================

    [Fact]
    public async Task StartQuizAsync_WhenMaximumAttemptsExceeded_ReturnsFailure()
    {
        // Arrange

        var quizId = Guid.NewGuid();

        var quiz = new Quiz
        {
            Id = quizId,
            TenantId = _tenantId,
            Title = "C# Basics",
            MaxAttempts = 2,
            IsActive = true,
            QuizQuestions = new List<QuizQuestion>()
        };

        _quizRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Quiz, bool>>>(),
                It.IsAny<Expression<Func<Quiz, object>>[]>()))
            .ReturnsAsync(quiz);

        _quizAttemptRepositoryMock
            .Setup(x => x.CountAsync(
                It.IsAny<Expression<Func<QuizAttempt, bool>>>()))
            .ReturnsAsync(2);

        // Act

        var result = await _quizService.StartQuizAsync(quizId);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.Conflict,
            result.Error.Type);

        Assert.Equal(
            "Maximum attempts exceeded.",
            result.Error.Message);

        _quizAttemptRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<QuizAttempt>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }


    // =========================================================
    // 7. Quiz Has No Questions
    // =========================================================

    [Fact]
    public async Task StartQuizAsync_WhenQuizHasNoAvailableQuestions_ReturnsFailure()
    {
        // Arrange

        var quizId = Guid.NewGuid();

        var quiz = new Quiz
        {
            Id = quizId,
            TenantId = _tenantId,
            Title = "Empty Quiz",
            MaxAttempts = 2,
            IsActive = true,
            QuizQuestions = new List<QuizQuestion>()
        };

        _quizRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Quiz, bool>>>(),
                It.IsAny<Expression<Func<Quiz, object>>[]>()))
            .ReturnsAsync(quiz);

        _quizAttemptRepositoryMock
            .Setup(x => x.CountAsync(
                It.IsAny<Expression<Func<QuizAttempt, bool>>>()))
            .ReturnsAsync(0);

        _questionRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<Expression<Func<Question, bool>>>(),
                It.IsAny<Expression<Func<Question, object>>[]>()))
            .ReturnsAsync(new List<Question>());

        // Act

        var result = await _quizService.StartQuizAsync(quizId);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.Validation,
            result.Error.Type);

        Assert.Equal(
            "Quiz must contain at least one question.",
            result.Error.Message);

        _quizAttemptRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<QuizAttempt>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }

}