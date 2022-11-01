using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class StockMovementDto
    {
        public int ItemStockId { get; set; }
        public int DestinationLocationId { get; set; }
        public int Amount { get; set; }
        public string Motive { get; set; }
    }
}
