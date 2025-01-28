using System;
using System.Threading.Tasks;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Entities;
using IdentityService.Domain.ValueObjects;
using IdentityService.Infra.Repositories.Context;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infra.Repositories;

public class ClientRepository : BaseRepository<ClientModel, ClientDbContext>, IClientRepository
{
    public ClientRepository(ClientDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddAsync(ClientModel client)
    {
        await InsertAsync(client);
    }

    public async Task<ClientModel?> GetClientByIdAsync(Guid id)
    {
        return await FindByPredicateAsync(px => px.Id.Equals(id));
    }

    public async Task<ClientModel?> GetClientByCpfAsync(Cpf cpf)
    {
        return await FindByPredicateAsync(px => px.Cpf.Equals(cpf.Value));
    }
}