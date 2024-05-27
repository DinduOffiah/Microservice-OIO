namespace AdministrationService.Publisher
{
    public class StudentCreatedEvent
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        public StudentCreatedEvent(Guid studentId, string name, string department)
        {
            StudentId = studentId;
            Name = name;
            Department = department;
        }
    }
}
