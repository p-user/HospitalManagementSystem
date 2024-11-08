using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using System.Security.Claims;


namespace Authentication.Authentication
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var claims = await GetClaimsAsync(context);
           context.IssuedClaims.AddRange(claims);

        }

        private async Task<List<Claim>> GetClaimsAsync(ProfileDataRequestContext context)
        {
            var test = context.Subject.Claims.ToList();
            var user = await _userManager.GetUserAsync(context.Subject);
            var roles = await _userManager.GetRolesAsync(user);

            //add additinal claims

            var claims = new List<Claim>
                {
                    new Claim("role", roles.FirstOrDefault()),
                    new Claim("email", user.Email),
                    new Claim("username", user.UserName),//todo: add department maybe??
                };
            return claims;
        }




        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

       
    }

}
