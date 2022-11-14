using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface IStockMovementRepository: IBaseRepository<StockMovementEntity>, IPersistable<StockMovementEntity>
    {
        public List<StockMovementEntity> getAllMovementsByDate(DateTime start, DateTime end);
        List<StockMovementEntity> getAllMovementsByItem(int itemStockId);
    }
}
