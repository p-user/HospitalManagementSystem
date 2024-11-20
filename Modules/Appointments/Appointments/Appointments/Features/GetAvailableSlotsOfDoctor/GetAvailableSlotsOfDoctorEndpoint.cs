using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Appointments.Appointments.Features.GetAvailableSlotsOfDoctor
{
    public class GetAvailableSlotsOfDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/appointments/availableSlots", async ([FromQuery] Guid doctor, ISender sender) =>
            {
                var query = new GetAvailableSlotsOfDoctorQuery(doctor);
                var result = await sender.Send(query);
                return TypedResults.Ok(result);

            }).WithDescription("Retrieves a doctors' availability to appoinments")
            .WithName("GetAvailableSlotsOfDoctor");
        }
    }
}
