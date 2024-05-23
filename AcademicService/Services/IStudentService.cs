using AcademicService.Models;

namespace AcademicService.Interface
{
    public interface IStudentService
    {
        Task<Student> RegisterStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
    }

}
