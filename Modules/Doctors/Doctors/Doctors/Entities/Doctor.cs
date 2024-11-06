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

        //from authentication  module
        public string Email { get; private set; }

        //from departments module
        public bool IsHeadOfDepartment { get; private set; } = false; 



        public static Doctor Create(string name, string surname, Guid departmentId, Guid specializationId, DateOnly workingStartDate, string graduatedUniversity,string email)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(surname);
            ArgumentException.ThrowIfNullOrEmpty(graduatedUniversity);



             //Create
             var doctor = new Doctor
            {

                Id=Guid.NewGuid(),
                Name = name,
                Surname = surname,
                DepartmentId = departmentId,
                SpecializationId = specializationId,
                WorkingStartDate = workingStartDate,
                GraduatedUniversity = graduatedUniversity,
                Email = email
            };

            doctor.AddDomainEvent(new DoctorAddedToDepartmentDomainEvent(doctor)); 

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
            SpecializationId = dto.SpecializationId;
            WorkingStartDate = dto.WorkingStartDate;
            GraduatedUniversity = dto.GraduatedUniversity;

            if (DepartmentId != dto.DepartmentId)
            {
                DepartmentId = dto.DepartmentId;
                AddDomainEvent(new DoctorChangedDepartmentDomainEvent(this));

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


        public void SetHeadOfDepartment(bool _isHeadOfDepartment)
        {
            IsHeadOfDepartment = _isHeadOfDepartment;
        }




    }
}
