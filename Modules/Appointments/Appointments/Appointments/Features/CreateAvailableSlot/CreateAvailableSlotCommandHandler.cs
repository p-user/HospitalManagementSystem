

namespace Appointments.Appointments.Features.CreateAvailableSlot
{
    public record CreateAvailableSlotCommand(AvailableSlotDto dto) : IRequest<CreateAvailableSlotCommandResponse>;
    public record CreateAvailableSlotCommandResponse(Guid Id);
    public class CreateAvailableSlotCommandHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<CreateAvailableSlotCommand, CreateAvailableSlotCommandResponse>
    {
        public async Task<CreateAvailableSlotCommandResponse> Handle(CreateAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            //verify shift + doctor

            var entity = CreateAvailableSlot(request.dto);
            appointmentsDbContext.AvailableSlots.Add(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new CreateAvailableSlotCommandResponse(entity.Id);

        }

        private AvailableSlot CreateAvailableSlot(AvailableSlotDto dto)
        {
            return AvailableSlot.Create(dto.DoctorId, dto.ShiftId, dto.StartTime, dto.EndTime, (Entities.SlotStatus)dto.Status, dto.AppointmentLimit);
        }
    }
}
