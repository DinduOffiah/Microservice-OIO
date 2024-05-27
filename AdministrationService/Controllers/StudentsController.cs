using AdministrationService.DTOs;
using AdministrationService.Interface;
using AdministrationService.Models;
using AdministrationService.Publisher;
using MassTransit;
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
        private readonly IBus _bus;

        public StudentsController(IStudentService studentService, IBus bus)
        {
            _studentService = studentService;
            _bus = bus;
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
        public async Task<IActionResult> RegisterStudent([FromBody] StudentDTO studentDTO)
        {
            var student = new Student
            {
                Name = studentDTO.Name,
                Department = studentDTO.Department,
            };

            try
            {
                await _studentService.RegisterStudentAsync(student);

                await _bus.Publish(new StudentCreatedEvent(student.Id, student.Name, student.Department));

                return Ok(student);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
