using Dulce.Heladeria.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.IRepositories;
using Dulce.Heladeria.DataAccess.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext bd) : base(bd)
        {
        }
        public async Task<bool> ExistsUser(string user)
        {
           var result = await BaseQuery.Where(x => x.Email == user).FirstOrDefaultAsync();

            return result != null ? true : false;
        }
    }
}
