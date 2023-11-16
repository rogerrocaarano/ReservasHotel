using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class InventarioReposicion
{
    public int Id { get; set; }

    public int IdHabitacion { get; set; }

    public int IdItemHabitacion { get; set; }

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual ItemHabitacion IdItemHabitacionNavigation { get; set; } = null!;
}
