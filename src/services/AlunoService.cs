using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
public class AlunoService
{
    private readonly AppDbContext _context;

    public AlunoService(AppDbContext context)
    {
        _context = context;
    }

    public Aluno CriarAluno(AlunoDto alunoDto)
    {
        var aluno = new Aluno
        {
            Nome = alunoDto.Nome,
            Email = alunoDto.Email,
            DataNascimento = alunoDto.DataNascimento
        };

        _context.Alunos.Add(aluno);
        _context.SaveChanges();
        return aluno;
    }

    public Aluno ObterAlunoPorId(int id)
    {
        return _context.Alunos.Find(id);
    }

    public IEnumerable<Aluno> ListarAluno()
    {
        return _context.Alunos.ToList();
    }

    public Aluno AtualizarAluno(int id, AlunoDto alunoDto)
    {
        var alunoExistente = _context.Alunos.FirstOrDefault(a => a.Id == id);
        if (alunoExistente == null) return null;

        alunoExistente.Nome = alunoDto.Nome;
        alunoExistente.Email = alunoDto.Email;
        alunoExistente.DataNascimento = alunoDto.DataNascimento;

        _context.SaveChanges();
        return alunoExistente;
    }
    public bool DeletarAluno(int id)
    {
        var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
        if (aluno == null) return false;

        _context.Alunos.Remove(aluno);
        _context.SaveChanges();
        return true;
    }

}