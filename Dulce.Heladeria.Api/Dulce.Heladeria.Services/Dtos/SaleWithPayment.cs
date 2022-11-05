using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SaleWithPayment
    {
        public SaleWithPayment(PaymentMethod paymentMethod, double total)
        {
            PaymentMethod = paymentMethod;
            Total = total;
        }
        public double Total { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
