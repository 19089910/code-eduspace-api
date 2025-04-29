using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
public class MatriculaService
{
    private readonly AppDbContext _context;

    public MatriculaService(AppDbContext context)
    {
        _context = context;
    }

    public Matricula CriarMatricula(MatriculaDto matriculaDto)
    {
        var matricula = new Matricula
        {
            AlunoId = matriculaDto.AlunoId,
            CursoId = matriculaDto.CursoId,
            DataMatricula = DateTime.Now
        };

        _context.Matriculas.Add(matricula);
        _context.SaveChanges();
        return matricula;
    }

    public Matricula ObterMatriculaPorId(int id)
    {
        return _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Matricula> ListarMatriculas()
    {
        return _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .ToList();
    }
}
