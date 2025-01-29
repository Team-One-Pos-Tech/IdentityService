using IdentityService.Application.Contracts;
using IdentityService.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Api.Extensions;

public static class AddUseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IGetClientUseCase, GetClientUseCase>()
            .AddScoped<ISignInUseCase, SignInUseCase>()
            .AddScoped<INotifyUserUseCase, NotifyUserUseCase>()
            .AddScoped<ISignUpUseCase, SignUpUseCase>();

        return serviceCollection;
    }
}