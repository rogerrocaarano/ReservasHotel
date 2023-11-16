using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class PagoQr
{
    public int IdPago { get; set; }

    public string NroTransaccion { get; set; } = null!;

    public virtual Pago IdPagoNavigation { get; set; } = null!;
}
