
namespace Patients.Patients.Features.AddMedicalRecordToPatient
{
    public class AddAllergyToMedicalRecordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/patients/{PatientId}/medicalrecords", async ([FromRoute] Guid PatientId, MedicalRecordDto record, ISender sender) =>
            {
                if(PatientId != record.PatientId)
                {
                    throw new ArgumentException("BadRequest!!");
                }

                var command = new AddMedicalRecordToPatientCommand(record);
                var response = await sender.Send(command);
                return Results.Ok(response);

            }).WithDescription("Add medical record to patient")
            .WithName("AddMedicalRecordToPatient")
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);      
                
        }
    }
}
