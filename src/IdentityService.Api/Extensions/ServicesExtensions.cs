using FrameUp.OrderService.Api.Configuration;
using IdentityService.Domain.Contracts;
using IdentityService.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection, Settings settings)
    {
        serviceCollection.AddScoped<IAuthService, JwtAuthService>();

        AddEmailService(serviceCollection, settings);

        return serviceCollection;
    }

    private static void AddEmailService(IServiceCollection serviceCollection, Settings settings)
    {
        var emailSettings = settings.EmailService;

        serviceCollection.AddTransient<IEmailSender>(
            provider => new EmailSender(
                emailSettings.Host,
                emailSettings.Port,
                emailSettings.User,
                emailSettings.Pass
            ));
    }
}