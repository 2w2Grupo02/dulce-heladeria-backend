using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asofar.Backend.Models.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetById(Int32 id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllActivesAsync();
        List<TEntity> Get(Expression<Func<TEntity, Boolean>> predicate);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, Boolean>> predicate);

    }
}
