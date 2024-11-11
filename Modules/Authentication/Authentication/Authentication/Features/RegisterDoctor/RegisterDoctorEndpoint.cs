
using Authentication.Authentication.Dtos;
using Authentication.Authentication.Features.RegisterUser;
using Doctors.Contracts.Doctors.Dtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace Authentication.Authentication.Features.RegisterDoctor
{
    public record CreateDoctorRequest(DoctorDto DoctorDto);
    public record CreateDoctorResponse(string Message);
    public class RegisterDoctorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/admin/registerDoctor", async ([FromBody]DoctorDto request, ISender sender) =>
            {
                var appUser = new ApplicationUserDto { Email = request.Email , Role= DefaultRoles.DoctorRole};
                var command = new RegisterUserCommand(appUser);
                var result = await sender.Send(command);
                var response = result.Adapt<CreateDoctorResponse>();
                return Results.Created($"/admin", response);
            }).WithDescription("Create doctor entity")
            .WithName("CreateDoctor")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<CreateDoctorResponse>(StatusCodes.Status201Created);
            //.RequireAuthorization();
        }
    }
}
