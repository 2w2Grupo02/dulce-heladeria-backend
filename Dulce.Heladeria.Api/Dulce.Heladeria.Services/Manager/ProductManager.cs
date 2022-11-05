using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.Manager
{
    public class ProductManager: IProductManager
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IItemStockRepository _itemStockRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IMapper _mapper;
        public ProductManager(
            IProductItemRepository productItemRepository, 
            IItemStockRepository itemStockRepository,
            IProductRepository productRepository,
            ISaleRepository saleRepository,
            ISaleDetailRepository saleDetailRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IItemRepository itemRepository)
        {
            _productItemRepository = productItemRepository;
            _itemStockRepository = itemStockRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
        }

        public async Task<List<ProductDto>> GetAllProductsWithItems()
        {
            List<ProductItemEntity> productItemEntityList = await _productItemRepository.GetAllProductItem();

            var products = productItemEntityList.GroupBy(x => x.ProductId).ToList();

            var productsDtoList = new List<ProductDto>();

            foreach (var product in products)
            {
                var items = new List<ProductItemDto>();                

                foreach (var item in product)
                {
                    var itemStocks = await _itemStockRepository.GetItemStock(item.ItemId);                    
                    bool disponibility = itemStocks.Where(x => x.Amount > 0).Count() > 0;
                    if (disponibility)
                    {
                        items.Add(new ProductItemDto() { Id = item.ItemId, Name = itemStocks.First().Item.Name });
                    }
                }

                var productEntity = product.First().Product;
                var productDto = new ProductDto()
                {
                    Id = product.Key,
                    Name = productEntity.Name,
                    Price = productEntity.ListPrice,
                    MaxItemAmount = productEntity.MaxItemAmount,
                    Items = items
                };

                productsDtoList.Add(productDto);
            }

            return productsDtoList;
        }

        public async Task<List<RankingProduct>> GetMostSaleProductsByRange(DateTime start, DateTime end)
        {
           //Obtengo todas la ventas
           var sales = await _saleRepository.GetAllAsync();

            //filtro por fechas
            var salesFiltered = sales
                .Where(sale => sale.Date <= end && sale.Date >= start);

            //hago una lista de detalles
            var salesDetails = new List<SaleDetailEntity>();

            //itero en todas las ventas para traer sus detalles y lo almaceno el la lista de detalles
            foreach (var sale in salesFiltered) {
                var details = await _saleDetailRepository.GetAsync(detalle => detalle.SaleId == sale.Id);
                foreach (var d in details) {
                    salesDetails.Add(d); 
                }
            }

            var salesDetailsGroupByProduct = salesDetails.GroupBy(x => x.ProductId);
            return null;
        }

        public async Task<bool> InsertProduct(CreateProductDto productDto)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var productEntity = _mapper.Map<ProductEntity>(productDto);

                    var productId = await _productRepository.SaveAsync(productEntity);

                    foreach (var productItem in productDto.Items)
                    {
                        if (await _itemRepository.GetBy(x => x.Id == productItem.Id) == null)
                        {
                            throw new InvalidOperationException($"No existe el articulo {productItem.Name}");
                        }

                        var productItemEntity = _mapper.Map<ProductItemEntity>(productItem);
                        productItemEntity.ProductId = productEntity.Id;
                        await _productItemRepository.InsertAsync(productItemEntity);
                    }

                    var resultsave = await _unitOfWork.SaveChangesAsync();

                    if (resultsave >= 1)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
               
        }
    }
}
