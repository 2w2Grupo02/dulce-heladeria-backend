using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class DepositEntity: BaseEntity
    {
        [Required]
        public string Address { get; set; }
    }
}
