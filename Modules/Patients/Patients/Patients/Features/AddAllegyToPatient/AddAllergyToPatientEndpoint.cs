
using Shared.Constants;

namespace Patients.Patients.Features.AddAllegyToPatient
{
    public class AddAllergyToPatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/patients/{PatientId}/allergies", async ([FromRoute] Guid PatientId, AllergyDto allergy, ISender sender) =>
            {
                var command = new AddAllegyToPatientCommand(PatientId, allergy);
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
