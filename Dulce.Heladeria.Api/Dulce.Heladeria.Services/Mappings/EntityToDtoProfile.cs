using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Mappings
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<ItemEntity, ItemDto>();
            CreateMap<ItemStockEntity, ItemStockDto>()
             .ForMember(dto => dto.Location, entity => entity.MapFrom(x => x.Location.Column + x.Location.Row))
             .ForMember(dto => dto.Item, entity => entity.MapFrom(x => x.Item.Name))
             .ForMember(dto => dto.ItemType, entity => entity.MapFrom(x => x.Item.ItemType.Description))
             .ForMember(dto => dto.Capacity, entity => entity.MapFrom(x => x.Location.Capacity))
             .ForMember(dto => dto.Deposit, entity => entity.MapFrom(x => x.Location.Deposit.Name));

            CreateMap<ItemEntity, GetItemsDto>()
            .ForMember(dto => dto.ItemType, entity => entity.MapFrom(x => x.ItemType.Description))
            .ForMember(dto => dto.MeasuringType, entity => entity.MapFrom(x => EnumHelper.GetDescription(x.MeasuringType)));

            CreateMap<DepositEntity, GetDepositDto>();

            CreateMap<LocationEntity, LocationDto>()
                //.ForMember(dto => dto.Deposito, entity => entity.MapFrom(x => x.DepositId + x.Deposit.Name))
                //.ForMember(dto => dto.itemType, entity => entity.MapFrom(x => x.ItemTypeId + x.ItemType.Description));
                .ForMember(dto => dto.Capacity, entity => entity.MapFrom(x => x.Capacity))
                .ForMember(dto => dto.Column, entity => entity.MapFrom(x => x.Column))
                .ForMember(dto => dto.Row, entity => entity.MapFrom(x => x.Row))
                .ForMember(dto => dto.Deposito, entity => entity.MapFrom(x => x.DepositId))
                .ForMember(dto => dto.itemType, entity => entity.MapFrom(x => x.ItemTypeId));
        }
    }
}
