using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.Enums;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Repositories.Repositories;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.Manager
{
    public class SaleManager : ISaleManager
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IItemStockRepository _itemStockRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IItemRepository _itemRepository;
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

        public async Task<List<SalePerDayDto>> getAllSales(DateTime start, DateTime end)
        {
            var sales = await _saleRepository.GetAllAsync();

            var salesAgroupedByDate = sales
                .Where(sale => sale.Date.Date <= end && sale.Date.Date >= start).GroupBy(x => x.Date.Date).ToList();

            List<SalePerDayDto> ventasDto = new List<SalePerDayDto>();

            foreach (var salesDay in salesAgroupedByDate)
            {
                double total = 0;
                foreach (var sale in salesDay)
                {
                    var detalles = await _saleDetailRepository.GetAsync(detalle => detalle.SaleId == sale.Id);
                    
                    foreach (var detalle in detalles)
                    {
                        total += detalle.Amount * detalle.SalePrice;
                    }
                   
                }
                ventasDto.Add(new SalePerDayDto() { Date = salesDay.Key, Total = total });
            }

            return ventasDto; 
        }

        public async Task<List<SalesWithMethod>> getAllSalesByMethod(DateTime start)
        {
            var sales = await _saleRepository.getAllSalesByDay(start);

            List<SaleWithPayment> salesWithPayment = new List<SaleWithPayment>();

            foreach (var sale in sales)
            {
                var detalles = await _saleDetailRepository.GetAsync(detalle => detalle.SaleId == sale.Id);
                double total = 0;
                foreach (var detalle in detalles)
                {
                    total += (detalle.Amount * detalle.SalePrice);
                }
                salesWithPayment.Add(new SaleWithPayment(sale.PaymentMethod, total));
            }

            List<SaleWithPayment> saleCash = salesWithPayment.Where(x => x.PaymentMethod == PaymentMethod.Cash).ToList();
            List<SaleWithPayment> saleCard = salesWithPayment.Where(x => x.PaymentMethod == PaymentMethod.Card).ToList();
            List<SaleWithPayment> saleMp = salesWithPayment.Where(x => x.PaymentMethod == PaymentMethod.MercadoPago).ToList();

            List<SalesWithMethod> salesByMethod = new List<SalesWithMethod>();

            SalesWithMethod cash = new SalesWithMethod(PaymentMethod.Cash,0,0);
            SalesWithMethod card = new SalesWithMethod(PaymentMethod.Card,0,0);
            SalesWithMethod mp = new SalesWithMethod(PaymentMethod.MercadoPago, 0, 0);

            

            foreach (var sale in saleCash) {
                cash.cant++;
                cash.total += sale.Total; 
            }

            foreach (var sale in saleCard)
            {
                card.cant++;
                card.total += sale.Total;
            }

            foreach (var sale in saleMp)
            {
                mp.cant++;
                mp.total += sale.Total;
            }

            List<SalesWithMethod> salesWithMethods = new List<SalesWithMethod>();
            salesWithMethods.Add(cash);
            salesWithMethods.Add(card);
            salesWithMethods.Add(mp);


            return salesWithMethods; 
        }

        public async Task<List<RankingProductDto>> GetMostSaleProductsByRange(DateTime start, DateTime end)
        {
            var saleProducts = await _saleDetailRepository.GetSaleProductsByRange(start, end);
            var rankingProducts = new List<RankingProductDto>();

            var saleProductGroup = saleProducts.GroupBy(x => x.ProductId).ToList();
            
            foreach (var saleProduct in saleProductGroup)
            {
                var rankingProduct = new RankingProductDto()
                {
                    ProductName = saleProduct.First().Product.Name,
                    TotalAmount = saleProduct.Sum(x => x.Amount),
                    TotalSaleAmount = saleProduct.Sum(x => x.Amount * x.SalePrice),
                    TotalSaleQuantity = saleProduct.Count()
                };

                rankingProducts.Add(rankingProduct);
            }

            rankingProducts = rankingProducts.OrderByDescending(x => x.TotalSaleAmount).Take(5).ToList();
            int rankingPosition = 1;
            rankingProducts.ForEach(x =>
            {
                x.RankingPosition = rankingPosition;
                rankingPosition++;
            });

            return rankingProducts;
        }

        public async Task<int> InsertNewSale(SaleDto saleDto)
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
                        var saleDetailEntity = _mapper.Map<SaleDetailEntity>(saleDetail);
                        saleDetailEntity.SaleId = saleID;
                        await _saleDetailRepository.InsertAsync(saleDetailEntity);

                        var quantityItems = saleDetail.ProductDetail.Count;

                        if (quantityItems < 1)
                        {
                            throw new InvalidOperationException($"El producto {saleDetail.ProductName} no tiene articulos para descontar");
                        }

                        foreach (var item in saleDetail.ProductDetail)
                        {
                            var itemEntity = await _itemRepository.GetById(item.Id);

                            if (itemEntity == null)
                            {
                                throw new InvalidOperationException($"No existe el articulo {item.Name}");
                            }

                            float productAmount = saleDetail.Amount;
                            float amountToDiscount = quantityItems > 1 ? (productAmount / quantityItems) : productAmount;

                            var itemStockEntity = await _itemStockRepository.GetBy(x => x.Item.Id == item.Id && x.Amount >= amountToDiscount);

                            if (itemStockEntity == null)
                            {
                                throw new InvalidOperationException($"No hay stock suficiente del articulo {item.Name}");
                            }
                            
                            itemStockEntity.Amount -= amountToDiscount;
                            await _itemStockRepository.UpdateAsync(itemStockEntity);                               
                            
                            var newOriginMovement = new StockMovementEntity()
                            {
                                Amount = -amountToDiscount,
                                ItemStockId = itemStockEntity.Id,
                                Motive = "Venta",
                                MovementDate = DateTime.Now
                            };
                            await _stockMovementRepository.InsertAsync(newOriginMovement);
                        }
                    }

                    var resultsave = await _unitOfWork.SaveChangesAsync();

                    if (resultsave < 1)
                    {
                        throw new InvalidOperationException("Error al insertar un nuevo movimiento");
                    }

                    transaction.Commit();
                    return saleID;
                }
                catch (InvalidOperationException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<List<GetSaleDto>> GetSales()
        {
            var saleEntities = await _saleRepository.GetAllSalesWithClients();
            var salesDto = _mapper.Map<List<GetSaleDto>>(saleEntities);

            foreach (var sale in salesDto)
            {
                var saleDetailEntities = await _saleDetailRepository.GetAsync(x => x.SaleId == sale.Id);

                sale.TotalAmount = saleDetailEntities.Sum(x => x.Amount * x.SalePrice);
            }

            return salesDto;
        }

    }
}
