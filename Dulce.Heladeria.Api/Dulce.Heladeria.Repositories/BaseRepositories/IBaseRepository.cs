using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asofar.Backend.Models.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity GetById(Int32 id);
        List<TEntity> GetAll();
        List<TEntity> Get(Expression<Func<TEntity, Boolean>> predicate);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, Boolean>> predicate);

    }
}
