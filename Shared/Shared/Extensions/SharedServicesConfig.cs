using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static  class SharedServicesConfig
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
