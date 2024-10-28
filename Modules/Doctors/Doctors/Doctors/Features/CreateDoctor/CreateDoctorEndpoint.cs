namespace Doctors.Doctors.Features.CreateDoctor
{

    public record CreateDoctorRequest(DoctorDto DoctorDto);
    public record CreateDoctorResponse(Guid Id);
    public class CreateDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("doctors", async (CreateDoctorRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateDoctorCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateDoctorResponse>(); 
                return Results.Created($"/doctors/{response.Id}", response);
            })
            .WithDescription("Create doctor entity")
            .WithName("CreateDoctor")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreateDoctorResponse>(StatusCodes.Status201Created);
        }
    }
}
