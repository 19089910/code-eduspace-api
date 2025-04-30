using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnrollmentService
{
    private readonly AppDbContext _context;

    public EnrollmentService(AppDbContext context)
    {
        _context = context;
    }

    public Enrollment CreateEnrollment(EnrollmentDto enrollmentDto)
    {
        if (!_context.Students.Any(s => s.Id == enrollmentDto.StudentId))
        {
            throw new ArgumentException("Student not found.");
        }

        if (!_context.Courses.Any(c => c.Id == enrollmentDto.CourseId))
        {
            throw new ArgumentException("Course not found.");
        }

        var existingEnrollment = _context.Enrollments
            .Any(e => e.StudentId == enrollmentDto.StudentId && e.CourseId == enrollmentDto.CourseId);

        if (existingEnrollment)
        {
            throw new InvalidOperationException("The student is already enrolled in this course.");
        }

        try
        {
            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId,
                EnrollmentDate = DateTime.Now
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            _context.Entry(enrollment).Reference(e => e.Student).Load();
            _context.Entry(enrollment).Reference(e => e.Course).Load();

            return enrollment;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error saving the enrollment to the database.", ex);
        }
    }

    public IEnumerable<object> ListStudentsByCourse(string courseName)
    {
        var students = _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Where(e => e.Course.Name == courseName)
            .Select(e => new {
                id = e.Student.Id,
                name = e.Student.Name,
                email = e.Student.Email,
                enrollmentDate = e.EnrollmentDate.ToString("yyyy-MM-dd")
            })
            .ToList();

        return students;
    }

    public IEnumerable<object> ListCoursesByStudent(string studentName)
    {
        var courses = _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Where(e => e.Student.Name == studentName)
            .Select(e => new {
                id = e.Course.Id,
                name = e.Course.Name,
                description = e.Course.Description,
                enrollmentDate = e.EnrollmentDate.ToString("yyyy-MM-dd")
            })
            .ToList();

        return courses;
    }

    public Enrollment GetEnrollmentById(int id)
    {
        var enrollment = _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefault(e => e.Id == id);

        if (enrollment == null)
        {
            throw new KeyNotFoundException("Enrollment not found.");
        }

        return enrollment;
    }

    public IEnumerable<Enrollment> ListEnrollments()
    {
        return _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToList();
    }

    public void DeleteEnrollment(int id)
    {
        var enrollment = _context.Enrollments.FirstOrDefault(e => e.Id == id);
        if (enrollment == null)
        {
            throw new KeyNotFoundException("Enrollment not found.");
        }

        try
        {
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error deleting the enrollment from the database.", ex);
        }
    }
}
