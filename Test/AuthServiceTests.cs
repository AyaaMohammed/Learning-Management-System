
using Application.DTOs.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Results;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Moq;
using System.Numerics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Tests;

public class AuthServiceTests
{
    private readonly Mock<IGenericRepositoryAsync<User>> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IGenericRepositoryAsync<User>>();
        _tokenServiceMock = new Mock<ITokenService>();

        _authService = new AuthService(
            _userRepositoryMock.Object,
            _tokenServiceMock.Object);
    }

    // =========================================================
    // 1. Valid Credentials
    // =========================================================

    [Fact]
    public async Task AuthenticateAsync_WithValidCredentials_ReturnsToken()
    {
        // Arrange

        var password = "Aya@123#";
        var username = "admin";
        var salt = "1234";

        var passwordHash = SecurityHash.ComputeHash(
            password,
            username,
            SecurityConstants.HashAlgorithm,
            System.Text.Encoding.UTF8.GetBytes(salt));

        var user = new User
        {
            Id = Guid.NewGuid(),
            TenantId = Guid.NewGuid(),
            Name = username,
            IsActive = true,
            PasswordHash = passwordHash,
            PasswordSalt = salt
        };

        var expectedToken = new TokenResponse
        {
            AccessToken = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };

        _userRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        _tokenServiceMock
            .Setup(x => x.CreateTokenAsync(It.IsAny<UserInfoDTO>()))
            .ReturnsAsync(Result<TokenResponse>.Success(expectedToken));

        // Act

        var result = await _authService.AuthenticateAsync(
            username,
            password);

        // Assert

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("test-token", result.Value.AccessToken);
    }


    // =========================================================
    // 2. User Not Found
    // =========================================================

    [Fact]
    public async Task AuthenticateAsync_WithUnknownUser_ReturnsUnauthorized()
    {
        // Arrange

        _userRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null);

        // Act

        var result = await _authService.AuthenticateAsync(
            "unknown",
            "Password123");

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal(ErrorType.Unauthorized, result.Error.Type);
        Assert.Equal(
            "Invalid username or password.",
            result.Error.Message);
    }


    // =========================================================
    // 3. Inactive User
    // =========================================================

    [Fact]
    public async Task AuthenticateAsync_WithInactiveUser_ReturnsUnauthorized()
    {
        // Arrange

        var user = new User
        {
            Id = Guid.NewGuid(),
            TenantId = Guid.NewGuid(),
            Name = "admin",
            IsActive = false
        };

        _userRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        // Act

        var result = await _authService.AuthenticateAsync(
            "admin",
            "Password123");

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal(ErrorType.Unauthorized, result.Error.Type);
        Assert.Equal(
            "User account is inactive.",
            result.Error.Message);
    }


    // =========================================================
    // 4. Wrong Password
    // =========================================================

    [Fact]
    public async Task AuthenticateAsync_WithWrongPassword_ReturnsUnauthorized()
    {
        // Arrange

        var username = "admin";
        var correctPassword = "Aya@123#";
        var wrongPassword = "WrongPassword";
        var salt = "1234";

        var passwordHash = SecurityHash.ComputeHash(
            correctPassword,
            username,
            SecurityConstants.HashAlgorithm,
            System.Text.Encoding.UTF8.GetBytes(salt));

        var user = new User
        {
            Id = Guid.NewGuid(),
            TenantId = Guid.NewGuid(),
            Name = username,
            IsActive = true,
            PasswordHash = passwordHash,
            PasswordSalt = salt
        };

        _userRepositoryMock
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        // Act

        var result = await _authService.AuthenticateAsync(
            username,
            wrongPassword);

        // Assert

        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal(ErrorType.Unauthorized, result.Error.Type);
        Assert.Equal(
            "Invalid username or password.",
            result.Error.Message);
    }
}
