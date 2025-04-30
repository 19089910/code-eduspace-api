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

            // Configurar o banco de dados
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Injeção de dependências
            builder.Services.AddScoped<CursoService>();
            builder.Services.AddScoped<AlunoService>();
            builder.Services.AddScoped<MatriculaService>();

            builder.Services.AddControllers();

            // Configuração do Swagger para documentação da API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();  // <- ESSENCIAL para gerar o Swagger

            // Habilita CORS
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
                app.UseSwagger();  // Habilita o Swagger para gerar a documentação
                app.UseSwaggerUI();  // Habilita a interface do Swagger para testar os endpoints
            }

            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}
