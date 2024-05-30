using AcademicService.DTOs;
using AcademicService.Models;
using AcademicService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenatelistController : ControllerBase
    {
        private readonly ISenateList _senateList;
        private readonly ILogger<SenatelistController> _logger;

        public SenatelistController(ISenateList senateList, ILogger<SenatelistController> logger)
        {
            _senateList = senateList;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            var list = await _senateList.GetSenateListAsync();

            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("No student found.");
            }

        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] SenateListDTO senateListDTO)
        {
            var student = new SenateList
            {
                Name = senateListDTO.Name,
                Degree = senateListDTO.Degree,
            };

            try
            {
                await _senateList.MakeSenateListAsync(student);
                _logger.LogInformation("Registered student with Id: {Id}", student.Id);

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
