using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class MatriculaController : ControllerBase
{
    private readonly MatriculaService _matriculaService;

    public MatriculaController(MatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    [HttpPost]
    public IActionResult CriarMatricula([FromBody] MatriculaDto matriculaDto)
    {
        try
        {
            var matricula = _matriculaService.CriarMatricula(matriculaDto);
            return CreatedAtAction(nameof(ObterMatriculaPorId), new { id = matricula.Id }, matricula);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro interno ao criar matrícula.", detalhe = ex.Message });
        }
    }

    [HttpGet("curso/{nome}")]
    public IActionResult ListarAlunosPorCurso(string nome)
    {
        try
        {
            var alunos = _matriculaService.ListarAlunosPorCurso(nome);
            return Ok(alunos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro ao listar alunos do curso.", detalhe = ex.Message });
        }
    }

    [HttpGet("aluno/{nome}")]
    public IActionResult ListarCursosPorAluno(string nome)
    {
        try
        {
            var cursos = _matriculaService.ListarCursosPorAluno(nome);
            return Ok(cursos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro ao listar cursos do aluno.", detalhe = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult ObterMatriculaPorId(int id)
    {
        try
        {
            var matricula = _matriculaService.ObterMatriculaPorId(id);
            return Ok(matricula);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro interno ao obter matrícula.", detalhe = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult ListarMatricula()
    {
        try
        {
            var matriculas = _matriculaService.ListarMatricula();
            return Ok(matriculas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro ao listar matrículas.", detalhe = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarMatricula(int id)
    {
        try
        {
            _matriculaService.DeletarMatricula(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro ao deletar matrícula.", detalhe = ex.Message });
        }
    }
}