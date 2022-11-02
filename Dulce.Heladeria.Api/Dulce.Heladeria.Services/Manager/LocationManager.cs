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
    public class LocationManager : ILocationManager
    {
        private readonly IDepositManager _depositManager;
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LocationManager(IDepositManager depositManager,ILocationRepository locationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _depositManager = depositManager;
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
       // public async Task<List<GetLocationDto>> GetAllLocations()
        //{
          //  List<GetLocationDto> LocationEntityList = await _locationRepository.GetAllAsync();

           // var LocationDtoList = _mapper.Map<List<GetLocationDto>>(LocationEntityList);

            //foreach (var location in LocationDtoList)
            //{
            //    var itemStockEntityList = await _locationRepository.GetAsync(x => x.DepositId == deposit.Id);
            //    deposit.Capacity = itemStockEntityList.Sum(x => x.Capacity);
            //}

            //return depositDtoList;
       //}

        public async Task<bool> InsertLocation(LocationDto location)
        {
            var locationEntity = _mapper.Map<LocationEntity>(location);

            await _locationRepository.InsertAsync(locationEntity);
            var resultsave = await _unitOfWork.SaveChangesAsync();

            return resultsave >= 1 ? true : false;
        }
    }
}