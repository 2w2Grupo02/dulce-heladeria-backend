using Dulce.Heladeria.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class IdentifierTypeEntity : BaseEntity
    {
        [Required]
        public string Description { get; set; }
    }
}
