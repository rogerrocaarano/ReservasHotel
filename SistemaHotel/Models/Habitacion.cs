using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Habitacion
{
    public int Id { get; set; }

    public int IdTipoHabitacion { get; set; }

    public int Piso { get; set; }

    public string Ubicacion { get; set; } = null!;

    public bool Habilitado { get; set; }

    public bool Disponible { get; set; }

    public virtual TipoHabitacion? IdTipoHabitacionNavigation { get; set; } = null!;

    public virtual ICollection<InventarioReposicion> InventarioReposicion { get; set; } = new List<InventarioReposicion>();
}
