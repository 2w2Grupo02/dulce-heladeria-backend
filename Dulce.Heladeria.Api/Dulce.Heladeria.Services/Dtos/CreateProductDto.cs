using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int MaxItemAmount { get; set; }
        public List<ProductItemDto> Items { get; set; }
    }
}
