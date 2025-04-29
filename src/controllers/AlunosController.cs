using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly AlunoService _alunoService;

    public AlunoController(AlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpPost]
    public IActionResult CriarAluno([FromBody] AlunoDto alunoDto)
    {
        var aluno = _alunoService.CriarAluno(alunoDto);
        return CreatedAtAction(nameof(CriarAluno), new { id = aluno.Id }, aluno);
    }

    [HttpGet("{id}")]
    public IActionResult ObterAlunoPorId(int id)
    {
        var aluno = _alunoService.ObterAlunoPorId(id);
        if (aluno == null) return NotFound();
        return Ok(aluno);
    }

    [HttpGet]
    public IActionResult ListarAlunos()
    {
        var alunos = _alunoService.ListarAlunos();
        return Ok(alunos);
    }
}
