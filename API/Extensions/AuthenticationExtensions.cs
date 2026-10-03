using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];
            var secretKey = configuration["JwtSettings:SecretKey"];

            if (string.IsNullOrWhiteSpace(issuer))
            {
                throw new InvalidOperationException(
                    "JWT Issuer is not configured.");
            }

            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException(
                    "JWT Audience is not configured.");
            }

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException(
                    "JWT SecretKey is not configured.");
            }

            if (Encoding.UTF8.GetBytes(secretKey).Length < 16)
            {
                throw new InvalidOperationException(
                    "JWT SecretKey must be at least 128 bits.");
            }

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = issuer,

                            ValidateAudience = true,
                            ValidAudience = audience,

                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(secretKey)),

                            ClockSkew = TimeSpan.Zero
                        };
                });

            services.AddAuthorization();

            return services;
        }
    }
}