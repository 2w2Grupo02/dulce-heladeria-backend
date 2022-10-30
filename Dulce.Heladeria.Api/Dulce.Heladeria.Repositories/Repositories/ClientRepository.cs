using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class ClientRepository : BaseRepository<ClientEntity>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext bd) : base(bd)
        {
        }

        public async Task<List<ClientEntity>> GetAllClients()
        {
            List<ClientEntity> clientEntities = await BaseQuery.Include(x => x.IdentifierType).ToListAsync();

            return clientEntities;
        }
    }
}
