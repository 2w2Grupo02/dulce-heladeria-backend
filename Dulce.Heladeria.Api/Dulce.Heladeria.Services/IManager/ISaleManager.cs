using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface ISaleManager
    {
        Task<int> InsertNewSale(SaleDto saleDto);
        Task<List<SalePerDayDto>> getAllSales(DateTime start, DateTime end);
        Task<List<GetSaleDto>> GetSales();
        Task<List<RankingProduct>> GetMostSaleProductsByRange(DateTime start, DateTime end);
        Task<List<SalesWithMethod>> getAllSalesByMethod(DateTime start);
    }
}
