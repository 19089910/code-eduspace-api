using Microsoft.AspNetCore.Mvc;
using code_eduspace_api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public IActionResult CreateCourse([FromBody] CourseDto courseDto)
    {
        try
        {
            var course = _courseService.CreateCourse(courseDto);
            return CreatedAtAction(nameof(CreateCourse), new { id = course.Id }, course);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal error: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCourse(int id, [FromBody] CourseDto courseDto)
    {
        try
        {
            var updatedCourse = _courseService.UpdateCourse(id, courseDto);
            return Ok(updatedCourse);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal error: {ex.Message}" });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(int id)
    {
        try
        {
            var course = _courseService.GetCourseById(id);
            return Ok(course);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal error: {ex.Message}" });
        }
    }

    [HttpGet]
    public IActionResult ListCourses()
    {
        try
        {
            var courses = _courseService.ListCourses();
            return Ok(courses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal error: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(int id)
    {
        try
        {
            _courseService.DeleteCourse(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal error: {ex.Message}" });
        }
    }
}
