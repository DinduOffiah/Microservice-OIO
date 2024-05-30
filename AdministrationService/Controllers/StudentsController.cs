using AdministrationService.DTOs;
using AdministrationService.Interface;
using AdministrationService.Models;
using AdministrationService.Publisher;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace AdministrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IBus _bus;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, IBus bus, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _bus = bus;
            _logger = logger;
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();

            if (students != null)
            {
                return Ok(students);
            }
            else
            {
                return NotFound("No student found.");
            }


        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentDTO studentDTO)
        {
            var student = new Student
            {
                Name = studentDTO.Name,
                Department = studentDTO.Department,
            };

            try
            {
                _logger.LogInformation("Registering student with Name: {Name}, Department: {Department}", student.Name, student.Department);
                await _studentService.RegisterStudentAsync(student);
                _logger.LogInformation("Registered student with Id: {Id}", student.Id);

                _logger.LogInformation("Publishing StudentCreatedEvent for StudentId: {StudentId}", student.Id);
                await _bus.Publish(new StudentCreatedEvent(student.Id, student.Name, student.Department));
                _logger.LogInformation("Published StudentCreatedEvent for StudentId: {StudentId}", student.Id);

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the student");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
