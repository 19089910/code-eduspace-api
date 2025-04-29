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

    [HttpGet("{id}")]
    public IActionResult ObterCursoPorId(int id)
    {
        var curso = _cursoService.ObterCursoPorId(id);
        if (curso == null) return NotFound();
        return Ok(curso);
    }

    [HttpGet]
    public IActionResult ListarCursos()
    {
        var cursos = _cursoService.ListarCursos();
        return Ok(cursos);
    }
}
