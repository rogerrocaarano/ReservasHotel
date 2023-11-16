using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int IdCobro { get; set; }

    public double Monto { get; set; }

    public virtual Cobro IdCobroNavigation { get; set; } = null!;
}
