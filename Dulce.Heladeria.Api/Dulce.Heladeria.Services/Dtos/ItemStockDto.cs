using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class ItemStockDto: BaseDto
    {
        public int Amount { get; set; }
        public string Location { get; set; }
        public int LocationId { get; set; }
        public string Item { get; set; }
        public string ItemType { get; set; }
        public int Capacity { get; set; }
        public string Deposit { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
