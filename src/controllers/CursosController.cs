using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase
{
    private readonly CursoService _cursoService;

    public CursoController(CursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpPost]
    public IActionResult CriarCurso([FromBody] CursoDto cursoDto)
    {
        try
        {
            var curso = _cursoService.CriarCurso(cursoDto);
            return CreatedAtAction(nameof(CriarCurso), new { id = curso.Id }, curso);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCurso(int id, [FromBody] CursoDto cursoDto)
    {
        try
        {
            var cursoAtualizado = _cursoService.AtualizarCurso(id, cursoDto);
            return Ok(cursoAtualizado);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpGet("{id}")]
    public IActionResult ObterCursoPorId(int id)
    {
        try
        {
            var curso = _cursoService.ObterCursoPorId(id);
            return Ok(curso);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpGet]
    public IActionResult ListarCurso()
    {
        try
        {
            var cursos = _cursoService.ListarCurso();
            return Ok(cursos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarCurso(int id)
    {
        try
        {
            _cursoService.DeletarCurso(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = $"Erro interno: {ex.Message}" });
        }
    }
}
