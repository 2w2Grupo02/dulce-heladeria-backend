using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Dulce.Heladeria.Services.Dtos
{
    public class RankingProduct
    {
        public string Producto { get; set; }
        public int Cant { get; set; }
        public double Total { get; set; }


        public RankingProduct(string producto,int cant, double total)
        {
            this.Producto = producto;
            this.Total = total;
            this.Cant = cant;
        }
    }
}
