using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface ISaleDetailRepository: IBaseRepository<SaleDetailEntity>, IPersistable<SaleDetailEntity>
    {
        Task<List<SaleDetailEntity>> GetSaleProductsByRange(DateTime start, DateTime end);
    }
}
