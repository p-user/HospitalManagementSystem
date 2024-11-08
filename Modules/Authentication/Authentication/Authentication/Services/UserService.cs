using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Authentication.Services
{
    public class UserService : IUserService
    {
        public readonly AuthenticationDbContext authenticationDbContext;

        public UserService(AuthenticationDbContext authenticationDbContext)
        {
            this.authenticationDbContext = authenticationDbContext;
        }

        public Task<ClaimsIdentity?> GetRoleAsync(string user)
        {
            throw new NotImplementedException();
        }

        public Task GetUserAsync(ClaimsPrincipal subject)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await authenticationDbContext.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> ValidateUserAsync(string username, string password)
        {
            var user = await authenticationDbContext.Users.FirstOrDefaultAsync(u => u.Email == username);

            if (user == null)
            {
                return null; 
            }

            // Validate password hash
            if (!VerifyPassword(password, user.PasswordHash))
            {
                return null; 
            }

            return user;
        }

        private bool VerifyPassword(string password, string storedHash)
        {
         
            using (var sha256 = SHA256.Create())
            {
                var computedHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return computedHash == storedHash;
            }
        }
    }
}
