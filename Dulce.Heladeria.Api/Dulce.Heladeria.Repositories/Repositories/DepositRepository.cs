using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class DepositRepository : BaseRepository<DepositEntity>, IDepositRepository
    {
        public DepositRepository(ApplicationDbContext bd) : base(bd)
        {
        }
    }
}
