using Doctors.Doctors.Dtos;


namespace Shared.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<SpecializationDto> Specializations => new List<SpecializationDto>
        {
            new SpecializationDto(Name: "Cardiology", Description: "Specializes in diagnosing and treating heart conditions and diseases."),
            new SpecializationDto(Name: "Neurology", Description: "Focuses on disorders of the nervous system, including the brain and spinal cord."),
            new SpecializationDto(Name: "Orthopedics", Description: "Deals with the musculoskeletal system, including bones, joints, ligaments, and muscles."),
            new SpecializationDto(Name: "Pediatrics", Description: "Provides medical care for infants, children, and adolescents."),
            new SpecializationDto(Name: "Dermatology", Description: "Specializes in skin conditions and diseases, including hair and nails."),
            new SpecializationDto(Name: "Oncology", Description: "Focuses on the treatment of cancer and tumors."),
            new SpecializationDto(Name: "Radiology", Description: "Uses imaging techniques like X-rays, MRIs, and CT scans for diagnosis and treatment."),
            new SpecializationDto(Name: "Psychiatry", Description: "Deals with the diagnosis, treatment, and prevention of mental health disorders."),
            new SpecializationDto(Name: "Gynecology", Description: "Focuses on women's reproductive health, including pregnancy and childbirth."),
            new SpecializationDto(Name: "Ophthalmology", Description: "Specializes in eye and vision care, including surgery for eye disorders."),
            new SpecializationDto(Name: "Endocrinology", Description: "Deals with disorders of the endocrine system, including diabetes and thyroid disorders."),
            new SpecializationDto(Name: "Gastroenterology", Description: "Focuses on diseases of the gastrointestinal tract, including the stomach and intestines."),
            new SpecializationDto(Name: "Pulmonology", Description: "Specializes in lung and respiratory conditions."),
            new SpecializationDto(Name: "Urology", Description: "Deals with urinary tract conditions and male reproductive organs.")



        };
        public static IEnumerable<DoctorFeedDto> Doctors => new List<DoctorFeedDto>
        {
            new DoctorFeedDto
            (
                Name: "Selin",
                Surname: "Aydın",
                Department: "Pediatrics",
                Specialization: "Pediatrics", 
                WorkingStartDate: new DateOnly(2011, 6, 10),
                GraduatedUniversity: "Ondokuzmayis University"
            ),

            new DoctorFeedDto
            (
                Name: "Emre",
                Surname: "Kopi",
                Department: "Radiology",
                Specialization: "Radiology", 
                WorkingStartDate: new DateOnly(2014, 4, 15),
                GraduatedUniversity: "Marmara University"
            ),

            new DoctorFeedDto
            (
                Name: "Burcu",
                Surname: "Ozkan",
                Department: "Ophthalmology",
                Specialization: "Ophthalmology", 
                WorkingStartDate: new DateOnly(2013, 9, 5),
                GraduatedUniversity: "Ankara University"
            ),

            new DoctorFeedDto
            (
                Name: "Burcu",
                Surname: "Karaman",
                Department: "Ophthalmology",
                Specialization: "Ophthalmology", 
                WorkingStartDate: new DateOnly(2013, 9, 5),
                GraduatedUniversity: "Samsun University"
            ),

            new DoctorFeedDto
            (
                Name: "Derya",
                Surname: "Bozkurt",
                Department: "Endocrinology",
                Specialization: "Endocrinology", 
                WorkingStartDate: new DateOnly(2009, 12, 30),
                GraduatedUniversity: "İstanbul Medipol University"
            )



        };
    }
}
