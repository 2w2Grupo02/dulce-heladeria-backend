using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class ProductItemEntity: BaseEntity
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }
    }
}
