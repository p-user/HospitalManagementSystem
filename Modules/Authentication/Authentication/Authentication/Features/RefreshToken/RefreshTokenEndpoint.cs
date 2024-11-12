
namespace Authentication.Authentication.Features.RefreshToken
{
    public class RefreshTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/refreshToken", async ([FromBody]string refreshToken, ISender sender) =>
            {
                var command = new RefreshTokenCommand(refreshToken);
                var response = await sender.Send(command);
                return Results.Ok(response);

            })
                .WithName("RefreshToken");
        }
    }
}