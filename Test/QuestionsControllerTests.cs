using Application.DTOs.Questions;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserService;
using Application.Results;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Application.Tests;

public class QuestionServiceTests
{
    private readonly Mock<IGenericRepositoryAsync<Question>> _questionRepositoryMock;
    private readonly Mock<IGenericRepositoryAsync<QuestionChoice>> _choiceRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;

    private readonly QuestionService _questionService;

    public QuestionServiceTests()
    {
        _questionRepositoryMock =
            new Mock<IGenericRepositoryAsync<Question>>();

        _choiceRepositoryMock =
            new Mock<IGenericRepositoryAsync<QuestionChoice>>();

        _userServiceMock =
            new Mock<IUserService>();

        _userServiceMock
            .Setup(x => x.UserId)
            .Returns(Guid.NewGuid());

        _userServiceMock
            .Setup(x => x.TenantId)
            .Returns(Guid.NewGuid());

        _questionService = new QuestionService(
            _questionRepositoryMock.Object,
            _choiceRepositoryMock.Object,
            _userServiceMock.Object);
    }


    // =========================================================
    // 1. Create Question
    // =========================================================

    [Fact]
    public async Task CreateAsync_WithValidRequest_ReturnsQuestionId()
    {
        // Arrange

        var request = new QuestionRequest
        {
            Text = "What is C#?",
            ImageUrl = null,
            Choices = new List<QuestionChoiceRequest>
            {
                new()
                {
                    Text = "Programming Language",
                    IsCorrect = true
                },
                new()
                {
                    Text = "Database",
                    IsCorrect = false
                }
            }
        };

        Question? createdQuestion = null;

        _questionRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Question>()))
            .Callback<Question>(question =>
            {
                createdQuestion = question;
            })
            .ReturnsAsync((Question question) => question);

        // Act

        var result = await _questionService.CreateAsync(request);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);

        Assert.NotNull(createdQuestion);

        Assert.Equal(
            request.Text,
            createdQuestion!.Text);

        Assert.Equal(
            _userServiceMock.Object.TenantId,
            createdQuestion.TenantId);

        Assert.Equal(
            _userServiceMock.Object.UserId,
            createdQuestion.CreatedByUserId);

        Assert.False(createdQuestion.IsDeleted);
        Assert.False(createdQuestion.IsLocked);

        Assert.Null(createdQuestion.PreviousQuestionId);

        Assert.Equal(
            2,
            createdQuestion.Choices.Count);

        _questionRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Question>()),
            Times.Once);

        _questionRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }


    // =========================================================
    // 2. Get All Questions
    // =========================================================

    [Fact]
    public async Task GetAllAsync_ReturnsQuestionsForCurrentTenant()
    {
        // Arrange

        var tenantId = _userServiceMock.Object.TenantId;

        var questions = new List<Question>
        {
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Text = "Question 1",
                IsDeleted = false,
                IsLocked = false,
                Choices = new List<QuestionChoice>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Text = "Choice 1",
                        IsCorrect = true
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Text = "Choice 2",
                        IsCorrect = false
                    }
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Text = "Question 2",
                IsDeleted = false,
                IsLocked = false,
                Choices = new List<QuestionChoice>()
            }
        };

        _questionRepositoryMock
            .Setup(x => x.GetAllAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>(),
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, object>>[]>()))
            .ReturnsAsync(questions);

        // Act

        var result = await _questionService.GetAllAsync();

        // Assert

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(
            2,
            result.Value.Count);

        Assert.Equal(
            "Question 1",
            result.Value[0].Text);

        _questionRepositoryMock.Verify(
            x => x.GetAllAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>(),
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, object>>[]>()),
            Times.Once);
    }


    // =========================================================
    // 3. Get By Id - Question Exists
    // =========================================================

    [Fact]
    public async Task GetByIdAsync_WithExistingQuestion_ReturnsQuestion()
    {
        // Arrange

        var questionId = Guid.NewGuid();
        var tenantId = _userServiceMock.Object.TenantId;

        var question = new Question
        {
            Id = questionId,
            TenantId = tenantId,
            Text = "What is .NET?",
            IsDeleted = false,
            IsLocked = false,
            Choices = new List<QuestionChoice>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = ".NET Framework",
                    IsCorrect = true
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "SQL Server",
                    IsCorrect = false
                }
            }
        };

        _questionRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>(),
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, object>>[]>()))
            .ReturnsAsync(question);

        // Act

        var result = await _questionService.GetByIdAsync(questionId);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(
            questionId,
            result.Value.Id);

        Assert.Equal(
            "What is .NET?",
            result.Value.Text);

        Assert.Equal(
            2,
            result.Value.Choices.Count);
    }


    // =========================================================
    // 4. Get By Id - Question Not Found
    // =========================================================

    [Fact]
    public async Task GetByIdAsync_WithNonExistingQuestion_ReturnsNotFound()
    {
        // Arrange

        var questionId = Guid.NewGuid();

        _questionRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>(),
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, object>>[]>()))
            .ReturnsAsync((Question?)null);

        // Act

        var result = await _questionService.GetByIdAsync(questionId);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.NotFound,
            result.Error.Type);

        Assert.Equal(
            "Question not found.",
            result.Error.Message);
    }


    // =========================================================
    // 5. Update Question - Versioning
    // =========================================================

    [Fact]
    public async Task UpdateAsync_WithExistingQuestion_LocksOldAndCreatesNewVersion()
    {
        // Arrange

        var oldQuestionId = Guid.NewGuid();

        var tenantId = _userServiceMock.Object.TenantId;

        var oldQuestion = new Question
        {
            Id = oldQuestionId,
            TenantId = tenantId,
            Text = "Old Question",
            IsDeleted = false,
            IsLocked = false,
            Choices = new List<QuestionChoice>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Text = "Old Choice",
                    IsCorrect = true
                }
            }
        };

        var request = new QuestionRequest
        {
            Text = "Updated Question",
            ImageUrl = null,
            Choices = new List<QuestionChoiceRequest>
            {
                new()
                {
                    Text = "New Choice",
                    IsCorrect = true
                },
                new()
                {
                    Text = "Wrong Choice",
                    IsCorrect = false
                }
            }
        };

        Question? newQuestion = null;

        _questionRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>()))
            .ReturnsAsync(oldQuestion);

        _questionRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Question>()))
            .Callback<Question>(question =>
            {
                newQuestion = question;
            })
            .ReturnsAsync((Question question) => question);

        // Act

        var result = await _questionService.UpdateAsync(
            oldQuestionId,
            request);

        // Assert

        Assert.True(result.IsSuccess);

        Assert.NotEqual(
            Guid.Empty,
            result.Value);

        // Old question should be locked
        Assert.True(oldQuestion.IsLocked);

        // New question should exist
        Assert.NotNull(newQuestion);

        // New question should have a different Id
        Assert.NotEqual(
            oldQuestionId,
            newQuestion!.Id);

        // New question should point to old version
        Assert.Equal(
            oldQuestionId,
            newQuestion.PreviousQuestionId);

        // New question should be unlocked
        Assert.False(newQuestion.IsLocked);

        // New question should not be deleted
        Assert.False(newQuestion.IsDeleted);

        // New question should contain updated data
        Assert.Equal(
            "Updated Question",
            newQuestion.Text);

        Assert.Equal(
            2,
            newQuestion.Choices.Count);

        // Returned Id should be the new question Id
        Assert.Equal(
            newQuestion.Id,
            result.Value);

        _questionRepositoryMock.Verify(
            x => x.UpdateAsync(oldQuestion),
            Times.Once);

        _questionRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Question>()),
            Times.Once);

        _questionRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }


    // =========================================================
    // 6. Update Question - Not Found
    // =========================================================

    [Fact]
    public async Task UpdateAsync_WithNonExistingQuestion_ReturnsNotFound()
    {
        // Arrange

        var questionId = Guid.NewGuid();

        var request = new QuestionRequest
        {
            Text = "Updated Question",
            Choices = new List<QuestionChoiceRequest>
            {
                new()
                {
                    Text = "Correct",
                    IsCorrect = true
                },
                new()
                {
                    Text = "Wrong",
                    IsCorrect = false
                }
            }
        };

        _questionRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>()))
            .ReturnsAsync((Question?)null);

        // Act

        var result = await _questionService.UpdateAsync(
            questionId,
            request);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.NotFound,
            result.Error.Type);

        Assert.Equal(
            "Question not found.",
            result.Error.Message);

        _questionRepositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Question>()),
            Times.Never);

        _questionRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Question>()),
            Times.Never);

        _questionRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }


    // =========================================================
    // 7. Delete Question - Soft Delete
    // =========================================================

    [Fact]
    public async Task DeleteAsync_WithExistingQuestion_SoftDeletesQuestion()
    {
        // Arrange

        var questionId = Guid.NewGuid();

        var question = new Question
        {
            Id = questionId,
            TenantId = _userServiceMock.Object.TenantId,
            Text = "Question",
            IsDeleted = false,
            IsLocked = false
        };

        _questionRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>()))
            .ReturnsAsync(question);

        // Act

        var result = await _questionService.DeleteAsync(questionId);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.True(result.Value);

        // Important: Soft delete
        Assert.True(question.IsDeleted);

        // Should not be physically deleted
        _questionRepositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<Question>()),
            Times.Never);

        _questionRepositoryMock.Verify(
            x => x.UpdateAsync(question),
            Times.Once);

        _questionRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }


    // =========================================================
    // 8. Delete Locked Question
    // =========================================================

    [Fact]
    public async Task DeleteAsync_WithLockedQuestion_ReturnsNotFound()
    {
        // Arrange

        var questionId = Guid.NewGuid();

        _questionRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<Question, bool>>>()))
            .ReturnsAsync((Question?)null);

        // Act

        var result = await _questionService.DeleteAsync(questionId);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(
            ErrorType.NotFound,
            result.Error.Type);

        Assert.Equal(
            "Question not found.",
            result.Error.Message);

        _questionRepositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Question>()),
            Times.Never);

        _questionRepositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<Question>()),
            Times.Never);

        _questionRepositoryMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }
}
