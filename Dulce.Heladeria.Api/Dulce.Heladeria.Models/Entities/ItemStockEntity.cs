using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class ItemStockEntity: BaseEntity
    {
        [Required]
        public int Amount { get; set; }

        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }

        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
