using IdentityService.Application.Contracts;
using IdentityService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Api.Extensions;

public static class ValidatorsExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRegisterClientValidator, RegisterClientValidator>();
    }
}