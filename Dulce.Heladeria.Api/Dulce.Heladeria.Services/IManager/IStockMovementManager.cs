using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IStockMovementManager
    {
        Task<List<MovementDto>> GetAll();
        List<MovementDto> GetAllByDate(DateTime start, DateTime end);

        List<MovementDto> GetAllByItemId(int id);
    }
}
