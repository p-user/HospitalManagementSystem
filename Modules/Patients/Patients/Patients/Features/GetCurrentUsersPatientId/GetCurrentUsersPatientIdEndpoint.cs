
using Microsoft.Extensions.DependencyInjection;
using Patients.Patients.Features.GetCurrentUsersPatientId;
using Shared.Services;

namespace Patients.Patients.Features.GetCurrentUserMedicalRecords
{
    public class GetCurrentUsersPatientIdEndpoint : ICarterModule
    {
        private readonly IServiceProvider _serviceProvider;

        public GetCurrentUsersPatientIdEndpoint(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddRoutes(IEndpointRouteBuilder app)
        {
           
            app.MapGet("/patients/GetUserId", async (ISender sender) =>
            {
                string userEmail = string.Empty;
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<IClaimsService>();
                    userEmail = context.GetCurrentUserEmail();

                }
                var result = await sender.Send(new GetCurrentUsersPatientIdQuery(userEmail));
                return Results.Ok(result);

            }).WithName("GetMyPatientId")
                .RequireAuthorization();
                
        }
    }
}
