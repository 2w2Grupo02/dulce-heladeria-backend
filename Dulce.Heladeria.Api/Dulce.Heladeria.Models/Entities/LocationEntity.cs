using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class LocationEntity: BaseEntity
    {
        [Required]
        public string Column { get; set; }

        [Required]
        public string Row { get; set; }

        [Required]
        public int Capacity { get; set; }

        public int ItemTypeId { get; set; }
        public ItemTypeEntity ItemType { get; set; }

        public int DepositId { get; set; }
        public DepositEntity Deposit { get; set; }
    }
}
