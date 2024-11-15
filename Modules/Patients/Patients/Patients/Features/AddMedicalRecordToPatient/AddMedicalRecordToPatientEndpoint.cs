
using Shared.Constants;

namespace Patients.Patients.Features.AddMedicalRecordToPatient
{
    public class AddAllergyToMedicalRecordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/patients/{id}/medicalrecords", async ([FromRoute] Guid id, MedicalRecordDto record, ISender sender) =>
            {
                if(id != record.PatientId)
                {
                    throw new ArgumentException("BadRequest!!");
                }

                var command = new AddMedicalRecordToPatientCommand(record);
                var response = await sender.Send(command);
                return Results.Ok(response);

            }).WithDescription("Add medical record to patient")
            .WithName("AddMedicalRecordToPatient")
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .RequireAuthorization(Policies.DoctorOnly,Policies.AdminOnly);      
                
        }
    }
}
