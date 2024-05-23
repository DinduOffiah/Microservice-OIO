using AdministrationService.Models;

namespace AdministrationService.Interface
{
    public interface IStudentService
    {
        Task<Student> RegisterStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
    }

}
