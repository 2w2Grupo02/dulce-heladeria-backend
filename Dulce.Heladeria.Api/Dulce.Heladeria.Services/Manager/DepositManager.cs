using AutoMapper;
using Dulce.Heladeria.Models.Entities;
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
    public class DepositManager : IDepositManager
    {
        private readonly IDepositRepository _depositRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepositManager(IDepositRepository depositRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _depositRepository = depositRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetDepositsDto>> GetAllDeposits()
        {
            List<DepositEntity> depositEntityList = await _depositRepository.GetAllAsync();

            var depositDtoList = _mapper.Map<List<GetDepositsDto>>(depositEntityList);

            foreach (var item in depositDtoList)
            {
                //var itemStockEntityList = await _itemStockRepository.GetItemStock(item.Id);
                //item.Capacity = itemStockEntityList.Sum(x => x.Amount);
            }

            return depositDtoList;
        }
    }
}
