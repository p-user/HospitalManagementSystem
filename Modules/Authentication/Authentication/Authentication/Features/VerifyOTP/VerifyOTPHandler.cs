

using Authentication.Authentication.Features.ConnectToken;

namespace Authentication.Authentication.Features.VerifyOTP
{
    public record VerifyOtpLoginCommand(string Email, string Password, string Otp) : IRequest<ConnectTokenResponse>;
    public class VerifyOTPHandler(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager, ISender sender) : IRequestHandler<VerifyOtpLoginCommand, ConnectTokenResponse>
    {
        public async  Task<ConnectTokenResponse> Handle(VerifyOtpLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new NotFoundException("User not found.");

            // Check OTP validation
            var isValidOtp = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "OTP", request.Otp);
            if (!isValidOtp || user.OtpExpiration < DateTime.UtcNow)
            {
                throw new Exception("OTP not valid!");
            }
            var test = await _userManager.AddPasswordAsync(user, request.Password);
            user.IsOtpVerified = true;
            await _userManager.UpdateAsync(user);


          
            //get token
            var tokenResponse = await sender.Send(new ConnectTokenRequest(request.Email, request.Password));
            return tokenResponse;
        }

        
    }
}
