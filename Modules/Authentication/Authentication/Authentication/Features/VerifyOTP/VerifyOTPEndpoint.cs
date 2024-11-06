
using Microsoft.AspNetCore.Http;

namespace Authentication.Authentication.Features.VerifyOTP
{
    public class VerifyOTPEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/verify-otp", async (string email, string password,string otp, ISender mediator) =>
            {
                var result = await mediator.Send(new VerifyOtpLoginCommand(email,password, otp));
                return Results.Ok(result);
            })
                .WithDescription("Verify Otp and sets new password")
                .WithName("VerifyOtp");
        }
    }
}
