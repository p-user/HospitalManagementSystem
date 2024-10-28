
using Shared.Pagination;

namespace Doctors.Doctors.Features.GetDoctors
{

    public record GetDoctorsResponse(PaginatedResult<DoctorDto> DoctorDtos);
    public class GetDoctorsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/doctors", async([AsParameters] PaginationRequest request,ISender sender) =>
            {
                var result  = await sender.Send( new GetDoctorsQuery(request));
                var response = result.Adapt<GetDoctorsResponse>();
                return Results.Ok(response);
            })
                .WithName("GetDoctors")
                .WithDescription("Get all doctor entities")
                .Produces<GetDoctorsResponse>(StatusCodes.Status200OK);
        }
    }
}
