using Application.Interfaces.Service;
using Application.Services;
using Application.Validators;
using FluentValidation;

namespace API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // FluentValidation
            services.AddValidatorsFromAssemblyContaining<TestValidator>();

            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
