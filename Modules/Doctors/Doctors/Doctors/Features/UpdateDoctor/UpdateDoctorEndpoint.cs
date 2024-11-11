

using Doctors.Contracts.Doctors.Dtos;

namespace Doctors.Doctors.Features.UpdateDoctor
{

    public record UpdateDoctorRequest(DoctorDto DoctorDto, Guid DoctorId);
    public record UpdateDoctorResponse(bool Succeeded);
    public class UpdateDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/doctors", async (UpdateDoctorRequest request, ISender sender) =>
            {
                var command = new UpdateDoctorCommand(request.DoctorDto, request.DoctorId);
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateDoctorResponse>();
                return Results.Ok(response);

            })
            .WithName("UpdateDoctor")
            .Produces<UpdateDoctorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Update doctor entity")
                ;
        }
    }
}
