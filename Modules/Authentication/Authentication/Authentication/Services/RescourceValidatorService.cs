
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using IdentityModel;
using System.Security.Claims;

namespace Authentication.Authentication.Services
{
    public class RescourceValidatorService : IResourceOwnerPasswordValidator
    {
        private readonly IUserService _userService;

        public RescourceValidatorService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
           var user = await _userService.ValidateUserAsync(context.UserName, context.Password);

            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "password",
                    claims: new List<Claim> {
                        new Claim(
                            JwtClaimTypes.Email, user.UserName
                            ) 
                    });
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
    }
}
