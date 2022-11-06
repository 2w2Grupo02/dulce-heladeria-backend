using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class GetSaleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string PaymentMethod { get; set; }
        public double TotalAmount { get; set; }
    }
}
