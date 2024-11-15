

using Departments.Contracts.Departments.Features.GetWorkingShiftQuery;

namespace Appointments.Appointments.Features.CreateAvailableSlot
{
    public record CreateAvailableSlotCommand(AvailableSlotDto dto) : IRequest<CreateAvailableSlotCommandResponse>;
    public record CreateAvailableSlotCommandResponse(Guid Id);
    public class CreateAvailableSlotCommandHandler(AppointmentsDbContext appointmentsDbContext,ISender sender) : IRequestHandler<CreateAvailableSlotCommand, CreateAvailableSlotCommandResponse>
    {
        public async Task<CreateAvailableSlotCommandResponse> Handle(CreateAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            //verify shift + doctor
            var shift = await sender.Send(new GetWorkingShiftQuery(request.dto.ShiftId));

            if(shift.WorkingShiftDto.DoctorId != request.dto.DoctorId)
            {
                throw new ArgumentException("This working shift does not correspond to the doctor you provided!");
            }


            var entity = CreateAvailableSlot(request.dto);
            appointmentsDbContext.AvailableSlots.Add(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new CreateAvailableSlotCommandResponse(entity.Id);

        }

        private AvailableSlot CreateAvailableSlot(AvailableSlotDto dto)
        {
            return AvailableSlot.Create((Guid)dto.DoctorId, dto.ShiftId, dto.StartTime, dto.EndTime, (Entities.SlotStatus)dto.Status, dto.AppointmentLimit);
        }
    }
}
