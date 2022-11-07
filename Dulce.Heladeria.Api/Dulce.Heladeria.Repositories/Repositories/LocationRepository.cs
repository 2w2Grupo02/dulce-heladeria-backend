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
    public class LocationRepository: BaseRepository<LocationEntity>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext bd) : base(bd)
        {
           
        }
        public async Task<List<LocationEntity>> GetAllLocations()
        {
            List<LocationEntity> locationEntity = await BaseQuery
                .Include(x => x.ItemType).ThenInclude(x => x.Description)
                .Include(x => x.Deposit).ThenInclude(x => x.Name)
                .ToListAsync();

            return locationEntity;
        }
    }
}
