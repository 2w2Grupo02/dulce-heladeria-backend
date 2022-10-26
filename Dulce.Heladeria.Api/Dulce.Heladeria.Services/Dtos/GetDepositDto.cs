using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class GetDepositDto: BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}
