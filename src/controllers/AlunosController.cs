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

    [HttpPut("{id}")]
    public IActionResult AtualizarAluno(int id, [FromBody] AlunoDto alunoDto)
    {
        var alunoAtualizado = _alunoService.AtualizarAluno(id, alunoDto);
        if (alunoAtualizado == null) return NotFound();

        return Ok(alunoAtualizado);
    }

    [HttpGet("{id}")]
    public IActionResult ObterAlunoPorId(int id)
    {
        var aluno = _alunoService.ObterAlunoPorId(id);
        if (aluno == null) return NotFound();
        return Ok(aluno);
    }

    [HttpGet]
    public IActionResult ListarAluno()
    {
        var alunos = _alunoService.ListarAluno();
        return Ok(alunos);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarAluno(int id)
    {
        var sucesso = _alunoService.DeletarAluno(id);
        if (!sucesso) return NotFound();

        return NoContent();
    }
}
