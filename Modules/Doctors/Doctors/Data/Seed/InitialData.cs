using Doctors.Doctors.Dtos;


namespace Shared.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<DoctorDto> Doctors => new List<DoctorDto>
        {
             new DoctorDto

                (
                    Name : "Selin",
                    Surname:"Aydın",
                    Department: "Pediatrics",
                    Specialization:"Neonatology",
                    WorkingStartDate : new DateOnly(2011, 6, 10),
                    GraduatedUniversity : "Ondokuzmayis University"
                ),


              new DoctorDto

                (
                    Name : "Emre",
                    Surname : "Kopi",
                    Department : "Radiology",
                    Specialization : "Interventional Radiology",
                    WorkingStartDate : new DateOnly(2014, 4, 15),
                    GraduatedUniversity : "Middle East Technical University"
                 ),

               new DoctorDto

                (
                    Name : "Burcu",
                    Surname : "Ozkan",
                    Department : "Ophthalmology",
                    Specialization : "Retina Specialist",
                    WorkingStartDate : new DateOnly(2013, 9, 5),
                    GraduatedUniversity : "Ankara University"

                ),
                new DoctorDto
                (

                    Name : "Burcu",
                    Surname : "Karaman",
                    Department : "Ophthalmology",
                    Specialization : "Retina Specialist",
                    WorkingStartDate : new DateOnly(2013, 9, 5),
                    GraduatedUniversity : "Samsun University"
                 ),
                new DoctorDto
                (
                     Name : "Derya",
                    Surname : "Bozkurt",
                    Department : "Endocrinology",
                    Specialization : "Diabetes Specialist",
                    WorkingStartDate : new DateOnly(2009, 12, 30),
                    GraduatedUniversity : "Yıldız Teknik University"
                 )



        };
    }
}
