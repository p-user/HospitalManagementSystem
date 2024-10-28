using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Departments
{
    public static class DepartmentsModule
    {

        public static IServiceCollection AddDepartmentsModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IApplicationBuilder UseDepartmentsModule (this IApplicationBuilder app) { return app; }
    }
}
