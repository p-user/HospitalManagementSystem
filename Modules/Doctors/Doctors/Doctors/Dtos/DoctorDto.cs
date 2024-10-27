

namespace Doctors.Doctors.Dtos
{
   public record DoctorDto
    (
     string Name ,
     string Surname ,
     string Department ,
     string Specialization ,
     DateOnly WorkingStartDate ,
     string GraduatedUniversity 
       );
}
