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

    public IEnumerable<Aluno> ListarAlunos()
    {
        return _context.Alunos.ToList();
    }
}