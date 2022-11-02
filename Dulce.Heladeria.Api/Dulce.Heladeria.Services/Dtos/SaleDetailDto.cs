using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class SaleDetailDto
    {
        public int ProductId { get; set; }
        public List<ProductItemDto> ProductDetail { get; set; }
        public string ProductName { get; set; }
        public float Amount { get; set; }
        public double SalePrice { get; set; }
    }
}
