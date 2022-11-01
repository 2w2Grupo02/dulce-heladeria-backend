using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class StockMovementEntity: BaseEntity
    {
        [Required]
        public int Amount { get; set; }

        [Required]
        public string Motive { get; set; }

        public DateTime MovementDate { get; set; }

        public int ItemStockId { get; set; }
        public ItemStockEntity ItemStock { get; set; }
    }
}
