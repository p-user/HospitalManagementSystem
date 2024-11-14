
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Appointments.Appointments.Features.RequestAppointment
{
    public class RequestAppointmentEndpoint : ICarterModule
    {
         private readonly IServiceProvider _serviceProvider;

        public RequestAppointmentEndpoint(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var userEmail = string.Empty;
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IClaimsService>();
                //to do
            }
            app.MapPost("/appointments/availableSlots/requestAppoinment", async(ISender sender) => 
            {
            }).WithName("RequestAppointment")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);
                
        }
    }
}
