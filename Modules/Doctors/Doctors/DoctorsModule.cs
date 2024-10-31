using Doctors.Data;
using Doctors.Data.Seed;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;
using Shared.Data.Seed;
using System;


namespace Doctors
{
    public static class DoctorsModule
    {

        public static IServiceCollection AddDoctorsModule(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

            services.AddDbContext<DoctorsDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());

            });

            services.AddScoped<ISeedData, DoctorsSeed>();
            return services;
        }

        public static IApplicationBuilder UseDoctorsModule (this IApplicationBuilder app)
        {
            app.UseMigration<DoctorsDbContext>();

         
            return app;
        }

       
    }
}
