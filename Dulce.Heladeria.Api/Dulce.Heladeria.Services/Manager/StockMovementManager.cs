using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.Manager
{
    public class StockMovementManager : IStockMovementManager
    { 
        private readonly IStockMovementRepository _stockMovementRepository;
        public StockMovementManager(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<List<MovementDto>> GetAll()
        {
            List<StockMovementEntity> movs = await _stockMovementRepository.GetAllAsync();
            List<MovementDto> movDtos = new List<MovementDto>();

            foreach (var mov in movs) {
                movDtos.Add(new MovementDto(mov.Amount, mov.Motive, mov.MovementDate, mov.ItemStockId));
            }
            return movDtos;
        }

        public List<MovementDto> GetAllByDate(DateTime start, DateTime end)
        {
            List<StockMovementEntity> movs =  _stockMovementRepository.getAllMovementsByDate(start, end);
            List<MovementDto> movDtos = new List<MovementDto>();

            foreach (var mov in movs)
            {
                movDtos.Add(new MovementDto(mov.Amount, mov.Motive, mov.MovementDate, mov.ItemStockId));
            }
            return movDtos;
        }

        public List<MovementDto> GetAllByItemId(int id)
        {
            List<StockMovementEntity> movs = _stockMovementRepository.getAllMovementsByItem(id);
            List<MovementDto> movDtos = new List<MovementDto>();

            foreach (var mov in movs)
            {
                movDtos.Add(new MovementDto(mov.Amount, mov.Motive, mov.MovementDate, mov.ItemStockId));
            }
            return movDtos;
        }
    }
}
