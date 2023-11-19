using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class ItemHabitacion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public double Costo { get; set; }

    public virtual ICollection<InventarioReposicion> InventarioReposicion { get; set; } = new List<InventarioReposicion>();
}
