

namespace Patients.Patients.Features.GetMedicalRecorsByPatient
{
    public class GetMedicalRecordsByPatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/patients/{id}/medicalRecords", async ([FromRoute] Guid Id,ISender sender) =>
            {
                var response = await sender.Send(new GetMedicalRecordsByPatientQuery(Id));
                return Results.Ok(response);
            }).WithDescription("Get Medical records of a patient")
            .WithName("GetMedicalRecords")
            .RequireAuthorization();
        }
    }
}
