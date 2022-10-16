using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface IClientRepository : IBaseRepository<ClientEntity>, IPersistable<ClientEntity>
    {
    }
}
