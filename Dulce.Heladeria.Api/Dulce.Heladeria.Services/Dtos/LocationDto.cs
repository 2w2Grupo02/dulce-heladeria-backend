using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class LocationDto
    {
        public string Column { get; set; }
        public string Row { get; set; }
        public int Capacity { get; set; }
        public int DepositId { get; set; }
        public int ItemTypeId { get; set; }
    }
}
