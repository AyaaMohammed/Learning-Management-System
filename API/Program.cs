using API.Middleware;
using Infrastructure.Extensions;
using Serilog;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Host.UseSerilog((context, configuration) =>
                        configuration
                            .ReadFrom.Configuration(context.Configuration)
                            .Enrich.FromLogContext());

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<CorrelationIdEnricherMiddleware>();

            app.UseExceptionHandler();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
