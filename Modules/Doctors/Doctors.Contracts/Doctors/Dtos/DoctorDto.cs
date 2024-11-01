

namespace Doctors.Doctors.Dtos
{
   public record DoctorDto
    (
     string Name ,
     string Surname ,
     Guid DepartmentId ,
     Guid SpecializationId ,
     DateOnly WorkingStartDate ,
     string GraduatedUniversity 
       );

    public record DoctorFeedDto
    (
     string Name,
     string Surname,
     string Department,
     string Specialization,
     DateOnly WorkingStartDate,
     string GraduatedUniversity
     );
}
