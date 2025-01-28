using IdentityService.Api.Configuration;
using IdentityService.Domain.Contracts;
using IdentityService.Infra.Repositories;
using IdentityService.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        IConfiguration configuration)
    {
        var settings = configuration.GetSection("Storage:PostgreSQL").Get<PostgreSQLSettings>();
        var connectionString =
            $"Host={settings.Host};Username={settings.UserName};Password={settings.Password};Database={settings.Database}";

        serviceCollection
            .AddDbContext<ClientDbContext>(options =>
                options.UseNpgsql(connectionString));

        return serviceCollection;
    }
}