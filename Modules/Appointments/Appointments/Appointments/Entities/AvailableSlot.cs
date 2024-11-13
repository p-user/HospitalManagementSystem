

namespace Appointments.Appointments.Entities
{
    public class AvailableSlot : Aggregate<Guid>
    {
        public Guid DoctorId { get; private set; }
        public Guid ShiftId { get; private set; }  // Links to the WorkingShift in Departments
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public SlotStatus Status { get; private set; }

        public int AppoinmentLimit {  get; private set; }

        //list of appointments reference 

        private static List<Appointment> _appointments;
        public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();


        public static AvailableSlot Create(Guid doctorId, Guid shiftId, DateTime startTime, DateTime endTime, SlotStatus status,int appoinmentLimit)
        {
            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(doctorId.ToString());
            ArgumentException.ThrowIfNullOrEmpty(shiftId.ToString());

            //Create
            var entity = new AvailableSlot
            {

                Id = Guid.NewGuid(),
                DoctorId = doctorId,
                ShiftId = shiftId,
                StartTime = startTime,
                EndTime = endTime,
                Status = status,
                AppoinmentLimit = appoinmentLimit


            };

            return entity;
        }

        public void Update (DateTime startTime, DateTime endTime, int appoinmentLimit)
        {
            StartTime = startTime;
            EndTime = endTime;
            AppoinmentLimit = appoinmentLimit;
        }

        public void SetStatus(SlotStatus status) 
        { 
            Status = status;
        }

        public void AddAppoinment(Guid patientId)
        {
            if (AppoinmentLimit <= _appointments.Count)
            {
                throw new ArgumentException("Doctor is fully booked for this time slot!");
            }
            var check = _appointments.Where(s=>s.PatientId == patientId).Any();
            if (!check)
            {
                var appointment = Appointment.Create(patientId, DoctorId, Id);
                _appointments.Add(appointment);
            }
            else
            {
                throw new ArgumentException("Patient already has booked an appointment for this time slot!");
            }
        }

        public void ConfirmAppoinment(Guid AppoinmentId)
        {
            var appoinment = _appointments.Where(s => s.Id == AppoinmentId).FirstOrDefault();
            if (appoinment != null)
            {
                appoinment.ConfirmAppointment();
                
            }
        }

        public void CancelAppointment(Guid AppoinmentId)
        {
            var appoinment = _appointments.Where(s => s.Id == AppoinmentId).FirstOrDefault();
            if (appoinment != null)
            {
                appoinment.CancelAppointment();
                _appointments.Remove(appoinment);
                
            }
        }


        public void CheckBookingLimit()
        {
            if (AppoinmentLimit == _appointments.Count)
            {
                Status = SlotStatus.Booked;
            }
            else
            {
                Status = SlotStatus.Available;
            }
        }


    }


    public enum SlotStatus
    {
        Available,
        Booked,
        Canceled
    }

   
}
