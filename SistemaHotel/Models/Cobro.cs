using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Cobro
{
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public string? Descripcion { get; set; }

    public double Total { get; set; }

    public string? Estado { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
