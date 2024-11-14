namespace Patients.Patients.Features.CreatePatient
{
    public class CreatePatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/patients", async ([FromBody] PatientDto request, ISender sender) =>
            {
                var command = new CreatePatientCommand(request);
                var result = await sender.Send(command);
                return Results.Created($"/doctors/{result.PatientId}", result);
            })
            .WithDescription("Create patient entity")
            .WithName("CreatePatient")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<Guid>(StatusCodes.Status201Created);
            
        }
    }
}
