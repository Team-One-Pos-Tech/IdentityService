using FrameUp.OrderService.Api.Configuration;
using IdentityService.Api.Configuration;
using IdentityService.Api.Extensions;
using IdentityService.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityService.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection serviceCollection, Settings settings)
    {
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("identity-service"));

            busConfigurator.AddConsumer<NotifyUpdateOrderConsumer>();

            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(settings.RabbitMQ.ConnectionString));

                configurator.AutoDelete = true;
                configurator.ConfigureEndpoints(context);
            });
        });

        return serviceCollection;
    }
}