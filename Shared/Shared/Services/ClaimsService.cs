
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        }

        public string GetCurrentUserEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }


        public IDictionary<string, string> GetUserClaims()
        {
            var claims = new Dictionary<string, string>();

            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated ?? false)
            {
                foreach (var claim in user.Claims)
                {
                    claims[claim.Type] = claim.Value;
                }
            }

            return claims;
        }
    }
}
