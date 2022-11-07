using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IClientManager
    {
        Task<bool> InsertClient(ClientDto client);
        Task<List<GetClientsDto>> GetAllClients();
        Task<GetClientsDto> GetClientByName(GetClientDto client);
    }
}