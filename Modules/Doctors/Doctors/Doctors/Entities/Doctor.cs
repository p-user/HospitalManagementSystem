using Shared;

namespace Doctors.Doctors.Entities
{
    public class Doctor: Entity<Guid> //should be an aggrehhate  => entity + events
    {
        public string Name { get; private set; } = default!;
        public string Surname { get; private set; } = default!;
        public Guid DepartmentId { get; private set; } 
        public Guid SpecializationId { get; private set; }
        public Specialization Specialization { get; private set; } = default!;
        public DateOnly WorkingStartDate { get; private set; }
        public string GraduatedUniversity { get; private set; } = default!;

        public static Doctor Create(DoctorDto dto)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(dto.Name);
            ArgumentException.ThrowIfNullOrEmpty(dto.Surname);
            ArgumentException.ThrowIfNullOrEmpty(dto.GraduatedUniversity);



             //Create
             var doctor = new Doctor
            {

                Id=Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                 DepartmentId = dto.DepartmentId,
                SpecializationId = dto.Specialization,
                WorkingStartDate = dto.WorkingStartDate,
                GraduatedUniversity = dto.GraduatedUniversity,
            };

            return doctor;
        }

        public void Update(DoctorDto dto)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(dto.Name);
            ArgumentException.ThrowIfNullOrEmpty(dto.Surname);
            ArgumentException.ThrowIfNullOrEmpty(dto.GraduatedUniversity);

           

            Name = dto.Name;
            Surname = dto.Surname;
            DepartmentId = dto.DepartmentId;
            SpecializationId = dto.Specialization;
            WorkingStartDate = dto.WorkingStartDate;
            GraduatedUniversity = dto.GraduatedUniversity;
        }

        public static Doctor Create(DoctorFeedDto dto, Guid SpecializationId)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(dto.Name);
            ArgumentException.ThrowIfNullOrEmpty(dto.Surname);
            ArgumentException.ThrowIfNullOrEmpty(dto.Department);
            ArgumentException.ThrowIfNullOrEmpty(dto.GraduatedUniversity);



            //Create
            var doctor = new Doctor
            {

                Id = Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
               // Department = dto.Department,
                SpecializationId = SpecializationId,
                WorkingStartDate = dto.WorkingStartDate,
                GraduatedUniversity = dto.GraduatedUniversity,
            };

            return doctor;
        }




    }
}
