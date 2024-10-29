namespace Doctors.Doctors.Features.DeleteSpecialization
{

    public record DeleteSpecializationResponse(bool Succeeded);
    public class DeleteSpecializationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/specializations/{id}", async (Guid id, ISender sender) =>
            {
                var toSendRequest = new DeleteSpecializationCommand(id);
                var result =await  sender.Send(toSendRequest);
                var response = result.Adapt<DeleteSpecializationResponse>();
                return Results.Ok(response);
            })
                .WithDescription("Delete specialization entity")
                .WithName("DeleteSpecialization")
                .Produces<DeleteSpecializationResponse>(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
