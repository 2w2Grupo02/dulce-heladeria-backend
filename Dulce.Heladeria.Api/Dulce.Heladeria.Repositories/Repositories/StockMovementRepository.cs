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
    public class StockMovementRepository: BaseRepository<StockMovementEntity>, IStockMovementRepository
    {
        public StockMovementRepository(ApplicationDbContext bd) : base(bd)
        {

        }

        public List<StockMovementEntity> getAllMovementsByDate(DateTime start, DateTime end)
        {
            return BaseQuery.Where(x => x.MovementDate >= start && x.MovementDate <= end).ToList();
        }

        public List<StockMovementEntity> getAllMovementsByItem(int itemStockId)
        {
            return BaseQuery.Where(x => x.ItemStockId == itemStockId).ToList();
        }
    }
}
