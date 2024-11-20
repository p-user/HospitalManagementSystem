
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Constants;
using System.Security.Claims;

namespace Appointments.Appointments.Features.CreateAvailableSlot
{
    public class CreateAvailableSlotEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/appointments/availableSlots", async ([FromBody] AvailableSlotDto dto, HttpContext context, ISender sender) =>
            {
                var role = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if(role != DefaultRoles.AdminRole)
                {
                    //we need the doctor id 
                    var doctorId = context.User?.Claims.FirstOrDefault(c => c.Type == "DoctorId")?.Value;
                    dto.DoctorId = Guid.Parse(doctorId);

                }
                var command = new CreateAvailableSlotCommand(dto);
                var result = await sender.Send(command);
                return TypedResults.Ok(result);

            }).WithDescription("Create available time slot within a working shift so that patients can book an appoinment with the doctor.")
            .WithName("CreateAvailableSlot")
            .RequireAuthorization(Policies.DoctorOrAdminOnly);
        }
    }
}
