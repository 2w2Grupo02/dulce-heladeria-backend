using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SalePerDayDto
    {
        public DateTime Date { get; set; }
        public double Total { get; set; }
    }
}
