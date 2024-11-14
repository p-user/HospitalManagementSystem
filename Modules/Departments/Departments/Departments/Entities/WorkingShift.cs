
using Departments.Departments.Events;
using Shared.DDD;

namespace Departments.Departments.Entities
{
    public class WorkingShift : Aggregate<Guid>
    {
        public ShiftName ShiftName { get; private set; } 
        public DateTime Date {  get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Department Department { get; private set; }


        public List<Guid> DoctorsOnDuty { get; private set; }

        public static WorkingShift Create(Guid departmentId, ShiftName shiftName, DateTime dateTime)
        {
            var entity = new WorkingShift
            {
                ShiftName = shiftName,
                Date = dateTime,
                DepartmentId = departmentId,
            };

            entity.SetShiftDetails();
            return entity;
        }
        public void SetShiftDetails()
        {

            switch (ShiftName)
            {
                case ShiftName.Morning:
                    StartTime = Date.AddHours(8);  // Example: 8:00 AM
                    EndTime = Date.AddHours(16);   // Example: 4:00 PM
                    break;

                case ShiftName.Night:
                    StartTime = Date.AddHours(20); // Example: 8:00 PM
                    EndTime = Date.AddHours(4).AddDays(1); // Example: 4:00 AM next day
                    break;

                default:
                    throw new ArgumentException("Invalid shift name");
            }
        }

        public void AddDoctorToShift(Guid doctorId)
        {
            if (DoctorsOnDuty.Contains(doctorId))
            {
                throw new ArgumentException("This doctor is already assigned in this shift!");
            }

            DoctorsOnDuty.Add(doctorId);

            //notify doctor module via domain event
            AddDomainEvent(new DoctorAddedToWorkingShiftDomainEvent(Id,doctorId));

        }

        public void RemoveDoctorFromShift(Guid doctorId)
        {
            if (!DoctorsOnDuty.Contains(doctorId))
            {
                throw new ArgumentException("This doctor was not assigned in this shift before!");
            }

            DoctorsOnDuty.Remove(doctorId);

            //TODO: notify doctor module via domain event

        }
    }


    public enum ShiftName
    {
        Morning,
        Night
    }
}
