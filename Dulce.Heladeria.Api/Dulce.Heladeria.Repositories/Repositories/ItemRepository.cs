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
    public class ItemRepository: BaseRepository<ItemEntity>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext bd): base(bd)
        {
        }

        public async Task<List<ItemEntity>> GetAllItems()
        {
            List<ItemEntity> itemEntities = await BaseQuery.Include(x => x.ItemType).ToListAsync();

            return itemEntities;
        }
    }
}
