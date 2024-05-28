using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace BuildingBlocks.Messaging.MassTransit;
public static class Extentions
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        //
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), Host =>
                {
                    Host.Username(configuration["MessageBroker:UserName"]);
                    Host.Password(configuration["MessageBroker:Password"]);
                });
                configurator.ConfigureEndpoints(context);
            });

        });
        return services;
    }
}