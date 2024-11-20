using System;
using System.Threading.Tasks;
using SnackHub.ClientService.Domain.Entities;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Domain.Contracts;

public interface IClientRepository
{
    Task AddAsync(ClientModel client);

    Task<ClientModel?> GetClientByIdAsync(Guid id);
    Task<ClientModel?> GetClientByCpfAsync(Cpf cpf);
}