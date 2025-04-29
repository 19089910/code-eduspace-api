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
        var curso = _cursoService.CriarCurso(cursoDto);
        return CreatedAtAction(nameof(CriarCurso), new { id = curso.Id }, curso);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCurso(int id, [FromBody] CursoDto cursoDto)
    {
        var cursoAtualizado = _cursoService.AtualizarCurso(id, cursoDto);
        if (cursoAtualizado == null) return NotFound();

        return Ok(cursoAtualizado);
    }

    [HttpGet("{id}")]
    public IActionResult ObterCursoPorId(int id)
    {
        var curso = _cursoService.ObterCursoPorId(id);
        if (curso == null) return NotFound();
        return Ok(curso);
    }

    [HttpGet]
    public IActionResult ListarCurso()
    {
        var cursos = _cursoService.ListarCurso();
        return Ok(cursos);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarCurso(int id)
    {
        var sucesso = _cursoService.DeletarCurso(id);
        if (!sucesso) return NotFound();

        return NoContent();
    }

}
