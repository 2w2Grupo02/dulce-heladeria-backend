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

        public async Task<GetClientsDto> GetClientByName(GetClientDto client)
        {
            var clientEntity = await _clientRepository.GetBy(x=> x.BusinessName.ToLower().Contains(client.BusinessName.ToLower()));
            if (clientEntity == null)
            {
                return null;
            }

            var userDto = _mapper.Map<GetClientsDto>(clientEntity);

            return userDto;
        }
        public async Task<bool> UpdateClient(int id, ClientDto client)
        {
            ClientEntity clientEntity = await _clientRepository.GetById(id);

            clientEntity.BusinessName = client.BusinessName;
            clientEntity.Identifier = client.Identifier;
            clientEntity.IdentifierTypeId = client.IdentifierTypeId;
            clientEntity.HomeAdress = client.HomeAdress;
            clientEntity.Email = client.Email;

            await _clientRepository.UpdateAsync(clientEntity);
            var resultsave = await _unitOfWork.SaveChangesAsync();

            return resultsave >= 1 ? true : false;
        }
    }
}