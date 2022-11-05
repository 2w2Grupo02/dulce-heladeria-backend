using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SalesWithMethod
    {
        public SalesWithMethod(PaymentMethod paymentMethod, int cant, double total)
        {
            PaymentMethod = paymentMethod;
            this.cant = cant;
            this.total = total;
        }

        public PaymentMethod PaymentMethod { get; set; }
        public int cant { get; set; }
        public double total { get; set; }
    }
}
