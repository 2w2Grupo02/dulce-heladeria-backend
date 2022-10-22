using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface IItemStockRepository : IBaseRepository<ItemStockEntity>, IPersistable<ItemStockEntity>
    {
        public Task<List<ItemStockEntity>> GetItemStock(int itemId);
    }
}
