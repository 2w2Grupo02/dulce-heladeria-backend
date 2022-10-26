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
    public class DepositManager : IDepositManager
    {
        private readonly IDepositRepository _depositRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepositManager(IDepositRepository depositRepository, ILocationRepository locationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _depositRepository = depositRepository;
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetDepositsDto>> GetAllDeposits()
        {
            List<DepositEntity> depositEntityList = await _depositRepository.GetAllAsync();

            var depositDtoList = _mapper.Map<List<GetDepositsDto>>(depositEntityList);

            foreach (var deposit in depositDtoList)
            {
                var itemStockEntityList = await _locationRepository.GetAsync(x => x.DepositId == deposit.Id);
                deposit.Capacity = itemStockEntityList.Sum(x => x.Capacity);
            }

            return depositDtoList;
        }
    }
}
