using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class GetItemsDto: BaseDto
    {
        public string Name { get; set; }
        public string MeasuringType { get; set; }
        public string ItemType { get; set; }
        public float Amount { get; set; }
    }
}
