using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
public class CursoService
{
    private readonly AppDbContext _context;

    public CursoService(AppDbContext context)
    {
        _context = context;
    }

    public Curso CriarCurso(CursoDto cursoDto)
    {
        var curso = new Curso
        {
            Nome = cursoDto.Nome,
            Descricao = cursoDto.Descricao
        };

        _context.Cursos.Add(curso);
        _context.SaveChanges();
        return curso;
    }

    public Curso ObterCursoPorId(int id)
    {
        return _context.Cursos.Find(id);
    }

    public IEnumerable<Curso> ListarCursos()
    {
        return _context.Cursos.ToList();
    }
}
