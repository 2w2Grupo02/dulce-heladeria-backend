using Dulce.Heladeria.DataAccess.Data;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Repositories.BaseRepositories;
using Dulce.Heladeria.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Repositories.Repositories
{
    public class SaleRepository: BaseRepository<SaleEntity>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext bd) : base(bd)
        {

        }
    }
}
