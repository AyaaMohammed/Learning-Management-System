using Application.DTOs.Questions;
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
            services.AddValidatorsFromAssemblyContaining<QuestionRequest>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IQuestionService, QuestionService>();
            return services;
        }
    }
}
