﻿using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductManager(
            IProductItemRepository productItemRepository, 
            IItemStockRepository itemStockRepository, 
            IProductRepository productRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _productItemRepository = productItemRepository;
            _itemStockRepository = itemStockRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
