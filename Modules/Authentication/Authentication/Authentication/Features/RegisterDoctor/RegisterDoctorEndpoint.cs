
using Carter;
using Doctors.Doctors.Dtos;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Authentication.Authentication.Features.RegisterDoctor
{
    public record CreateDoctorRequest(DoctorDto DoctorDto);
    public record CreateDoctorResponse(bool Succeded);
    public class RegisterDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/admin/registerDoctor", async ([FromBody]DoctorDto request, ISender sender) =>
            {
                //var command = request.Adapt<RegisterDoctorDto>();
                var command = new RegisterDoctorDto(request);
                var result = await sender.Send(command);
                var response = result.Adapt<RegisterDoctorResponse>();
                return Results.Created($"/admin", response);
            }).WithDescription("Create doctor entity")
            .WithName("CreateDoctor")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreateDoctorResponse>(StatusCodes.Status201Created);
            //.RequireAuthorization();
        }
    }
}
