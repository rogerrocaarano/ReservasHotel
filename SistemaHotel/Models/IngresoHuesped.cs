using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class IngresoHuesped
{
    public int IdHuesped { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Huesped IdHuespedNavigation { get; set; } = null!;
}
