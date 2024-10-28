

namespace Doctors.Doctors.Dtos
{
   public record DoctorDto
    (
     string Name ,
     string Surname ,
     string Department ,
     Guid Specialization ,
     DateOnly WorkingStartDate ,
     string GraduatedUniversity 
       );

    public record DoctorFeedDto
    (
     string Name,
     string Surname,
     string Department,
     String Specialization,
     DateOnly WorkingStartDate,
     string GraduatedUniversity
     );
}
