using FrameUp.OrderService.Api.Configuration;
using IdentityService.Domain.Contracts;
using IdentityService.Infra.Repositories;
using IdentityService.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Api.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IClientRepository, ClientRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection,
        Settings settings)
    {
        serviceCollection
            .AddDbContext<ClientDbContext>(options =>
                options.UseNpgsql(settings.PostgreSQL.ConnectionString));

        return serviceCollection;
    }
}