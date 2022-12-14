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

        public async Task<List<ProductDto>> GetProductsWithAvailableItems()
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

                if(items.Count > 0)
                {
                    var productEntity = product.First().Product;
                    var productDto = new ProductDto()
                    {
                        Id = product.Key,
                        Name = productEntity.Name,
                        Price = productEntity.ListPrice,
                        MaxItemAmount = productEntity.MaxItemAmount,
                        ImageUrl = productEntity.ImageUrl,
                        Items = items
                    };

                    productsDtoList.Add(productDto);
                }
            }

            return productsDtoList;
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

        public async Task<List<ProductDto>> GetProductsWithItems()
        {
            var productEntityList = await _productRepository.GetAllAsync();
            var productDtoList = _mapper.Map<List<ProductDto>>(productEntityList);
            foreach (var productDto in productDtoList)
            {
                var items = new List<ProductItemDto>();
                var productItemList = await _productItemRepository.GetAsync(x => x.ProductId == productDto.Id && x.DeletionDate == null);
                foreach (var item in productItemList)
                {
                    var itemEntity = await _itemRepository.GetBy(x => x.Id == item.ItemId);
                    if (itemEntity == null)
                    {
                        throw new InvalidOperationException($"No existe el articulo {item.ItemId}");
                    }

                    items.Add(new ProductItemDto() { Id = item.ItemId, Name = itemEntity.Name });
                }

                productDto.Items = items;
            }

            return productDtoList;
        }

        public async Task<bool> UpdateProduct(int productId, CreateProductDto productDto)
        {           
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var productEntity = await _productRepository.GetById(productId);

                    if (productEntity == null)
                    {
                        throw new InvalidOperationException($"No existe el producto {productDto.Name}");
                    }

                    productEntity.Name = productDto.Name;
                    productEntity.ListPrice = productDto.Price;
                    productEntity.MaxItemAmount = productDto.MaxItemAmount;
                    productEntity.ImageUrl = productDto.ImageUrl;
                    await _productRepository.UpdateAsync(productEntity);

                    var productItemList = await _productItemRepository.GetAsync(x => x.ProductId == productId);

                    foreach (var item in productDto.Items)
                    {
                        var productItem = productItemList.Where(x => x.ItemId == item.Id).FirstOrDefault();

                        if (productItem == null)
                        {
                            var newProductItem = new ProductItemEntity() { ItemId = item.Id, ProductId = productId };
                            await _productItemRepository.InsertAsync(newProductItem);
                        }
                        else if(productItem.DeletionDate != null)
                        {
                            productItem.DeletionDate = null;
                            await _productItemRepository.UpdateAsync(productItem);
                        }
                    }

                    foreach (var productItem in productItemList)
                    {
                        if (productItem.DeletionDate == null && productDto.Items.Where(x => x.Id == productItem.ItemId).FirstOrDefault() == null)
                        {
                            productItem.DeletionDate = DateTime.Now;
                            await _productItemRepository.UpdateAsync(productItem);
                        }
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

        public async Task<CreateProductDto> GetProductById(int productId)
        {
            var productEntity = await _productRepository.GetById(productId);
            var productDto = _mapper.Map<CreateProductDto> (productEntity);
            
            var items = new List<ProductItemDto>();
            var productItemList = await _productItemRepository.GetAsync(x => x.ProductId == productId && x.DeletionDate == null);
            foreach (var item in productItemList)
            {
                var itemEntity = await _itemRepository.GetBy(x => x.Id == item.ItemId);
                if (itemEntity == null)
                {
                    throw new InvalidOperationException($"No existe el articulo {item.ItemId}");
                }

                items.Add(new ProductItemDto() { Id = item.ItemId, Name = itemEntity.Name });
            }
            productDto.Items = items;

            return productDto;
        }
    }
}
