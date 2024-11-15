using Doctors.Contracts.Doctors.Features.GetDoctorIdByEmail;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using System.Security.Claims;


namespace Authentication.Authentication.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISender _sender;

        public ProfileService(UserManager<ApplicationUser> userManager,ISender sender)
        {
            _userManager = userManager;
            _sender = sender;
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

            //add additional claims

            var claims = new List<Claim>
                {
                    
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                };

            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
                if (role == DefaultRoles.DoctorRole)
                {
                    var doctorId = await _sender.Send(new GetDoctorIdByEmailQuery(user.Email));
                    claims.Add(new Claim("DoctorId", doctorId.DoctorId));
                }

                else if(role == DefaultRoles.PatientRole)
                {
                    var patientId = await _sender.Send(new GetPatientIdByEmailQuery(user.Email));
                    claims.Add(new Claim("PatientId", patientId));
                }
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
