
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Exceptions;
using Shared.Services;
using System.Reflection;

namespace Authentication.Authentication.Features.GenerateOTP
{
    public record GenerateOTPCommand(string UserId) : IRequest<GenerateOTPResponse>;
    public record GenerateOTPResponse(bool Succeded);
    public class GenerateOTPCommandHandler(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager, IEmailService emailService) : IRequestHandler<GenerateOTPCommand, GenerateOTPResponse>
    {
        public async Task<GenerateOTPResponse> Handle(GenerateOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new NotFoundException("User not found");

            // Generate OTP using Microsoft Identity
            var otp = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "OTP");

            user.OtpExpiration = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);

            //send email with the password
            var emailText = $"Please Login in Hospital Management System with email {user.Email} and one-time-password :  {otp} ." +
                $"Please remember this password can only be used once" + $" and it it valid for only 15 min!";

            await emailService.SendEmail(toEmail: user.Email, subject: "Confirm Account", body: emailText, isBodyHTML: false);

            return new GenerateOTPResponse(true);
        }
    }
}