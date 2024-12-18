﻿
using Shared.Data;
using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.Services;
using Authentication.Authentication.Services;
using Duende.IdentityServer.Validation;
using Authentication.ServerConfiguration;
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



            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IResourceOwnerPasswordValidator, RescourceValidatorService>();
            services.AddScoped<IPasswordHasher<ApplicationUser>, Sha256PasswordHasher<ApplicationUser>>();

            services.AddIdentityServer(options =>
            {
                options.IssuerUri = configuration["IdentityServer"];
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;

            }).AddInMemoryClients(Config.Clients)
              .AddInMemoryApiScopes(Config.ApiScopes)
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
              .AddProfileService<ProfileService>()
              .AddDeveloperSigningCredential();

          

            services.AddOpenIdConnectAccessTokenManagement();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AdminOnly, policy => policy.RequireRole(DefaultRoles.AdminRole));
                options.AddPolicy(Policies.DoctorOnly, policy => policy.RequireRole(DefaultRoles.DoctorRole));
                options.AddPolicy(Policies.DoctorOrAdminOnly, policy =>
                {
                    policy.RequireAssertion(context =>
                                   context.User.IsInRole(DefaultRoles.AdminRole) ||
                                   context.User.IsInRole(DefaultRoles.DoctorRole));
                });
            });

            services.AddScoped<ISeedData, AuthenticationSeed>();

            return services;
        }

        public static IApplicationBuilder UseAuthenticationModule(this IApplicationBuilder app)
        {
            app.UseMigration<AuthenticationDbContext>();
            app.UseIdentityServer();


            return app;
        }
    }
}
