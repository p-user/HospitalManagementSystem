using Doctors.Contracts.Doctors.Features.GetDoctorById;
using Microsoft.AspNetCore.Mvc;

namespace Doctors.Doctors.Features.GetDoctorById
{

    public record GetDoctorByIdResponse(DoctorDto DoctorDto);
    public class GetDoctorByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/doctors/{id}", async ([FromRoute]Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetDoctorByIdQuery(id));
                var response = result.Adapt<GetDoctorByIdResponse>();
                return Results.Ok(response);
            }).WithDescription("Retrieve Doctor entity")
                .WithName("GetDoctorById")
                .Produces<GetDoctorByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
