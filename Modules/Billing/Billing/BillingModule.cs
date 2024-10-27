using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Billing
{
    public static class BillingModule
    {

        public static IServiceCollection AddBillingModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IApplicationBuilder UseBillingModule (this IApplicationBuilder app) { return app; }
    }
}
