using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SalePerDayDto
    {
        public SalePerDayDto(DateTime date, float total)
        {
            Date = date;
            Total = total;
        }

        public DateTime Date { get; set; }
        public float Total { get; set; }
    }
}
