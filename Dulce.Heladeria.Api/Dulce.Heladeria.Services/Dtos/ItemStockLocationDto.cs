using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class ItemStockLocationDto
    {
        public int ItemId { get; set; }
        public List<DestinationLocationDto> DestinationLocations { get; set; }
    }
}
