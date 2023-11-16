using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class InventarioHabitacion
{
    public int IdHabitacion { get; set; }

    public int IdItem { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual ItemHabitacion IdItemNavigation { get; set; } = null!;
}
