
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Constants;

namespace Appointments.Appointments.Features.DeleteAvailableSlot
{
    public class DeleteAvailableSlotEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/appointments/availableSlots/{id}", async ([FromRoute] Guid id, ISender sender, HttpContext context) =>
            {
                //we need the doctor id 
                var doctorId = context.User?.Claims.FirstOrDefault(c => c.Type == "DoctorId")?.Value;
                Guid DoctorId = Guid.Parse(doctorId);

                var command = new DeleteAvailableSlotCommand(id, DoctorId);
                var response = await sender.Send(command);
                return TypedResults.Ok(response.Succeded);
            }).WithDescription("Delete an available slot")
            .WithName("DeleteAvailableSlot ")
            .RequireAuthorization(Policies.DoctorOrAdminOnly);
        }
    }
}
