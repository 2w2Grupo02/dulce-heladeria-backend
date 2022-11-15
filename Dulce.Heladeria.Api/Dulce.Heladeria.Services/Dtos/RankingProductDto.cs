using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Dulce.Heladeria.Services.Dtos
{
    public class RankingProductDto
    {
        public int RankingPosition { get; set; }
        public string ProductName { get; set; }
        public float TotalAmount { get; set; }
        public int TotalSaleQuantity { get; set; }
        public double TotalSaleAmount { get; set; }
    }
}
