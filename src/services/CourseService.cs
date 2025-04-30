using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
using code_eduspace_api.Exceptions;

public class CourseService
{
    private readonly AppDbContext _context;

    public CourseService(AppDbContext context)
    {
        _context = context;
    }

    public Course CreateCourse(CourseDto courseDto)
    {
        if (string.IsNullOrWhiteSpace(courseDto.Name))
            throw new ArgumentException("Course name is required.");

        var course = new Course
        {
            Name = courseDto.Name,
            Description = courseDto.Description
        };

        _context.Courses.Add(course);
        _context.SaveChanges();
        return course;
    }

    public Course UpdateCourse(int id, CourseDto courseDto)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
            throw new InvalidOperationException("Course not found.");

        course.Name = courseDto.Name;
        course.Description = courseDto.Description;

        _context.SaveChanges();
        return course;
    }

    public Course GetCourseById(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
            throw new InvalidOperationException("Course not found.");
        return course;
    }

    public IEnumerable<Course> ListCourses()
    {
        return _context.Courses.ToList();
    }

    public bool DeleteCourse(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
            throw new InvalidOperationException("Course not found.");

        _context.Courses.Remove(course);
        _context.SaveChanges();
        return true;
    }
}
