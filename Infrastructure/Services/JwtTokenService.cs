using Application.DTOs.Auth;
using Application.Interfaces.Service;
using Application.Results;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public Task<Result<TokenResponse>> CreateTokenAsync(UserInfoDTO userInfo)
        {
            if (string.IsNullOrWhiteSpace(_jwtSettings.SecretKey))
            {
                return Task.FromResult(
                    Result<TokenResponse>.Failure(
                        new Error(
                            "JWT secret key is not configured.",
                            ErrorType.Failure)));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(
                _jwtSettings.ExpirationInMinutes);

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    userInfo.UserId.ToString()),

                new Claim(
                    "userId",
                    userInfo.UserId.ToString()),

                new Claim(
                    "tenantId",
                    userInfo.TenantId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    userInfo.UserName),

                new Claim(
                    ClaimTypes.Role,
                    userInfo.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new TokenResponse
            {
                AccessToken = accessToken,
                ExpiresAt = expiresAt
            };

            return Task.FromResult(Result<TokenResponse>.Success(response));
        }
    }
}

