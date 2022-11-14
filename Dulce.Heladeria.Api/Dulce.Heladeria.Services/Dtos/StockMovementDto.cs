using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class StockMovementDto
    {
        public StockMovementDto(int itemStockId, int destinationLocationId, float amount, string motive)
        {
            ItemStockId = itemStockId;
            DestinationLocationId = destinationLocationId;
            Amount = amount;
            Motive = motive;
        }

        public int ItemStockId { get; set; }
        public int DestinationLocationId { get; set; }
        public float Amount { get; set; }
        public string Motive { get; set; }
    }
}
