using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IProductManager
    {
        Task<List<ProductDto>> GetProductsWithAvailableItems();
        Task<List<ProductDto>> GetProductsWithItems();
        Task<bool> InsertProduct(CreateProductDto productDto);
        Task<bool> UpdateProduct(int productId, CreateProductDto productDto);
        Task<CreateProductDto> GetProductById(int productId);
    }
}
