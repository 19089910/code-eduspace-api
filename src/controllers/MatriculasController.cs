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
        var matricula = _matriculaService.CriarMatricula(matriculaDto);
        return CreatedAtAction(nameof(CriarMatricula), new { id = matricula.Id }, matricula);
    }

    [HttpGet("{id}")]
    public IActionResult ObterMatriculaPorId(int id)
    {
        var matricula = _matriculaService.ObterMatriculaPorId(id);
        if (matricula == null) return NotFound();
        return Ok(matricula);
    }

    [HttpGet]
    public IActionResult ListarMatriculas()
    {
        var matriculas = _matriculaService.ListarMatriculas();
        return Ok(matriculas);
    }
}
