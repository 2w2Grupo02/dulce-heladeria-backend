using AutoMapper;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.Manager
{
    public class ItemStockManager : IItemStockManager
    {
        private readonly IItemStockRepository _itemStockRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemStockManager(IItemStockRepository itemStockRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _itemStockRepository = itemStockRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<List<ItemStockDto>> GetStockItem(int itemId)
        {
            var itemStockEntityList = await _itemStockRepository.GetAsync(x=>x.Item.Id == itemId);

            var itemStockDtoList = _mapper.Map<List<ItemStockDto>>(itemStockEntityList);

            return itemStockDtoList;
        }
    }
}
