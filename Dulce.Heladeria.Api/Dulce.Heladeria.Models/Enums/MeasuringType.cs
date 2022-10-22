
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dulce.Heladeria.Models.Enums
{
    public enum MeasuringType
    {
        [Description("Unidad")]
        unit = 1,
        [Description("Litro")]
        liter,
        [Description("Paquete")]
        package
    }
}
