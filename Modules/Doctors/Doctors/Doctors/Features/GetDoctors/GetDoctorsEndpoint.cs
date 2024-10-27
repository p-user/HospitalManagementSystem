
namespace Doctors.Doctors.Features.GetDoctors
{

    public record GetDoctorsResponse(List<DoctorDto> DoctorDtos);
    public class GetDoctorsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/doctors", async (ISender sender) =>
            {
                var result  = await sender.Send( new GetDoctorsQuery());
                var response = result.Adapt<GetDoctorsResponse>();
                return Results.Ok(response);
            })
                .WithName("GetDoctors")
                .WithDescription("Get all doctor entities")
                .Produces<GetDoctorsResponse>(StatusCodes.Status200OK);
        }
    }
}
