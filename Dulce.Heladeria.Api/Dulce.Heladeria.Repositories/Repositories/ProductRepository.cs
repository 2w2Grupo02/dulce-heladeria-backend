using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext bd) : base(bd)
        {

        }

        public async Task<List<MostSaleProduct>> getProducts(DateTime start, DateTime end)
        {
            var products = await _bd.SaleDetail.Include(x=> x.Product).Include(x => x.Sale).
                Where(x => x.Sale.Date >= start && x.Sale.Date <= end)
                .GroupBy(x => x.Product.Name)
                .Select(x => new MostSaleProduct
                {
                    Name = x.Key,
                    Cant = x.Select(x => x.Product.Id).Count(),
                    Total = x.Select(x => x.SalePrice * x.Amount).Sum()
                })
                .OrderBy(x => x.Total)
                .ThenBy(x => x.Cant)
                .ToListAsync();

            return products;
        }
    }

    public class MostSaleProduct { 
        public string Name { get; set; }
        public int Cant { get; set; }
        public double Total { get; set; }
    }
}
