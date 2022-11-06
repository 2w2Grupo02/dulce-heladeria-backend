using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class SaleRepository: BaseRepository<SaleEntity>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext bd) : base(bd)
        {

        }
        public async Task<List<SaleEntity>> GetAllSalesWithClients()
        {
            List<SaleEntity> saleEntities = await BaseQuery.Include(x => x.Client).ToListAsync();

            return saleEntities;
        }
    }
}
