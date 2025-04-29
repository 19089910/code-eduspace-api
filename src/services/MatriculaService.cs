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
        var existeMatricula = _context.Matriculas
        .Any(m => m.AlunoId == matriculaDto.AlunoId && m.CursoId == matriculaDto.CursoId);

        if (existeMatricula)
        {
            throw new Exception("O aluno já está matriculado neste curso.");
        }

        var matricula = new Matricula
        {
            AlunoId = matriculaDto.AlunoId,
            CursoId = matriculaDto.CursoId,
        };

        _context.Matriculas.Add(matricula);
        _context.SaveChanges();

        _context.Entry(matricula).Reference(m => m.Aluno).Load();
        _context.Entry(matricula).Reference(m => m.Curso).Load();

        return matricula;
    }

    public Matricula ObterMatriculaPorId(int id)
    {
        return _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Matricula> ListarMatricula()
    {
        return _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .ToList();
    }
    public bool DeletarMatricula(int id)
    {
        var matricula = _context.Matriculas.FirstOrDefault(m => m.Id == id);
        if (matricula == null) return false;

        _context.Matriculas.Remove(matricula);
        _context.SaveChanges();
        return true;
    }
}
