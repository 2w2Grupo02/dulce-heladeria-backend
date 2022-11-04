
using Asofar.Backend.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Dulce.Heladeria.Models.Entities;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>, IPersistable<UserEntity>
    {
        Task<bool> ExistsUser(string user);
    }
}
