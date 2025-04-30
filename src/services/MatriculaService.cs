using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
using System;
using System.Collections.Generic;
using System.Linq;

public class MatriculaService
{
    private readonly AppDbContext _context;

    public MatriculaService(AppDbContext context)
    {
        _context = context;
    }

    public Matricula CriarMatricula(MatriculaDto matriculaDto)
    {
        if (!_context.Alunos.Any(a => a.Id == matriculaDto.AlunoId))
        {
            throw new ArgumentException("Aluno não encontrado.");
        }

        if (!_context.Cursos.Any(c => c.Id == matriculaDto.CursoId))
        {
            throw new ArgumentException("Curso não encontrado.");
        }

        var existeMatricula = _context.Matriculas
            .Any(m => m.AlunoId == matriculaDto.AlunoId && m.CursoId == matriculaDto.CursoId);

        if (existeMatricula)
        {
            throw new InvalidOperationException("O aluno já está matriculado neste curso.");
        }

        try
        {
            var matricula = new Matricula
            {
                AlunoId = matriculaDto.AlunoId,
                CursoId = matriculaDto.CursoId,
                DataMatricula = DateTime.Now
            };

            _context.Matriculas.Add(matricula);
            _context.SaveChanges();

            _context.Entry(matricula).Reference(m => m.Aluno).Load();
            _context.Entry(matricula).Reference(m => m.Curso).Load();

            return matricula;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao salvar a matrícula no banco de dados.", ex);
        }
    }

    public IEnumerable<object> ListarAlunosPorCurso(string courseName)
    {
        var alunos = _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .Where(m => m.Curso.Nome == courseName)
            .Select(m => new {
                id = m.Aluno.Id,
                name = m.Aluno.Nome,
                email = m.Aluno.Email,
                enrollmentDate = m.DataMatricula.ToString("yyyy-MM-dd")
            })
            .ToList();

        return alunos;
    }
    public IEnumerable<object> ListarCursosPorAluno(string alunoNome)
    {
        var cursos = _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .Where(m => m.Aluno.Nome == alunoNome)
            .Select(m => new {
                id = m.Curso.Id,
                name = m.Curso.Nome,
                description = m.Curso.Descricao,
                enrollmentDate = m.DataMatricula.ToString("yyyy-MM-dd")
            })
            .ToList();

        return cursos;
    }


    public Matricula ObterMatriculaPorId(int id)
    {
        var matricula = _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefault(m => m.Id == id);

        if (matricula == null)
        {
            throw new KeyNotFoundException("Matrícula não encontrada.");
        }

        return matricula;
    }

    public IEnumerable<Matricula> ListarMatricula()
    {
        return _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .ToList();
    }
    public void DeletarMatricula(int id)
    {
        var matricula = _context.Matriculas.FirstOrDefault(m => m.Id == id);
        if (matricula == null)
        {
            throw new KeyNotFoundException("Matrícula não encontrada.");
        }

        try
        {
            _context.Matriculas.Remove(matricula);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao deletar a matrícula no banco de dados.", ex);
        }
    }
}
