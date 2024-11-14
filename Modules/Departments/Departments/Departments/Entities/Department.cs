using Departments.Departments.Events;
using Shared.DDD;

namespace Departments.Departments.Entities
{
    public class Department : Aggregate<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Guid? HeadOfDepartment { get; private set; } = default!;
        public List<Guid> Doctors { get; private set; }



        //add working shifts 

        public static List<WorkingShift> _workingShifts = new();
        public IReadOnlyList<WorkingShift> WorkingShifts= _workingShifts.AsReadOnly();


        public static Department Create(string name, string description)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);

            //Create
            var entity = new Department
            {

                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Doctors = new List<Guid>()


            };

            return entity;
        }


        public void Update(string name, string description, Guid headOfDepartment)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);

            //Update

            Name = name;
            Description = description;
            HeadOfDepartment = headOfDepartment;


        }

        public void AssignHeadOfDepartment(Guid headOfDepartment)
        {
            if (!Doctors.Any(s => s == headOfDepartment))
            {
                throw new ArgumentException("Doctor should be registered as part of the department!");
            }

            HeadOfDepartment = headOfDepartment;

            //create domain event to notify doctor's module
            AddDomainEvent(new HeadOfDepartmentAssignedDomainEvent(headOfDepartment));

        }

        public void AddDoctorToDepartment(Guid DoctorId)
        {
            if (!Doctors.Any(s => s == DoctorId))
            {
                Doctors.Add(DoctorId);
            }

        }

        public void RemoveDoctorFromDepartment(Guid DoctorId)
        {
            if (Doctors.Any(s => s == DoctorId))
            {
                Doctors.Remove(DoctorId);
            }

        }

    }
}
