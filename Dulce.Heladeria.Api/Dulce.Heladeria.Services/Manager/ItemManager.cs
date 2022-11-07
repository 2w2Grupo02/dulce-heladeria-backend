using AutoMapper;
using Dulce.Heladeria.DataAccess.Data;
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
    public class ItemManager : IItemManager
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemStockRepository _itemStockRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemManager(IItemRepository itemRepository, IItemStockRepository itemStockRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _itemStockRepository = itemStockRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<bool> InsertItem(ItemDto item)
        {
            var itemEntity = _mapper.Map<ItemEntity>(item);

            await _itemRepository.InsertAsync(itemEntity);
            var resultsave = await _unitOfWork.SaveChangesAsync();

            return resultsave >= 1 ? true : false;
        }

        public async Task<List<GetItemsDto>> GetAllItems()
        {
            List<ItemEntity> itemEntityList = await _itemRepository.GetAllItems();

            var itemDtoList = _mapper.Map<List<GetItemsDto>>(itemEntityList);

            foreach (var item in itemDtoList)
            {
                var itemStockEntityList = await _itemStockRepository.GetItemStock(item.Id);
                item.Amount = itemStockEntityList.Sum(x => x.Amount);
            }

            return itemDtoList;
        }

        public async Task<bool> UpdateItem(int id, ItemDto item)
        {
            ItemEntity itemEntity = await _itemRepository.GetById(id);

            itemEntity.Name = item.Name;
            itemEntity.Description = item.Description;
            itemEntity.ItemTypeId = item.ItemTypeId;
            itemEntity.MeasuringType = item.MeasuringType;

            await _itemRepository.UpdateAsync(itemEntity);
            var resultsave = await _unitOfWork.SaveChangesAsync();

            return resultsave >= 1 ? true : false;
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            var productEntity = await _itemRepository.GetById(id);
            var productDto = _mapper.Map<ItemDto>(productEntity);            

            return productDto;
        }
    }
}
