using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class TipoHabitacion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int HuespedesPermitidos { get; set; }

    public double PrecioNoche { get; set; }

    public virtual ICollection<Habitacion> Habitacion { get; set; } = new List<Habitacion>();
}
