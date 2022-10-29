using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IDepositManager
    {
        Task<List<GetDepositDto>> GetAllDeposits();
        Task<bool> InsertDeposit(DepositDto deposit);
    }
}
