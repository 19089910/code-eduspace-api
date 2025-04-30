using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
using code_eduspace_api.Exceptions;
public class CursoService
{
    private readonly AppDbContext _context;

    public CursoService(AppDbContext context)
    {
        _context = context;
    }

    public Curso CriarCurso(CursoDto cursoDto)
    {
        if (string.IsNullOrWhiteSpace(cursoDto.Nome))
            throw new ArgumentException("O nome do curso é obrigatório.");

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
        if (curso == null)
            throw new InvalidOperationException("Curso não encontrado.");

        curso.Nome = cursoDto.Nome;
        curso.Descricao = cursoDto.Descricao;

        _context.SaveChanges();
        return curso;
    }

    public Curso ObterCursoPorId(int id)
    {
        var curso = _context.Cursos.Find(id);
        if (curso == null)
            throw new InvalidOperationException("Curso não encontrado.");
        return curso;
    }

    public IEnumerable<Curso> ListarCurso()
    {
        return _context.Cursos.ToList();
    }

    public bool DeletarCurso(int id)
    {
        var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
        if (curso == null)
            throw new InvalidOperationException("Curso não encontrado.");

        _context.Cursos.Remove(curso);
        _context.SaveChanges();
        return true;
    }
}