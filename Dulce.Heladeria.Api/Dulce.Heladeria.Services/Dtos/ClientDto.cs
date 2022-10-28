using Dulce.Heladeria.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dulce.Heladeria.Services.Dtos
{
    public class ClientDto : BaseDto
    {
        public string BusinessName { get; set; }
        public int IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public string HomeAdress { get; set; }
        public string Email { get; set; }
    }
}
