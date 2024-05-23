using AppDbContext.Data;
using Microsoft.EntityFrameworkCore;
using RegisterService.Interface;
using RegisterService.Models;

namespace RegisterService.Services
{
    public class StudentService : IStudentService
    {
        private readonly RegisterDbContext _context;
        private readonly INotificationService _notificationService;


        public StudentService(RegisterDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Patient> RegisterPatientAsync(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            // Notify VitalService about new patient
            await _notificationService.NotifyNewPatientAsync(patient);

            return patient;
        }

        public async Task<Patient> GetPatientAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }
    }

}
