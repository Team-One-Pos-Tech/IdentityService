using System;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Domain.Contracts;

public interface IClientRepository
{
    Task AddAsync(ClientModel client);

    Task<ClientModel?> GetClientByIdAsync(Guid id);
    Task<ClientModel?> GetClientByCpfAsync(Cpf cpf);
}