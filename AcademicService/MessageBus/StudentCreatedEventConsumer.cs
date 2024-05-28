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
    }

    public async Task Consume(ConsumeContext<StudentCreatedEvent> context)
    {
        var studentId = context.Message.StudentId;

        // Logging the received event
        _logger.LogInformation("Received StudentCreatedEvent with StudentId: {StudentId}", studentId);

        var student = new Student
        {
            Id = studentId,
            Name = context.Message.Name,
            Department = context.Message.Department
        };

        await _studentService.RegisterStudentAsync(student);
    }
}

