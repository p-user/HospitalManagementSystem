
namespace Doctors.Doctors.Features.UpdateSpecialization
{

    public record UpdateSpecializationRequest(SpecializationDto SpecializationDto, Guid Id);
    public record UpdateSpecializationResponse(Guid Id);
    public class UpdateSpecializationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/doctors/specializations", async (UpdateSpecializationRequest request, ISender sender) =>
            {
                var toSendObj = new UpdateSpecializationCommand(request.SpecializationDto, request.Id);
                var result = await sender.Send(toSendObj);
                return Results.Ok(new UpdateSpecializationResponse(result.Id));

            })
                .WithDescription("Update a specific specialization entity")
                .WithName("UpdateSpecialization")
                .Produces<UpdateSpecializationResponse>(StatusCodes.Status200OK)
                .Produces<UpdateSpecializationResponse>(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
