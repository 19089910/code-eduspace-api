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
        if (string.IsNullOrWhiteSpace(alunoDto.Nome))
            throw new ArgumentException("O nome do aluno é obrigatório.");

        if (string.IsNullOrWhiteSpace(alunoDto.Email))
            throw new ArgumentException("O email do aluno é obrigatório.");

        if (alunoDto.DataNascimento == default)
            throw new ArgumentException("A data de nascimento é obrigatória.");

        if (CalcularIdade(alunoDto.DataNascimento) < 18)
            throw new ArgumentException("O aluno deve ter pelo menos 18 anos.");

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

    public Aluno AtualizarAluno(int id, AlunoDto alunoDto)
    {
        var alunoExistente = _context.Alunos.FirstOrDefault(a => a.Id == id);
        if (alunoExistente == null)
            throw new Exception("Aluno não encontrado.");

        if (string.IsNullOrWhiteSpace(alunoDto.Nome))
            throw new ArgumentException("O nome do aluno é obrigatório.");

        if (string.IsNullOrWhiteSpace(alunoDto.Email))
            throw new ArgumentException("O email do aluno é obrigatório.");

        if (alunoDto.DataNascimento == default)
            throw new ArgumentException("A data de nascimento é obrigatória.");

        if (CalcularIdade(alunoDto.DataNascimento) < 18)
            throw new ArgumentException("O aluno deve ter pelo menos 18 anos.");

        alunoExistente.Nome = alunoDto.Nome;
        alunoExistente.Email = alunoDto.Email;
        alunoExistente.DataNascimento = alunoDto.DataNascimento;

        _context.SaveChanges();
        return alunoExistente;
    }

    public Aluno ObterAlunoPorId(int id) => _context.Alunos.Find(id);

    public IEnumerable<Aluno> ListarAluno() => _context.Alunos.ToList();

    public bool DeletarAluno(int id)
    {
        var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
        if (aluno == null) return false;

        _context.Alunos.Remove(aluno);
        _context.SaveChanges();
        return true;
    }

    private int CalcularIdade(DateTime dataNascimento)
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - dataNascimento.Year;
        if (dataNascimento > hoje.AddYears(-idade)) idade--;
        return idade;
    }
}
