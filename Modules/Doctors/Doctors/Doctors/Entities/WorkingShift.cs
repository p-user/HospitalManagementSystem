
namespace Doctors.Doctors.Entities
{
    public class WorkingShift
    {
        public Guid ShiftId { get; private set; }

        public WorkingShift(Guid shiftId)
        {

            ShiftId = shiftId;
            
        }

    }
}
