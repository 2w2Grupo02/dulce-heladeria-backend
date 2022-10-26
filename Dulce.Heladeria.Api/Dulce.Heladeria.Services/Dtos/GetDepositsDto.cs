using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class GetDepositsDto: DepositDto
    {
        public int Capacity { get; set; }
    }
}
