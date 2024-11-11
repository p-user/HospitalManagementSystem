
namespace Doctors.Doctors.Features.CreateSpecialization
{

    public record CreateSpecializationRequest(SpecializationDto SpecializationDto);
    public record CreateSpecializationResponse(Guid Id);
    public class CreateSpecializationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/admin/specializations", async (CreateSpecializationRequest request, ISender sender) =>
            {
                var toSendRequest = request.Adapt<CreateSpecializationCommand>();
                var result = await sender.Send(toSendRequest);
                var response = result.Adapt<CreateSpecializationResponse>();
                return Results.Ok(response);

            })
                .WithDescription("Create a doctor's field of specialization")
                .WithName("CreateSpecialization")
                .Produces<CreateSpecializationResponse>(StatusCodes.Status201Created)
                .Produces<CreateSpecializationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .RequireAuthorization();
        }
    }
}
