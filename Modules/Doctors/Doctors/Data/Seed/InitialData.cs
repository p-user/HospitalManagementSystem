
namespace Shared.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Specialization> Specializations => new List<Specialization>
        {
            Specialization.Create(name: "Cardiologist", description: "Specializes in diagnosing and treating heart conditions and diseases."),
            Specialization.Create(name: "Neurologist", description: "Focuses on disorders of the brain, spinal cord, and nerves."),
            Specialization.Create(name : "Orthopedic Surgeon", description : "Treats injuries and conditions of the musculoskeletal system."),
            Specialization.Create(name : "Pediatrician", description : "Provides medical care for infants, children, and adolescents."),
            Specialization.Create(name : "Oncologist", description : "Specializes in the diagnosis and treatment of cancer."),
            Specialization.Create(name : "Dermatologist", description : "Specializes in skin, hair, and nail conditions."),
            Specialization.Create(name : "Psychiatrist", description : "Focuses on mental, emotional, and behavioral disorders."),
            Specialization.Create(name : "General Surgeon", description : "Performs a broad range of surgeries on various body parts."),
            Specialization.Create(name : "Gynecologist", description : "Focuses on the health of the female reproductive systems."),
            Specialization.Create(name : "Endocrinologist", description : "Treats hormone imbalances and endocrine gland diseases.")



        };
        public static IEnumerable<DoctorFeedDto> Doctors => new List<DoctorFeedDto>
        {
             new DoctorFeedDto(
                Name: "Ahmet",
                Surname: "Yılmaz",
                Department: "Cardiology",
                Specialization: "Cardiologist",
                WorkingStartDate: new DateOnly(2010, 5, 12),
                GraduatedUniversity: "Istanbul University"
            ),
            new DoctorFeedDto(
                Name: "Fatma",
                Surname: "Kara",
                Department: "Neurology",
                Specialization: "Neurologist",
                WorkingStartDate: new DateOnly(2015, 8, 20),
                GraduatedUniversity: "Hacettepe University"
            ),
            new DoctorFeedDto(
                Name: "Mehmet",
                Surname: "Demir",
                Department: "Orthopedics",
                Specialization: "Orthopedic Surgeon",
                WorkingStartDate: new DateOnly(2012, 3, 15),
                GraduatedUniversity: "Ege University"
            ),
            new DoctorFeedDto(
                Name: "Ayşe",
                Surname: "Çelik",
                Department: "Pediatrics",
                Specialization: "Pediatrician",
                WorkingStartDate: new DateOnly(2017, 11, 2),
                GraduatedUniversity: "Ankara University"
            ),
            new DoctorFeedDto(
                Name: "Ali",
                Surname: "Şahin",
                Department: "Oncology",
                Specialization: "Oncologist",
                WorkingStartDate: new DateOnly(2008, 6, 25),
                GraduatedUniversity: "Dokuz Eylul University"
            ),
            new DoctorFeedDto(
                Name: "Elif",
                Surname: "Arslan",
                Department: "Dermatology",
                Specialization: "Dermatologist",
                WorkingStartDate: new DateOnly(2014, 3, 8),
                GraduatedUniversity: "Marmara University"
            ),
            new DoctorFeedDto(
                Name: "Burak",
                Surname: "Tekin",
                Department: "Psychiatry",
                Specialization: "Psychiatrist",
                WorkingStartDate: new DateOnly(2013, 10, 10),
                GraduatedUniversity: "Uludag University"
            ),
            new DoctorFeedDto(
                Name: "Gül",
                Surname: "Koç",
                Department: "Surgery",
                Specialization: "General Surgeon",
                WorkingStartDate: new DateOnly(2011, 1, 15),
                GraduatedUniversity: "Gazi University"
            ),
            new DoctorFeedDto(
                Name: "Emre",
                Surname: "Polat",
                Department: "Gynecology",
                Specialization: "Gynecologist",
                WorkingStartDate: new DateOnly(2016, 7, 5),
                GraduatedUniversity: "Akdeniz University"
            ),
            new DoctorFeedDto(
                Name: "Derya",
                Surname: "Öztürk",
                Department: "Endocrinology",
                Specialization: "Endocrinologist",
                WorkingStartDate: new DateOnly(2009, 9, 1),
                GraduatedUniversity: "Çukurova University"
            )
        };



        
    }
}
