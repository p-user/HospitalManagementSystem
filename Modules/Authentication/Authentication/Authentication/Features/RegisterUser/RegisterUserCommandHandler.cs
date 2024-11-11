using Authentication.Authentication.Dtos;
using Authentication.Authentication.Features.GenerateOTP;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Authentication.Features.RegisterUser
{
    public record RegisterUserCommand(ApplicationUserDto ApplicationUserDto) : IRequest<RegisterUserCommandResponse>;
    public record RegisterUserCommandResponse(bool Succeded);
    public class RegisterUserCommandHandler(UserManager<ApplicationUser> _userManager, ISender sender) : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        public async  Task<RegisterUserCommandResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = CreateUser(request.ApplicationUserDto.Email, request.ApplicationUserDto.Password, request.ApplicationUserDto.UserName);

            //check if user exists
            var existingUser = await _userManager.FindByEmailAsync(appUser.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            //create user
            var result = await _userManager.CreateAsync(appUser);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.FirstOrDefault().Description);
            }

            //assign to role
            await _userManager.AddToRoleAsync(appUser, request.ApplicationUserDto.Role);

            //create otp and send email
            var otpResponse = await sender.Send(new GenerateOTPCommand(appUser.Id));

            return new RegisterUserCommandResponse(true);

        }

        private ApplicationUser CreateUser(string email, string passwod, string username)
        {
            return ApplicationUser.CreateUser(email, passwod, username);
        }
    }
}
