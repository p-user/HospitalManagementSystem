using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Patients.Patients.Dtos;

namespace Patients.Patients.Features.CreatePatient
{
    public record CreatePatientResponse(Guid Id);
    public class CreatePatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/patients", async ([FromBody] PatientDto request, ISender sender) =>
            {
                var command = new CreatePatientCommand(request);
                var result = await sender.Send(command);
                var response = result.Adapt<CreatePatientResponse>();
                return Results.Created($"/doctors/{response.Id}", response);
            })
            .WithDescription("Create patient entity")
            .WithName("CreatePatient")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreatePatientResponse>(StatusCodes.Status201Created);
            
        }
    }
}
