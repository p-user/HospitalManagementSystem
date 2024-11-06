using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using System.Security.Claims;


namespace Authentication.Authentication
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var roles = await _userManager.GetRolesAsync(user);

          
            var claims = new List<Claim>
        {
            new Claim("role", roles.FirstOrDefault()),
            new Claim("email", user.Email)
        };

            context.AddRequestedClaims(claims);
        }

       
        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

       
    }

}
