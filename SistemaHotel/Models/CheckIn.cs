using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class CheckIn
{
    public int IdHuesped { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime? Ingreso { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Huesped IdHuespedNavigation { get; set; } = null!;
}
