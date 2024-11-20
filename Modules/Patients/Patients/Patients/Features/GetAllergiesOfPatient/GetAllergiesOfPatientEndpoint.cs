

namespace Patients.Patients.Features.GetAllergiesOfPatient
{
    public class GetAllergiesOfPatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/patients/{id}/allergies", async ([FromRoute] Guid id, ISender sender) =>
            {
                var query = new GetAllergiesOfPatientQuery(id);
                var result = await sender.Send(query);
                return TypedResults.Ok(result.Allergies);
            }).WithDescription("Gets allergies of a patient")
            .WithName("GetAllergies")
            .RequireAuthorization();

        }
    }
}
