using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class ItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemTypeId { get; set; }
        public MeasuringType MeasuringType { get; set; }
    }
}
