using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authentication.Data
{
    public class AuthenticationDbContext : IdentityDbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
        public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Authentication");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
           
        }
    }
}
