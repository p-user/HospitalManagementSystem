using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patients.Data;
using Shared.Data;
using Shared.Data.Interceptors;
using System.Reflection;

namespace Patients
{
    public static class PatientsModule
    {

        public static IServiceCollection AddPatientsModule (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PatientsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableInterceptor());

            });
            return services;
        }

        public static IApplicationBuilder UsePatientsModule (this IApplicationBuilder app) 
        {
            app.UseMigration<PatientsDbContext>();
            return app;
        }
    }
}
