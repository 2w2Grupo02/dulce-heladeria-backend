using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.Manager
{
    public class ClientManager : IClientManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClientManager(IClientRepository clientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<bool> InsertClient(ClientDto client)
        {
            var clientEntity = _mapper.Map<ClientEntity>(client);

            await _clientRepository.InsertAsync(clientEntity);
            var resultsave = await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<List<GetClientsDto>> GetAllClients()
        {
            List<ClientEntity> clientEntityList = await _clientRepository.GetAllClients();

            var clientDtoList = _mapper.Map<List<GetClientsDto>>(clientEntityList);

            //foreach (var client in clientDtoList)
            //{
            //    var clientStockEntityList = await _itemStockRepository.GetItemStock(item.Id);
            //    item.Amount = itemStockEntityList.Sum(x => x.Amount);
            //}



            return clientDtoList;
        }
    }
}