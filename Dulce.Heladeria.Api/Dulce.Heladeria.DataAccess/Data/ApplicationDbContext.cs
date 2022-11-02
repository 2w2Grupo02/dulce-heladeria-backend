
using Dulce.Heladeria.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.DataAccess.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ItemEntity> Item { get; set; }
        public DbSet<ItemTypeEntity> ItemType { get; set; }

        public DbSet<UserEntity> user { get; set; }
        public DbSet<ItemStockEntity> ItemStock { get; set; }
        public DbSet<LocationEntity> Location { get; set; }
        public DbSet<DepositEntity> Deposit { get; set; }
        public DbSet<ClientEntity> Client { get; set; }
        public DbSet<IdentifierTypeEntity> IdentifierType { get; set; }
        public DbSet<StockMovementEntity> StockMovement { get; set; }
        public DbSet<SaleEntity> Sale { get; set; }
        public DbSet<SaleDetailEntity> SaleDetail { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
    }
}
