using System;
using System.Threading.Tasks;
using MassTransit;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Domain.Entities;
using SnackHub.ClientService.Domain.Models.Gateways;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Application.UseCases;

public class SignUpUseCase : ISignUpUseCase
{
    private readonly IClientRepository _clientRepository;
    private readonly IRegisterClientValidator _validator;
    private readonly IPublishEndpoint _publishEndpoint;


    public SignUpUseCase(
        IClientRepository clientRepository, 
        IRegisterClientValidator validator, 
        IPublishEndpoint publishEndpoint)
    {
        _clientRepository = clientRepository;
        _validator = validator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<RegisterClientResponse> Execute(SignUpRequest request)
    {
        var registerClientRequest = new RegisterClientRequest(request.Name, request.Username, request.Email);

        var response = new RegisterClientResponse();
        if (await _validator.IsValid(registerClientRequest, response) == false)
            return response;

        var client = CreateClient(registerClientRequest);
        await _clientRepository.AddAsync(client.ToDatabaseModel());

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
        await _publishEndpoint.Publish(eventMessage);
    }
}