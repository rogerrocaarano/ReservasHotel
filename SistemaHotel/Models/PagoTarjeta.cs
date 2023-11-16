using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class PagoTarjeta
{
    public int IdPago { get; set; }

    public string TipoTarjeta { get; set; } = null!;

    public int UltimosDigTarjeta { get; set; }

    public string Autorizacion { get; set; } = null!;

    public string Operacion { get; set; } = null!;

    public virtual Pago IdPagoNavigation { get; set; } = null!;
}
