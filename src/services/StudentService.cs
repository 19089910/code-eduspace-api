using code_eduspace_api.Dtos;
using code_eduspace_api.Models;
using Microsoft.EntityFrameworkCore;
using code_eduspace_api;

public class StudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public Student CreateStudent(StudentDto studentDto)
    {
        if (string.IsNullOrWhiteSpace(studentDto.Name))
            throw new ArgumentException("Student name is required.");

        if (string.IsNullOrWhiteSpace(studentDto.Email))
            throw new ArgumentException("Student email is required.");

        if (studentDto.BirthDate == default)
            throw new ArgumentException("Birth date is required.");

        if (CalculateAge(studentDto.BirthDate) < 18)
            throw new ArgumentException("Student must be at least 18 years old.");

        var student = new Student
        {
            Name = studentDto.Name,
            Email = studentDto.Email,
            BirthDate = studentDto.BirthDate
        };

        _context.Students.Add(student);
        _context.SaveChanges();
        return student;
    }

    public Student UpdateStudent(int id, StudentDto studentDto)
    {
        var existingStudent = _context.Students.FirstOrDefault(s => s.Id == id);
        if (existingStudent == null)
            throw new Exception("Student not found.");

        if (string.IsNullOrWhiteSpace(studentDto.Name))
            throw new ArgumentException("Student name is required.");

        if (string.IsNullOrWhiteSpace(studentDto.Email))
            throw new ArgumentException("Student email is required.");

        if (studentDto.BirthDate == default)
            throw new ArgumentException("Birth date is required.");

        if (CalculateAge(studentDto.BirthDate) < 18)
            throw new ArgumentException("Student must be at least 18 years old.");

        existingStudent.Name = studentDto.Name;
        existingStudent.Email = studentDto.Email;
        existingStudent.BirthDate = studentDto.BirthDate;

        _context.SaveChanges();
        return existingStudent;
    }

    public Student GetStudentById(int id) => _context.Students.Find(id);

    public IEnumerable<Student> ListStudents() => _context.Students.ToList();

    public bool DeleteStudent(int id)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == id);
        if (student == null) return false;

        _context.Students.Remove(student);
        _context.SaveChanges();
        return true;
    }

    private int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;
        return age;
    }
}
