using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IProductManager
    {
        Task<List<ProductDto>> GetAllProductsWithItems();
        Task<List<RankingProduct>> GetMostSaleProductsByRange(DateTime start, DateTime end);
        Task<bool> InsertProduct(CreateProductDto productDto);
    }
}
