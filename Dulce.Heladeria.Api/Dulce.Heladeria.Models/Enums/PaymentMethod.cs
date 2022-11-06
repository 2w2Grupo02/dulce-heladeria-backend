using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dulce.Heladeria.Models.Enums
{
    public enum PaymentMethod
    {
        [Description("Efectivo")]
        Cash = 1,
        [Description("Mercado Pago")]
        MercadoPago,
        [Description("Tarjeta")]
        Card
    }
}
