
using MassTransit;
using Shared.Exceptions;
using Shared.Messaging.Events;

namespace Doctors.EventHandlers.EventConsumers
{
    public class DoctorAddedToWorkingShiftIntegrationEventConsumer(DoctorsDbContext  doctorsDbContext) : IConsumer<DoctorAddedToWorkingShiftIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<DoctorAddedToWorkingShiftIntegrationEvent> context)
        {
            var doctor = await doctorsDbContext.Doctors.Include(s=>s.WorkingShifts)
                .Where(s=>s.Id == context.Message.DoctorId)
                .FirstOrDefaultAsync();

            doctor.AddShiftToDoctor(context.Message.ShiftId);
            doctorsDbContext.Doctors.Update(doctor);
            await doctorsDbContext.SaveChangesAsync();

        }
    }
}
