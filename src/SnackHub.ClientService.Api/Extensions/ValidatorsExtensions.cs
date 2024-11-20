using Microsoft.Extensions.DependencyInjection;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.UseCases;

namespace SnackHub.ClientService.Api.Extensions;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRegisterClientValidator, RegisterClientValidator>();
    }
}