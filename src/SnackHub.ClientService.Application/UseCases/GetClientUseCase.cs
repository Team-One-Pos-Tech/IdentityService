using System;
using System.Threading.Tasks;
using SnackHub.ClientService.Application.Contracts;
using SnackHub.ClientService.Application.Models;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Application.UseCases;

public class GetClientUseCase(IClientRepository clientRepository) : IGetClientUseCase
{
    public async Task<GetClientResponse?> Execute(Guid id)
    {
        var client = await clientRepository.GetClientByIdAsync(id);
        if (client is null)
            return null;

        var domainModel = client.ToDomainModel();

        return new GetClientResponse(domainModel.Name, domainModel.Cpf);
    }

    public async Task<GetClientResponse?> Execute(string cpf)
    {
        var cpfObject = new Cpf(cpf);
        if (!cpfObject.IsValid())
            return null; //Todo: Improve those validations!

        var client = await clientRepository.GetClientByCpfAsync(cpfObject);
        if (client is null)
            return null;

        var domainModel = client.ToDomainModel();

        return new GetClientResponse(domainModel.Name, domainModel.Cpf);
    }
}