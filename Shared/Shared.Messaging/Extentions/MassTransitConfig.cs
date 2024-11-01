using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging.Extentions
{
    public static  class MassTransitConfig
    {

        public static IServiceCollection AddMassTransitForAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

                configure.SetInMemorySagaRepositoryProvider();
                configure.AddConsumers(assemblies);
                configure.AddSagaStateMachines(assemblies);
                configure.AddSagas(assemblies);
                configure.AddActivities(assemblies);

                configure.UsingInMemory((context, configurator) =>
                {
                    configurator.ConfigureEndpoints(context);
                });

            });
            return services;
        }
    }
}
