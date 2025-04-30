using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] StudentDto studentDto)
    {
        try
        {
            var student = _studentService.CreateStudent(studentDto);
            return CreatedAtAction(nameof(CreateStudent), new { id = student.Id }, student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error while creating student.", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] StudentDto studentDto)
    {
        try
        {
            var student = _studentService.UpdateStudent(id, studentDto);
            return Ok(student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("not found"))
                return NotFound(new { error = ex.Message });
            return StatusCode(500, new { error = "Error while updating student.", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _studentService.GetStudentById(id);
        if (student == null) return NotFound(new { error = "Student not found." });
        return Ok(student);
    }

    [HttpGet]
    public IActionResult ListStudents()
    {
        var students = _studentService.ListStudents();
        return Ok(students);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var success = _studentService.DeleteStudent(id);
        if (!success) return NotFound(new { error = "Student not found." });

        return NoContent();
    }
}
