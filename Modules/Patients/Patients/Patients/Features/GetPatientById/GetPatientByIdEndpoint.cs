

using Shared.Constants;

namespace Patients.Patients.Features.GetPatientById
{
    public class GetPatientByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/patients/{id}", async ([FromRoute] Guid id, ISender sender) =>
            {
                var query = new GetPatientByIdQuery(id);
                var response = await sender.Send(query);
                return Results.Ok(response);
            }).WithDescription("Get a specific patient")
            .Produces<PatientDto>(StatusCodes.Status200OK)
            .WithName("GetPatient")
            .RequireAuthorization(Policies.AdminOnly,Policies.DoctorOnly); 
        }
    }
}
