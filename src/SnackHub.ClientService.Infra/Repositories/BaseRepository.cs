using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SnackHub.ClientService.Infra.Extensions;
using SnackHub.ClientService.Infra.Repositories.Abstractions;

namespace SnackHub.ClientService.Infra.Repositories;

public class BaseRepository<TModel, TDbContext> : IBaseRepository<TModel>
    where TModel : class
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private readonly DbSet<TModel> _dbSet;

    protected HashSet<string> _expandProperties = [];

    protected BaseRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TModel>();
    }

    public async Task InsertAsync(TModel model)
    {
        await _dbSet.AddAsync(model);
        await CompleteAsync();
    }

    public async Task<TModel?> FindByPredicateAsync(Expression<Func<TModel, bool>> predicate)
    {
        return await _dbSet
            .AsNoTracking()
            .Inflate(_expandProperties)
            .Where(predicate)
            .FirstOrDefaultAsync();
    }

    private async Task CompleteAsync()
    {
        //TODO: Move it to a better context {UnitOfWork or Transactions based}
        await _dbContext.SaveChangesAsync();
    }
}