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
            CreateMap<ClientEntity, ClientDto>();
        }
    }
}
