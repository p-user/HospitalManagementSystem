
using Doctors.Contracts.Doctors.Dtos;
using Doctors.Contracts.Doctors.Features.CreateDoctor;
using Microsoft.AspNetCore.Mvc;

namespace Doctors.Doctors.Features.CreateDoctor
{
    public record CreateDoctorResponse(Guid Id);
    public class CreateDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/doctors", async ([FromBody] DoctorDto request, ISender sender) =>
            {
                var command = new CreateDoctorCommand(request);
                var result = await sender.Send(command);
                var response = result.Adapt<CreateDoctorResponse>();
                return Results.Created($"/doctors/{response.Id}", response);
            })
            .WithDescription("Create doctor entity")
            .WithName("CreateDoctor")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreateDoctorResponse>(StatusCodes.Status201Created);
            //.RequireAuthorization(); -> set adminOnly
        }
    }
}
