using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SnackHub.ClientService.Infra.Repositories.Abstractions;

public interface IBaseRepository<TModel> where TModel : class
{
    Task InsertAsync(TModel model);
    
    Task<TModel?> FindByPredicateAsync(Expression<Func<TModel, bool>> predicate);
    
}