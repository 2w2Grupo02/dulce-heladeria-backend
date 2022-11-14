using Dulce.Heladeria.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class MovementDto
    {
        public MovementDto(float amount, string motive, DateTime movementDate, int itemStockId)
        {
            Amount = amount;
            Motive = motive;
            MovementDate = movementDate;
            ItemStockId = itemStockId;
        }

        public float Amount { get; set; }

        public string Motive { get; set; }

        public DateTime MovementDate { get; set; }

        public int ItemStockId { get; set; }
    }
}
