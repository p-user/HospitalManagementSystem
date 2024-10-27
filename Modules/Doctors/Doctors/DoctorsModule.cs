using Doctors.Data;
using Doctors.Data.Seed;
using Microsoft.AspNetCore.Builder;
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

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DoctorsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableInterceptor());

            });

            services.AddScoped<ISeedData, DoctorsSeed>();
            return services;
        }

        public static IApplicationBuilder UseDoctorsModule (this IApplicationBuilder app)
        {
            //init db
            app.UseMigration<DoctorsDbContext>();

           // InitializeDbAsync(app).GetAwaiter().GetResult();    
            return app;
        }

        //private static async Task InitializeDbAsync (IApplicationBuilder app)
        //{

            

        //}
    }
}
