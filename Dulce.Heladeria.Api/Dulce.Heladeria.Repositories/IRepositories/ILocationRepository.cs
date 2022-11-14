using Asofar.Backend.Models.Repositories;
using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.IRepositories
{
    public interface ILocationRepository: IBaseRepository<LocationEntity>, IPersistable<LocationEntity>
    {
        Task<List<LocationEntity>> GetAllLocations();
        Task<List<LocationEntity>> GetLocation(int id);
    }
}
