using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime? FechaReserva { get; set; }

    public DateTime? InicioReserva { get; set; }

    public DateTime? FinReserva { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
