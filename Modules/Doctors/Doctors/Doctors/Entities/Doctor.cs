using Doctors.Events;
using Shared.DDD;

namespace Doctors.Doctors.Entities
{
    public class Doctor: Aggregate<Guid>
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

            doctor.AddDomainEvent(new DoctorAddedToDepartmentEvent(doctor));

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
            SpecializationId = dto.Specialization;
            WorkingStartDate = dto.WorkingStartDate;
            GraduatedUniversity = dto.GraduatedUniversity;

            if (DepartmentId != dto.DepartmentId)
            {
                DepartmentId = dto.DepartmentId;
                AddDomainEvent(new DoctorChangedDepartmentEvent(this));

            }
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
