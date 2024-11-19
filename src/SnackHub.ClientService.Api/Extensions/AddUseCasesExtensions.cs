using Microsoft.Extensions.DependencyInjection;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.UseCases;

namespace SnackHub.ClientService.Api.Extensions;

public static class AddUseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IRegisterClientUseCase, RegisterClientUseCase>()
            .AddScoped<IGetClientUseCase, GetClientUseCase>()
            .AddScoped<ISignInUseCase, SignInUseCase>()
            .AddScoped<ISignUpUseCase, SignUpUseCase>();

        return serviceCollection;
    }
}