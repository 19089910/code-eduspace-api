using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;
using code_eduspace_api.Exceptions;


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
        try
        {
            var aluno = _alunoService.CriarAluno(alunoDto);
            return CreatedAtAction(nameof(CriarAluno), new { id = aluno.Id }, aluno);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro ao criar aluno.", detalhe = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarAluno(int id, [FromBody] AlunoDto alunoDto)
    {
        try
        {
            var aluno = _alunoService.AtualizarAluno(id, alunoDto);
            return Ok(aluno);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("não encontrado"))
                return NotFound(new { erro = ex.Message });
            return StatusCode(500, new { erro = "Erro ao atualizar aluno.", detalhe = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult ObterAlunoPorId(int id)
    {
        var aluno = _alunoService.ObterAlunoPorId(id);
        if (aluno == null) return NotFound(new { erro = "Aluno não encontrado." });
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
        if (!sucesso) return NotFound(new { erro = "Aluno não encontrado." });

        return NoContent();
    }
}
