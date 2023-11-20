using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Habitacion
{
    public int Id { get; set; }

    public int IdTipoHabitacion { get; set; }

    public bool Habilitado { get; set; } = true;

    public bool Reservado { get; set; } = false;

    public string Nro { get; set; } = null!;

    public virtual TipoHabitacion? IdTipoHabitacionNavigation { get; set; } = null!;

    public virtual ICollection<InventarioReposicion> InventarioReposicion { get; set; } = new List<InventarioReposicion>();

    public virtual ICollection<Reserva> Reserva { get; set; } = new List<Reserva>();
}
