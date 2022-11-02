using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class SaleDetailEntity : BaseEntity
    {
        [Required]
        public float Amount { get; set; }

        [Required]
        public double SalePrice { get; set; }
        
        public int ProductId { get; set; }
        public ItemEntity Product { get; set; }

        public int SaleId { get; set; }
        public SaleEntity Sale { get; set; }
    }
}
