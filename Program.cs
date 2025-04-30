using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace code_eduspace_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure the database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency injection
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<EnrollmentService>();

            builder.Services.AddControllers();

            // Swagger configuration for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();  // <- ESSENTIAL to generate Swagger

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  // Enable Swagger to generate documentation
                app.UseSwaggerUI();  // Enable Swagger UI to test endpoints
            }

            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}
