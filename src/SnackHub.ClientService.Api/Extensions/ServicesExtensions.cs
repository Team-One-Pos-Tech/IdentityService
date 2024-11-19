using Microsoft.Extensions.DependencyInjection;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Infra.Services;

namespace SnackHub.ClientService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, JwtAuthService>();
        return serviceCollection;
    }
}