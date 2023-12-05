using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class RegistroReservaHabitacion
{
    public int IdReserva { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime? InicioReserva { get; set; }

    public DateTime? FinReserva { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
