using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Appointments
{
    public static class AppointmentsModule
    {

        public static IServiceCollection AddAppointmentsModule (this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public  static IApplicationBuilder UseAppointmentsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
