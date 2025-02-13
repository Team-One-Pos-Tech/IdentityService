﻿using System.Threading.Tasks;
using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models.Gateways;

namespace IdentityService.Application.UseCases;

public class SignInUseCase(IAuthService auth, IGetClientUseCase getClientUseCase) : ISignInUseCase
{
    public async Task<SignInResponse> Execute(SignInRequest request)
    {
        var client = await getClientUseCase.GetByCpf(request.Username);

        if (client == null)
        {
            var signInResponse = new SignInResponse(null);
            signInResponse.AddNotification("User", "User not found");
            return signInResponse;
        }

        request.UserId = client.Id;

        var response = await auth.Execute(request);

        return new SignInResponse(response.IdToken);
    }
}