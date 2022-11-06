using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface IProductRepository: IBaseRepository<ProductEntity>, IPersistable<ProductEntity>
    {
        Task<List<MostSaleProduct>> getProducts(DateTime start, DateTime end);
    }
}
