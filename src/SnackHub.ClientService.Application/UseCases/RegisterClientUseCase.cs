using System;
using System.Threading.Tasks;
using MassTransit;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Domain.Entities;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Application.UseCases;

public class RegisterClientUseCase(
    IClientRepository clientRepository,
    IRegisterClientValidator validator,
    IPublishEndpoint publishEndpoint) : IRegisterClientUseCase
{
    public async Task<RegisterClientResponse> Execute(RegisterClientRequest registerClientRequest)
    {
        var response = new RegisterClientResponse();

        var isValid = await validator.IsValid(registerClientRequest, response);

        if (!isValid) return response;

        var client = CreateClient(registerClientRequest);

        await clientRepository.AddAsync(client);

        response.Id = client.Id;

        await PublishEventMessage(client);

        return response;
    }

    private static Client CreateClient(RegisterClientRequest registerClientRequest)
    {
        return new Client(
            Guid.NewGuid(),
            registerClientRequest.Name,
            new Cpf(registerClientRequest.Cpf),
            registerClientRequest.Email
        );
    }

    private async Task PublishEventMessage(Client client)
    {
        var eventMessage = new ClientCreated(client.Id, client.Name, client.Cpf.Value, client.Email);
        await publishEndpoint.Publish(eventMessage);
    }
}