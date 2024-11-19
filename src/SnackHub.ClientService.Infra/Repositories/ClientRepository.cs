using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SnackHub.ClientService.Domain.Contracts;
using SnackHub.ClientService.Domain.Entities;
using SnackHub.ClientService.Domain.ValueObjects;
using SnackHub.ClientService.Infra.Repositories.Context;

namespace SnackHub.ClientService.Infra.Repositories;

public class ClientRepository : BaseRepository<Client, ClientDbContext>, IClientRepository
{
    protected ClientRepository(ClientDbContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
    {
    }

    public async Task AddAsync(Client client)
    {
        await InsertAsync(client);
    }

    public async Task<Client?> GetClientByIdAsync(Guid id)
    {
        return await FindByPredicateAsync(px => px.Id.Equals(id));
    }

    public async Task<Client?> GetClientByCpfAsync(Cpf cpf)
    {
        return await FindByPredicateAsync(px => px.Cpf.Equals(cpf));
    }
}