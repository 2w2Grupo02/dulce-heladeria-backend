using Dulce.Heladeria.Models.BaseEntities;
using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Models.Entities
{
    public class SaleEntity : BaseEntity
    {
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int ClientId { get; set; }
        public ClientEntity Client { get; set; }
    }
}
