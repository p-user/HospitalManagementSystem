namespace Doctors.Contracts.Doctors.Dtos
{
    public record DoctorDto
    {
        public string Name;
        public string Surname;
        public Guid DepartmentId;
        public Guid SpecializationId;
        public DateOnly WorkingStartDate;
        public string GraduatedUniversity;
        public string Email;

    }
        ;

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
