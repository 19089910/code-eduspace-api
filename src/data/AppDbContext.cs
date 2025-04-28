using Microsoft.EntityFrameworkCore;
using code_eduspace_api.Models;

namespace code_eduspace_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matricula>()
                .HasIndex(m => new { m.AlunoId, m.CursoId })
                .IsUnique(); // Impede matrícula duplicada
        }
    }
}