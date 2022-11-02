using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SaleDto
    {        
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<SaleDetailDto> Details { get; set; }
    }
}
