using AdministrationService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace AdministrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();

            if(students != null)
            {
                return Ok(students);
            }
            else
            {
                return NotFound("No student found.");
            }

           
        }

       [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] PatientDTO patientDto)
        {
            var patient = new Patient
            {
                Name = patientDto.Name,
                DateOfBirth = patientDto.DateOfBirth
            };

            try
            {
                await _patientService.RegisterPatientAsync(patient);
                return Ok(patient);
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "An error occurred during patient registration");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("GetPatient/{id}")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await _patientService.GetPatientAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }

}
