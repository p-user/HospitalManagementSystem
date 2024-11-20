

using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Appointments.Appointments.Features.GetAvailableSlot
{
    public class GetAvailableSlotEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/appointments/availableSlots/{id}", async ([FromRoute] Guid id, ISender sender) =>
            {
                var query = new GetAvailableSlotQuery(id);
                var result = await sender.Send(query);
                return TypedResults.Ok(result.dto);
            }).WithDescription("Get specific time slot inside a working shift")
            .WithName("GetAvailableSlot")
            .Produces<AvailableSlotDto>(StatusCodes.Status200OK);
        }
    }
}
