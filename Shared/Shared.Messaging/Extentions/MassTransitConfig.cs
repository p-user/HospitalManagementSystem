using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging.Extentions
{
    public static  class MassTransitConfig
    {

        public static IServiceCollection AddMassTransitForAssemblies(this IServiceCollection services,IConfiguration configuration, params Assembly[] assemblies)
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


                //after configuring Rabbitmq


                //configure.UsingRabbitMq((context, configurator) =>
                //{
                //    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                //    {
                //        host.Username(configuration["MessageBroker:UserName"]!);
                //        host.Password(configuration["MessageBroker:Password"]!);
                //    });

                //    configurator.ConfigureEndpoints(context);

                //});

            });
            return services;
        }
    }
}
