using System;
using System.Threading.Tasks;
using IdentityService.Application.Contracts;
using IdentityService.Application.Models;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Application.UseCases;

public class GetClientUseCase(IClientRepository clientRepository) : IGetClientUseCase
{
    public async Task<GetClientResponse?> GetById(Guid id)
    {
        var client = await clientRepository.GetClientByIdAsync(id);
        if (client is null)
            return null;

        var domainModel = client.ToDomainModel();

        var response = new GetClientResponse(domainModel.Name, domainModel.Cpf)
        {
            Email = domainModel.Email,
            Id = domainModel.Id
        };

        return response;
    }

    public async Task<GetClientResponse?> GetByCpf(string cpf)
    {
        var cpfObject = new Cpf(cpf);
        if (!cpfObject.IsValid())
            return null; //Todo: Improve those validations!

        var client = await clientRepository.GetClientByCpfAsync(cpfObject);
        if (client is null)
            return null;

        var domainModel = client.ToDomainModel();

       
        var response = new GetClientResponse(domainModel.Name, domainModel.Cpf)
        {
            Email = domainModel.Email,
            Id = domainModel.Id
        };

        return response;
    }
}