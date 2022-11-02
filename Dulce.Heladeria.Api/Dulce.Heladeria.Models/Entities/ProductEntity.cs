using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class ProductEntity: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double ListPrice { get; set; }
    }
}
