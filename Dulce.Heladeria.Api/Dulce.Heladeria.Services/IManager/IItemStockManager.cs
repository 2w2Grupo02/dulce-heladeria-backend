using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IItemStockManager
    {
        Task<List<ItemStockDto>> GetItemStock(int itemId);
        Task<bool> InsertStockMovement(StockMovementDto movement);
        Task<ItemStockLocationDto> GetAvailableLocations(int itemStockId, int depositId);
        Task<bool> NewEntryToStock(NewItemStockDto newEntry);
        Task<List<DestinationLocationDto>> GetLocations(int itemId, int depositId);
    }
}
