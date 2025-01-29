using IdentityService.Domain.Contracts;
using IdentityService.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, JwtAuthService>();
        return serviceCollection;
    }
}