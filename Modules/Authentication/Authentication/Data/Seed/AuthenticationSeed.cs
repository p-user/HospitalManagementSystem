
using Authentication.Data.Constants;
using Microsoft.AspNetCore.Identity;
using Shared.Data.Seed;

namespace Authentication.Data.Seed
{
    public class AuthenticationSeed(IServiceProvider serviceProvider) : ISeedData
    {
        public async Task SeedAsync()
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { DefaultRoles.AdminRole, DefaultRoles.DoctorRole, DefaultRoles.PatientRole};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var newRole = new ApplicationRole()
                    {
                        Name = roleName,

                    };
                    roleResult = await roleManager.CreateAsync(newRole);
                }
            }

                var admin = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                };

                string adminPassword = "Admin123!";
                var _user = await userManager.FindByEmailAsync("admin@admin.com");

                if (_user == null)
                {
                    var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                    if (createAdmin.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, DefaultRoles.AdminRole);
                    }
                }
            
        }
    }
}
