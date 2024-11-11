
using Microsoft.AspNetCore.Identity;

namespace Authentication.Authentication.Features.VerifyOTP
{
    public record VerifyOtpLoginCommand(string Email, string Password, string Otp) : IRequest<VerifyOtpLoginReponse>;
    public record VerifyOtpLoginReponse(string message);
    public class VerifyOTPHandler(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager, ISender sender) : IRequestHandler<VerifyOtpLoginCommand, VerifyOtpLoginReponse>
    {
        public async  Task<VerifyOtpLoginReponse> Handle(VerifyOtpLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException("User not found.");

            // Check OTP validation
            var isValidOtp = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "OTP", request.Otp);
            user.VerifyOtp();
            user.EmailConfirmed = true;
            user.PasswordHash =_userManager.PasswordHasher.HashPassword(user,request.Password);


            //update db
            var result = await _userManager.UpdateAsync(user);

            //set response
            string message = string.Empty;
            if (result.Succeeded)
            {
                message = $"OTP was verified! you can procceed to login";
            }
            else
            {

                message = $"OTP was not verified!";
            }

            return new VerifyOtpLoginReponse(message);

        }

        
    }
}
