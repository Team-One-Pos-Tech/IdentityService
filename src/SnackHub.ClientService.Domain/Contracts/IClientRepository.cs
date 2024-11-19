using System;
using System.Threading.Tasks;
using SnackHub.ClientService.Domain.Entities;
using SnackHub.ClientService.Domain.ValueObjects;

namespace SnackHub.ClientService.Domain.Contracts
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);

        Task<Client?> GetClientByIdAsync(Guid id);
        Task<Client?> GetClientByCpfAsync(Cpf cpf);
    }
}