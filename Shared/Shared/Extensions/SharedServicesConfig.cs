using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared.Extensions
{
    public static  class SharedServicesConfig
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IClaimsService, ClaimsService>();
            return services;
        }
    }
}
