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
    public class SaleDetailRepository : BaseRepository<SaleDetailEntity>, ISaleDetailRepository
    {
        public SaleDetailRepository(ApplicationDbContext bd) : base(bd)
        {
            
        }

        public async Task<List<SaleDetailEntity>> GetSaleProductsByRange(DateTime start, DateTime end)
        {
            var products = await BaseQuery.Include(x => x.Product).Include(x => x.Sale).
                Where(x => x.Sale.Date >= start && x.Sale.Date <= end)                
                .ToListAsync();

            return products;
        }
    }
}
