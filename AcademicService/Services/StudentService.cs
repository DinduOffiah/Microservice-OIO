using AcademicService.Interface;
using AcademicService.Models;
using AppDbContext.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Services
{
    public class StudentService : IStudentService
    {
        private readonly AcademicDbContext _context;


        public StudentService(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<Student> RegisterStudentAsync(Student student)
        {
            student.Id = Guid.NewGuid();
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
    }

}
