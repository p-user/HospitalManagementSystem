using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class CarterConfig
    {

        public static IServiceCollection AddCarter(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                foreach (var assembly in assemblies)
                { 
                    var modules =assembly.GetTypes().Where(f => f.IsAssignableTo(typeof(ICarterModule))).ToArray();
                    config.WithModules(modules);
                }
            });
            return services;
        }
    }
}
