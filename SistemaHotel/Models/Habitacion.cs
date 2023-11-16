using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Habitacion
{
    public int Id { get; set; }

    public int IdTipoHabitacion { get; set; }

    public bool? Habilitado { get; set; }

    public bool? Reservado { get; set; }

    public string Nro { get; set; } = null!;

    public virtual TipoHabitacion IdTipoHabitacionNavigation { get; set; } = null!;

    public virtual ICollection<InventarioReposicion> InventarioReposicions { get; set; } = new List<InventarioReposicion>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
