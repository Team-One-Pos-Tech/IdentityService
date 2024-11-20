using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnackHub.ClientService.Api.Configuration;

namespace SnackHub.ClientService.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var settings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();

        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("client-service"));

            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(settings.Host, "/", rabbitMqHostConfigurator =>
                {
                    rabbitMqHostConfigurator.Username(settings.UserName);
                    rabbitMqHostConfigurator.Password(settings.Password);
                });

                configurator.AutoDelete = true;
                configurator.ConfigureEndpoints(context);
            });
        });

        return serviceCollection;
    }
}