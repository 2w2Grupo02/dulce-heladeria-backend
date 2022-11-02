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
    public class ProductItemRepository : BaseRepository<ProductItemEntity>, IProductItemRepository
    {
        public ProductItemRepository(ApplicationDbContext bd) : base(bd)
        {

        }
        public async Task<List<ProductItemEntity>> GetAllProductItem()
        {
            List<ProductItemEntity> list = await BaseQuery
                .Include(x => x.Product)
                .ToListAsync();

            return list;
        }
    }
}
