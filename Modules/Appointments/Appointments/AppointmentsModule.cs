using Appointments.Data;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;
using System.Reflection;



namespace Appointments
{
    public static class AppointmentsModule
    {

        public static IServiceCollection AddAppointmentsModule (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppointmentsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableInterceptor());

            });

            return services;
        }

        public  static IApplicationBuilder UseAppointmentsModule(this IApplicationBuilder app)
        {
            app.UseMigration<AppointmentsDbContext>();
            return app;
        }
    }
}
