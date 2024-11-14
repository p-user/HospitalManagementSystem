
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


        public void SetShiftDetails(ShiftName shiftName, DateTime date)
        {
            ShiftName = shiftName;

            switch (shiftName)
            {
                case ShiftName.Morning:
                    StartTime = date.AddHours(8);  // Example: 8:00 AM
                    EndTime = date.AddHours(16);   // Example: 4:00 PM
                    break;

                case ShiftName.Night:
                    StartTime = date.AddHours(20); // Example: 8:00 PM
                    EndTime = date.AddHours(4).AddDays(1); // Example: 4:00 AM next day
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

            //TODO: notify doctor module via domain event

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
