using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class ItemStockRepository: BaseRepository<ItemStockEntity>, IItemStockRepository
    {
        public ItemStockRepository(ApplicationDbContext bd) : base(bd)
        {
        }

        public async Task<List<ItemStockEntity>> GetItemStock(int itemId)
        {
            List<ItemStockEntity> lista = await BaseQuery
                .Include(x => x.Location)
                .Include(x => x.Location).ThenInclude(x=>x.Deposit)
                .Include(x => x.Item)
                .Include(x => x.Item).ThenInclude(x => x.ItemType)
                .Where(x => x.ItemId == itemId)
                .ToListAsync();

            return lista;
        }
    }
}
