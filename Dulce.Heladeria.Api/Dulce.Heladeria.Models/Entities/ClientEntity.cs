using Dulce.Heladeria.Models.BaseEntities;
using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class ClientEntity : BaseEntity
    {
        [Required]
        public string BusinessName { get; set; }
        public int IdentifierTypeId { get; set; }
        public IdentifierTypeEntity IdentifierType { get; set; }
        public string Identifier { get; set; }
        public string HomeAdress { get; set; }
        public string Email { get; set; }
    }
}
