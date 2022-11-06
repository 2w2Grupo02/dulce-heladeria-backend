using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Dulce.Heladeria.Services.Dtos
{
    public class RankingProduct
    {
        int posicion;
        ProductEntity producto;
        double total;
        int cant; 
    }
}
