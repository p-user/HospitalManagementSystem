namespace Doctors.Contracts.Doctors.Dtos
{
    public record DoctorDto
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public Guid DepartmentId { get; init; }
        public Guid SpecializationId { get; init; }
        public DateOnly WorkingStartDate { get; init; }
        public string GraduatedUniversity { get; init; }
        public string Email { get; init; }

    };

    public record DoctorFeedDto
    (
     string Name,
     string Surname,
     string Department,
     string Specialization,
     DateOnly WorkingStartDate,
     string GraduatedUniversity
     //string Email
     );
}
