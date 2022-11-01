using Dulce.Heladeria.Models.BaseEntities;
using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class ItemEntity : BaseEntity
    {

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemTypeId { get; set; }
        public ItemTypeEntity ItemType { get; set; }
        public MeasuringType MeasuringType { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
