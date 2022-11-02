using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SaleDetailDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public float Amount { get; set; }
        public double SalePrice { get; set; }
    }
}
