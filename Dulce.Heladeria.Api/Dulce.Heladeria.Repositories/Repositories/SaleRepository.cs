using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.Enums;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class SaleRepository: BaseRepository<SaleEntity>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext bd) : base(bd)
        {

        }

        public async Task<List<SaleEntity>> getAllSalesByDay(DateTime start) {
            return await BaseQuery.Where(sale => sale.Date.Day == start.Day && sale.Date.Month == start.Month && sale.Date.Year == start.Year).ToListAsync();
        }
    }
}
