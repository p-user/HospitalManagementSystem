using Doctors.Doctors.Dtos;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Doctors.Entities
{
    public class Doctor: Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Surname { get; private set; } = default!;
        public string Department { get; private set; } = default!;
        public string Specialization { get; private set; } = default!;
        public DateOnly WorkingStartDate { get; private set; }
        public string GraduatedUniversity { get; private set; } = default!;

        public static Doctor Create(DoctorDto dto)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(dto.Name);
            ArgumentException.ThrowIfNullOrEmpty(dto.Surname);
            ArgumentException.ThrowIfNullOrEmpty(dto.Department);
            ArgumentException.ThrowIfNullOrEmpty(dto.Specialization);
            ArgumentException.ThrowIfNullOrEmpty(dto.GraduatedUniversity);

            //Create
            var doctor = new Doctor
            {

                Id=Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                Department = dto.Department,
                Specialization = dto.Specialization,
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
            ArgumentException.ThrowIfNullOrEmpty(dto.Department);
            ArgumentException.ThrowIfNullOrEmpty(dto.Specialization);
            ArgumentException.ThrowIfNullOrEmpty(dto.GraduatedUniversity);

           

            Name = dto.Name;
            Surname = dto.Surname;
            Department = dto.Department;
            Specialization = dto.Specialization;
            WorkingStartDate = dto.WorkingStartDate;
            GraduatedUniversity = dto.GraduatedUniversity;
        }
    }
}
