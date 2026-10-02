using Application.DTOs.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Results;
using Domain.Common;
using Domain.Entities;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepositoryAsync<User> _userRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IGenericRepositoryAsync<User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<TokenResponse>> AuthenticateAsync(string userName,string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Name == userName);
            if (user is null)
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "Invalid username or password.",
                        ErrorType.Unauthorized));
            }

            if (!user.IsActive)
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "User account is inactive.",
                        ErrorType.Unauthorized));
            }

            var isValidPassword = SecurityHash.VerifyHash(password,user.Name,user.PasswordSalt,SecurityConstants.HashAlgorithm,user.PasswordHash);

            if (!isValidPassword)
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "Invalid username or password.",
                        ErrorType.Unauthorized));
            }

            var userInfo = new UserInfoDTO
            {
                UserId = user.Id,
                TenantId = user.TenantId,
                UserName = user.Name,
                Role = user.Role.ToString(),
                IsActive = user.IsActive
            };
            return await CreateTokenAsync(userInfo);
        }

        public async Task<Result<TokenResponse>> CreateTokenAsync(UserInfoDTO userInfo)
        {
            if (userInfo.UserId == Guid.Empty)
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "Invalid user information.",
                        ErrorType.Validation));
            }

            if (userInfo.TenantId == Guid.Empty)
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "Invalid tenant information.",
                        ErrorType.Validation));
            }

            if (string.IsNullOrWhiteSpace(userInfo.UserName))
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "Username is required.",
                        ErrorType.Validation));
            }

            if (string.IsNullOrWhiteSpace(userInfo.Role))
            {
                return Result<TokenResponse>.Failure(
                    new Error(
                        "User role is required.",
                        ErrorType.Validation));
            }

            return await _tokenService.CreateTokenAsync(userInfo);
        }
    }
}
