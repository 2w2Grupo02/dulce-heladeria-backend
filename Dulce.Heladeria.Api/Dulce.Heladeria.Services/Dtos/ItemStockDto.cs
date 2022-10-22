using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class ItemStockDto: BaseDto
    {
        public int Amount { get; set; }
        public int Location { get; set; }
        public int Item { get; set; }
        public int ItemType { get; set; }
        public int Capacity { get; set; }
        public int Deposit { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
