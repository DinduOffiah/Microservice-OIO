using AcademicService.Consumers;
using AcademicService.Interface;
using AcademicService.Models;
using MassTransit;

public class StudentCreatedEventConsumer : IConsumer<StudentCreatedEvent>
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentCreatedEventConsumer> _logger;

    public StudentCreatedEventConsumer(IStudentService studentService, ILogger<StudentCreatedEventConsumer> logger)
    {
        _studentService = studentService;
        _logger = logger;
        _logger.LogInformation("StudentCreatedEventConsumer initialized.");
    }

    public async Task Consume(ConsumeContext<StudentCreatedEvent> context)
    {
        _logger.LogInformation("Received StudentCreatedEvent with StudentId: {StudentId}", context.Message.StudentId);

        var student = new Student
        {
            Id = context.Message.StudentId,
            Name = context.Message.Name,
            Department = context.Message.Department
        };

        _logger.LogInformation("Registering student with StudentId: {StudentId}", student.Id);
        await _studentService.RegisterStudentAsync(student);
        _logger.LogInformation("Registered student with StudentId: {StudentId}", student.Id);
    }
}
