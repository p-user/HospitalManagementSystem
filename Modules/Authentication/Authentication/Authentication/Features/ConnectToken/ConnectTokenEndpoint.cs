

using Microsoft.AspNetCore.Http;

namespace Authentication.Authentication.Features.ConnectToken
{
    public class ConnectTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/connectToken", async (string email, string password, ISender sender) =>
            {
                var command = new ConnectTokenRequest(email, password);
                var response = await sender.Send(command);
                return Results.Ok(response);

            })
                .WithName("GetToken");
        }
    }
}