using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class GetLocationDto: BaseDto
    {
        public string Column { get; set; }
        public string Row { get; set; }
        public int Capacity { get; set; }
        public string depositName { get; set; }
        public string itemTypeName { get; set; }
    }
}
