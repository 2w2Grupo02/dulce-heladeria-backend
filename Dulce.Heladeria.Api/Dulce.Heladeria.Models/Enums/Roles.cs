
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dulce.Heladeria.Models.Enums
{
    public enum Roles
    {
        [Description("Administrador")]
        administrator,
        [Description("Vendedor")]
        seller
    }
}
