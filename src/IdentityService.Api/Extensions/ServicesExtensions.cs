using IdentityService.Api.Configuration;
using IdentityService.Domain.Contracts;
using IdentityService.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IAuthService, JwtAuthService>();

        AddEmailService(serviceCollection, configuration);

        return serviceCollection;
    }

    private static void AddEmailService(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection("EmailServiceSettings").Get<EmailServiceSettings>()!;

        serviceCollection.AddTransient<IEmailSender>(
            provider => new EmailSender(
                emailSettings.Host,
                emailSettings.Port,
                emailSettings.User,
                emailSettings.Pass
            ));
    }
}