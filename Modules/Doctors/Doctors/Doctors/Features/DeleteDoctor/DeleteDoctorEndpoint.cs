
namespace Doctors.Doctors.Features.DeleteDoctor
{
    public record DeleteDoctorResponse(bool Succeeded);
    public class DeleteDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/doctors/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteDoctorCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteDoctorResponse>();
                return Results.Ok(response);
            })
                .WithDescription("Delete Doctor entity")
                .WithName("DeleteDoctor")
                .Produces<DeleteDoctorResponse>(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)           
                ;
        }
    }
}
