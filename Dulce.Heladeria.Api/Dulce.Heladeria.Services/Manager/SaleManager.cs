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
    public class SaleManager: ISaleManager
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IItemStockRepository _itemStockRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SaleManager(
            ISaleRepository saleRepository, 
            ISaleDetailRepository saleDetailRepository, 
            IStockMovementRepository stockMovementRepository,
            IItemStockRepository itemStockRepository,
            IClientRepository clientRepository,
            IItemRepository itemRepository,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _stockMovementRepository = stockMovementRepository;
            _itemStockRepository = itemStockRepository;
            _clientRepository = clientRepository;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> InsertNewSale(SaleDto saleDto)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var clientResult = await _clientRepository.GetAsync(x => x.Id == saleDto.ClientId);
                    var clientEntity = clientResult.FirstOrDefault();

                    if (clientEntity == null)
                    {
                        throw new InvalidOperationException($"No existe el cliente {saleDto.ClientName}");
                    }

                    var saleEntity = new SaleEntity()
                    {
                        PaymentMethod = saleDto.PaymentMethod,
                        ClientId = saleDto.ClientId,
                        Date = DateTime.Now
                    };

                    int saleID = await _saleRepository.SaveAsync(saleEntity);                   

                    foreach (var saleDetail in saleDto.Details)
                    {
                        //guardar detalle

                        var saleDetailEntity = _mapper.Map<SaleDetailEntity>(saleDetail);
                        saleDetailEntity.SaleId = saleID;
                        await _saleDetailRepository.InsertAsync(saleDetailEntity);

                        //descontar stock

                        var itemStockResult = await _itemStockRepository.GetAsync(x => x.Item.Id == saleDetail.ItemId);
                        var itemStockEntity = itemStockResult.FirstOrDefault();

                        if (itemStockEntity == null)
                        {
                            throw new InvalidOperationException($"No hay stock del articulo {saleDetail.ItemName}");
                        }

                        if (itemStockEntity.Amount < saleDetail.Amount)
                        {
                            throw new InvalidOperationException("La cantidad a descontar excede la disponible. Cant. existente: " + itemStockEntity.Amount);
                        }

                        //var itemResult = await _itemRepository.GetAsync(x => x.Id == sale.ItemId);
                        //var itemEntity = itemResult.FirstOrDefault();

                        //ver la forma de descontar el articulo

                        itemStockEntity.Amount -= saleDetail.Amount;
                        await _itemStockRepository.UpdateAsync(itemStockEntity);

                        //registrar movimiento

                        var newOriginMovement = new StockMovementEntity()
                        {
                            Amount = -saleDetail.Amount,
                            ItemStockId = itemStockEntity.Id,
                            Motive = "Venta",
                            MovementDate = DateTime.Now
                        };
                        await _stockMovementRepository.InsertAsync(newOriginMovement);
                    }

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
