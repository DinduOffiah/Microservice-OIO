using AdministrationService.Interface;
using AppDbContext.Data;
using Microsoft.EntityFrameworkCore;
using AdministrationService.Interface;
using AdministrationService.Models;

namespace AdministrationService.Services
{
    public class StudentService : IStudentService
    {
        private readonly AdminDbContext _context;


        public StudentService(AdminDbContext context)
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
