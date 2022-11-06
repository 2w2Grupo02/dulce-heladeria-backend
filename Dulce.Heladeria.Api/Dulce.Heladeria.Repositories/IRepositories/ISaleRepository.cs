using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface ISaleRepository : IBaseRepository<SaleEntity>, IPersistable<SaleEntity>
    {
        Task<List<SaleEntity>> GetAllSalesWithClients();
        public Task<List<SaleEntity>> getAllSalesByDay(DateTime start);
    }
}
