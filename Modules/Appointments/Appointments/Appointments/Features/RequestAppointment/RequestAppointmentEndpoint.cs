
using Appointments.Appointments.Features.CreateAvailableSlot;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using Shared.Services;
using System.Security.Claims;

namespace Appointments.Appointments.Features.RequestAppointment
{
    public class RequestAppointmentEndpoint : ICarterModule
    {
         

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var userEmail = string.Empty;
           
            app.MapPost("/appointments/availableSlots/{slotId}/requestAppoinment", async([FromRoute]Guid slotId, HttpContext context,ISender sender) => 
            {
                var role = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                Guid PatientId;
                if (role != DefaultRoles.PatientRole)
                {
                    //we need the patient id 
                    var patient = context.User?.Claims.FirstOrDefault(c => c.Type == "PatientId")?.Value;
                    PatientId = Guid.Parse(patient);
                    var command = new RequestAppointmentCommand(slotId,PatientId);
                    var result = await sender.Send(command);
                    return Results.Ok(result);

                }
                else
                {
                    throw new Exception("Only patients should require an appoinment");
                }

            }).WithName("RequestAppointment")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .RequireAuthorization();
                
        }
    }
}
