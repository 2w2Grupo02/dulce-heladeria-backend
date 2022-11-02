using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.Manager
{
    public class ItemStockManager : IItemStockManager
    {
        private readonly IItemStockRepository _itemStockRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemStockManager(
            IItemStockRepository itemStockRepository, 
            ILocationRepository locationRepository, 
            IStockMovementRepository stockMovementRepository, 
            IItemRepository itemRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _itemStockRepository = itemStockRepository;
            _locationRepository = locationRepository;
            _stockMovementRepository = stockMovementRepository;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<List<ItemStockDto>> GetItemStock(int itemId)
        {
            var itemStockEntityList = await _itemStockRepository.GetItemStock(itemId);

            var itemStockDtoList = _mapper.Map<List<ItemStockDto>>(itemStockEntityList);

            return itemStockDtoList;
        }
        public async Task<ItemStockLocationDto> GetAvailableLocations(int itemId, int depositId)
        {
            var itemEntity = await _itemRepository.GetById(itemId);
            var destinationLocations = await _locationRepository.GetAsync(x => x.ItemTypeId == itemEntity.ItemTypeId && x.DepositId == depositId);
            var availableDestinationLocations = new List<LocationEntity>();

            foreach (var location in destinationLocations)
            {
                var destinationResult = await _itemStockRepository.GetAsync(x => x.LocationId == location.Id);
                var destinationItemStockEntity = destinationResult.FirstOrDefault();
                if(destinationItemStockEntity != null && location.Capacity == destinationItemStockEntity.Amount)
                {
                    continue;
                }
                availableDestinationLocations.Add(location);
            }

            var destinationLocationsDto = _mapper.Map<List<DestinationLocationDto>>(availableDestinationLocations);

            return new ItemStockLocationDto() { ItemId = itemId, DestinationLocations = destinationLocationsDto };
        }
        public async Task<bool> InsertStockMovement(StockMovementDto movement)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var originResult = await _itemStockRepository.GetAsync(x => x.Id == movement.ItemStockId);
                    var originItemStockEntity = originResult.FirstOrDefault();

                    if(originItemStockEntity == null)
                    {
                        throw new InvalidOperationException("No existe la ubicacion de stock de origen.");
                    }

                    if (originItemStockEntity.Amount < movement.Amount)
                    {
                        throw new InvalidOperationException("La cantidad a descontar excede la disponible. Cant. existente: " + originItemStockEntity.Amount);
                    }

                    originItemStockEntity.Amount -= movement.Amount;
                    await _itemStockRepository.UpdateAsync(originItemStockEntity);

                    var newOriginMovement = new StockMovementEntity() 
                    { 
                        Amount = - movement.Amount, 
                        ItemStockId = movement.ItemStockId, 
                        Motive = movement.Motive, 
                        MovementDate = DateTime.Now 
                    };
                    await _stockMovementRepository.InsertAsync(newOriginMovement);

                    var destinationResult = await _itemStockRepository.GetAsync(x => x.LocationId == movement.DestinationLocationId);
                    var destinationItemStockEntity = destinationResult.FirstOrDefault();

                    var destinationLocation = await _locationRepository.GetById(movement.DestinationLocationId);

                    if(destinationLocation == null)
                    {
                        throw new InvalidOperationException("No existe la ubicacion de destino.");
                    }

                    var newDestinationMovement = new StockMovementEntity()
                    {
                        Amount = movement.Amount,
                        Motive = movement.Motive,
                        MovementDate = DateTime.Now
                    };

                    if (destinationItemStockEntity == null)
                    {
                        if (destinationLocation.Capacity < movement.Amount)
                        {
                            throw new InvalidOperationException("La cantidad a sumar excede a la capacidad de ubicacion destino. Capacidad: " + destinationLocation.Capacity);
                        }

                        var newItemStockEntity = new ItemStockEntity()
                        {
                            Amount = movement.Amount,
                            ItemId = originItemStockEntity.ItemId,
                            LocationId = movement.DestinationLocationId,
                            EntryDate = DateTime.Now
                        };
                        var itemStockId = await _itemStockRepository.SaveAsync(newItemStockEntity);
                        newDestinationMovement.ItemStockId = itemStockId;   
                    }
                    else
                    {
                        destinationItemStockEntity.Amount += movement.Amount;

                        if (destinationLocation.Capacity < destinationItemStockEntity.Amount)
                        {
                            throw new InvalidOperationException("La cantidad a sumar excede a la capacidad de ubicacion destino. Capacidad: " + destinationLocation.Capacity);
                        }

                        await _itemStockRepository.UpdateAsync(destinationItemStockEntity);
                        newDestinationMovement.ItemStockId = destinationItemStockEntity.Id;            
                    }
                    await _stockMovementRepository.InsertAsync(newDestinationMovement);

                    var resultsave = await _unitOfWork.SaveChangesAsync();

                    if (resultsave < 1)
                    {
                        throw new InvalidOperationException("Error al insertar un nuevo movimiento");
                    }

                    transaction.Commit();
                    return true;
                }
                catch (InvalidOperationException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
