﻿
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
    }
}
