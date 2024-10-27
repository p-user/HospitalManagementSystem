using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Patients
{
    public static class PatientsModule
    {

        public static IServiceCollection AddPatientsModule (this IServiceCollection services , IConfiguration configuration)
        {
            return services;
        }

        public static IApplicationBuilder UsePatientsModule (this IApplicationBuilder app) 
        { 
            return app;
        }
    }
}
