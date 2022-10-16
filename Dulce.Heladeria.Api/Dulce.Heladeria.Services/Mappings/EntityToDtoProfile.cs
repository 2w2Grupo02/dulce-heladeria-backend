﻿using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Services.Dtos;
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
        }
    }
}
