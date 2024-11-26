
namespace Appointments.Appointments.Entities
{
    public class Appointment : Entity<Guid>
    {
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get;private set; }
        public Guid AvailableSlotId { get; private set; }  // connection to the AvailableSlot
        public AvailableSlot AvailableSlot { get; private set; }
        public AppointmentStatus Status { get; private set; }


        public static  Appointment Create(Guid patientId, Guid doctorId, Guid availableSlotId)
        {
            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(patientId.ToString());
            ArgumentException.ThrowIfNullOrEmpty(doctorId.ToString());


            var entity = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = patientId,
                DoctorId = doctorId,
                AvailableSlotId = availableSlotId,
                Status = AppointmentStatus.Pending

            };
            return entity;
        }

        public  void ConfirmAppointment()
        {
            if (Status != AppointmentStatus.Pending)
            {

                throw new InvalidOperationException("Only pending appointments can be confirmed.");
            }

            Status = AppointmentStatus.Confirmed;
        }

        public void CancelAppointment()
        {
            if (Status != AppointmentStatus.Canceled)
            {

                throw new InvalidOperationException("The appointment is already canceled");
            }

            Status = AppointmentStatus.Canceled;
        }
    }

    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Canceled
    }
}
