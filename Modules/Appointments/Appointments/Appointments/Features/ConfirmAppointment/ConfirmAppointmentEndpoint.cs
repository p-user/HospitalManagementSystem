using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Constants;
using System.Security.Claims;

namespace Appointments.Appointments.Features.ConfirmAppointment
{
    public class ConfirmAppointmentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/appoinments/{id}/confirm-appoinment", async ([FromRoute]Guid id, HttpContext context, ISender sender) =>
            {
                             
                //we need the doctor id 
                var doctor = context.User?.Claims.FirstOrDefault(c => c.Type == "DoctorId")?.Value;
                Guid DoctorId = Guid.Parse(doctor);

                var command = new ConfirmAppointmentCommand(id, DoctorId);
                var result = await sender.Send(command);
                return TypedResults.Ok(result);

               
            }).WithDescription("Confirm-Appointment")
            .WithName("Confirm-Appointment")
                .RequireAuthorization(Policies.DoctorOnly);


            
        }
    }
}
