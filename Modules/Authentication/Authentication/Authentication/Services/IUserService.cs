using System.Security.Claims;

namespace Authentication.Authentication.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> ValidateUserAsync(string username, string password);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task GetUserAsync(ClaimsPrincipal subject);
        Task<ClaimsIdentity?> GetRoleAsync(string user);
    }
}