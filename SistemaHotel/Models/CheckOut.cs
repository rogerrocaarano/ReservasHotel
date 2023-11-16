using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class CheckOut
{
    public int IdHuesped { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime? Salida { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Huesped IdHuespedNavigation { get; set; } = null!;
}
