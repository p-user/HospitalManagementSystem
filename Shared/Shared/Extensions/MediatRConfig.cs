using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MediatRConfig
    {
        public static IServiceCollection AddMediatRDromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(config =>
             {
                foreach (var assembly in assemblies)
                { 
                    config.RegisterServicesFromAssembly(assembly);
                    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                }
            });
            return services;
        }
    }
}
