
using Microsoft.AspNetCore.Builder;
using Shared.Data;
using Microsoft.EntityFrameworkCore;
using Authentication.Data.Constants;
using Duende.IdentityServer.Services;
using Authentication.Authentication;
using Authentication.Data.Configurations;
using Duende.IdentityServer.Test;

namespace Authentication
{
    public static class AuthenticationModule
    {

        public static IServiceCollection AddAuthenticationModule(this IServiceCollection services, IConfiguration  configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

            services.AddDbContext<AuthenticationDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                

            });
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                        .AddEntityFrameworkStores<AuthenticationDbContext>()
                        .AddDefaultTokenProviders();

            services.AddScoped<IProfileService, CustomProfileService>();



            services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients);



            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(DefaultRoles.AdminRole));
                options.AddPolicy("DoctorOnly", policy => policy.RequireRole(DefaultRoles.DoctorRole));
            });

            services.AddScoped<ISeedData, AuthenticationSeed>();

            return services;
        }

        public static IApplicationBuilder UseAuthenticationModule(this IApplicationBuilder app)
        {
            app.UseMigration<AuthenticationDbContext>();


            return app;
        }
    }
}
