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
            CreateMap<ClientDto,ClientEntity>();
            CreateMap<DepositDto, DepositEntity>();
            CreateMap<LocationDto, LocationEntity>();
            CreateMap<SaleDetailDto, SaleDetailEntity>();
            CreateMap<CreateProductDto, ProductEntity>()
                .ForMember(entity => entity.ListPrice, dto => dto.MapFrom(x => x.Price));
            CreateMap<ProductItemDto, ProductItemEntity>()
                .ForMember(entity => entity.ItemId, dto => dto.MapFrom(x => x.Id))
                .ForMember(entity => entity.Id, dto => dto.MapFrom(x => 0));
            CreateMap<NewItemStockDto, ItemStockEntity>();
        }
    }
}
