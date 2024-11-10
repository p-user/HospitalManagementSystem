using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using System.Security.Claims;


namespace Authentication.Authentication.Services
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
            var userId = context.Subject.FindFirst(JwtClaimTypes.Subject)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            //add additinal claims

            var claims = new List<Claim>
                {
                    
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                };

            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }
            return claims;
        }




        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

       
    }

}
