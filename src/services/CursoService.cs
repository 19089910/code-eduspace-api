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
    public Curso AtualizarCurso(int id, CursoDto cursoDto)
    {
        var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
        if (curso == null) return null;

        curso.Nome = cursoDto.Nome;
        curso.Descricao = cursoDto.Descricao;

        _context.SaveChanges();
        return curso;
    }

    public Curso ObterCursoPorId(int id)
    {
        return _context.Cursos.Find(id);
    }

    public IEnumerable<Curso> ListarCurso()
    {
        return _context.Cursos.ToList();
    }

    public bool DeletarCurso(int id)
    {
        var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
        if (curso == null) return false;

        _context.Cursos.Remove(curso);
        _context.SaveChanges();
        return true;
    }

}
