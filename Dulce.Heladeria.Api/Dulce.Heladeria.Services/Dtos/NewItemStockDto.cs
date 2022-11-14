using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class NewItemStockDto
    {
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public float Amount { get; set; }
    }
}
