using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int MaxItemAmount { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductItemDto> Items { get; set; }
    }
}
