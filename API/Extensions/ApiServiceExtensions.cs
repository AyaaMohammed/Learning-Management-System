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
            return services;
        }
    }
}
