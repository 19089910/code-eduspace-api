using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public IActionResult CreateEnrollment([FromBody] EnrollmentDto enrollmentDto)
    {
        try
        {
            var enrollment = _enrollmentService.CreateEnrollment(enrollmentDto);
            return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, enrollment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal error while creating enrollment.", details = ex.Message });
        }
    }

    [HttpGet("course/{name}")]
    public IActionResult ListStudentsByCourse(string name)
    {
        try
        {
            var students = _enrollmentService.ListStudentsByCourse(name);
            return Ok(students);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error while listing students of the course.", details = ex.Message });
        }
    }

    [HttpGet("student/{name}")]
    public IActionResult ListCoursesByStudent(string name)
    {
        try
        {
            var courses = _enrollmentService.ListCoursesByStudent(name);
            return Ok(courses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error while listing courses of the student.", details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetEnrollmentById(int id)
    {
        try
        {
            var enrollment = _enrollmentService.GetEnrollmentById(id);
            return Ok(enrollment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Internal error while getting enrollment.", details = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult ListEnrollments()
    {
        try
        {
            var enrollments = _enrollmentService.ListEnrollments();
            return Ok(enrollments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error while listing enrollments.", details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEnrollment(int id)
    {
        try
        {
            _enrollmentService.DeleteEnrollment(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error while deleting enrollment.", details = ex.Message });
        }
    }
}
