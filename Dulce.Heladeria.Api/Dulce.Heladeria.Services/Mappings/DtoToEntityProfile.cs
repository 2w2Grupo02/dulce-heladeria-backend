using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Mappings
{
    public class DtoToEntityProfile: Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<ItemDto,ItemEntity>();
            CreateMap<CreateUserDto, UserEntity>();
            CreateMap<DepositDto, DepositEntity>();
            CreateMap<LocationDto, LocationEntity>();
        }
    }
}
