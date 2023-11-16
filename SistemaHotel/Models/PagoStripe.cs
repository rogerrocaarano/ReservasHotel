using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class PagoStripe
{
    public int IdPago { get; set; }

    public string IdStripe { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual Pago IdPagoNavigation { get; set; } = null!;
}
