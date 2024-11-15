
using Shared.Constants;

namespace Patients.Patients.Features.AddAllegyToPatient
{
    public class AddAllergyToPatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/patients/{id}/allergies", async ([FromRoute] Guid id, AllergyDto allergy, ISender sender) =>
            {
                var command = new AddAllegyToPatientCommand(id, allergy);
                var response = await sender.Send(command);
                return Results.Ok(response);

            }).WithDescription("Add Allergy to patient")
            .WithName("AddAllergyToPatient")
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Policies.DoctorOnly);

        }
    }
}
