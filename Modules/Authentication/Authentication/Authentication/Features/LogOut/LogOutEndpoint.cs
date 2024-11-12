namespace Authentication.Authentication.Features.LogOut
{
    public class LogOutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/logout", async (string refreshToken, ISender sender) =>
            {
                var command = new LogOutCommand(refreshToken);
                var response = await sender.Send(command);
                return Results.Ok(response);

            })
                .WithName("LogOut");
        }
    }
}