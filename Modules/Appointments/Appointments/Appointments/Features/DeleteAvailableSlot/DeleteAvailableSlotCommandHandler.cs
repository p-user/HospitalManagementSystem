

using Microsoft.EntityFrameworkCore;

namespace Appointments.Appointments.Features.DeleteAvailableSlot
{

    public record DeleteAvailableSlotCommand(Guid Id) : IRequest<DeleteAvailableSlotCommandResponse>;
    public record DeleteAvailableSlotCommandResponse(bool Succeded);
    public class DeleteAvailableSlotCommandHandler(AppointmentsDbContext appointmentsDbContext) : IRequestHandler<DeleteAvailableSlotCommand, DeleteAvailableSlotCommandResponse>
    {
        public async  Task<DeleteAvailableSlotCommandResponse> Handle(DeleteAvailableSlotCommand request, CancellationToken cancellationToken)
        {
            var entity = await appointmentsDbContext.AvailableSlots
                .Include(s=>s.Appointments)
                .Where(s=>s.Id==request.Id)
                .FirstOrDefaultAsync(cancellationToken);


            if (entity == null) { throw new NotFoundException($" Available slot was not found with Id {request.Id}"); }

            appointmentsDbContext.AvailableSlots.Remove(entity);
            await appointmentsDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteAvailableSlotCommandResponse(true);


        }
    }
}
