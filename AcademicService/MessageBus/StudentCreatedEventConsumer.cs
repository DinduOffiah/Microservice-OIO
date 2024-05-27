using AcademicService.Consumers;
using AcademicService.Interface;
using AcademicService.Models;
using MassTransit;

public class StudentCreatedEventConsumer : IConsumer<StudentCreatedEvent>
{
    private readonly IStudentService _studentService;

    public StudentCreatedEventConsumer(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task Consume(ConsumeContext<StudentCreatedEvent> context)
    {
        var studentId = context.Message.StudentId;

        // Create a new student with the given ID
        var student = new Student 
        {  
            Id = studentId,
            Name = context.Message.Name,
            Department = context.Message.Department
        };

        // Use the IStudentService to save the student to the database
        await _studentService.RegisterStudentAsync(student);
    }
}
