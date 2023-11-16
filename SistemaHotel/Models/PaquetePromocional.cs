using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class PaquetePromocional
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Precio { get; set; }

    public bool? Habilitado { get; set; }

    public DateTime FechaDisponibleInicio { get; set; }

    public DateTime FechaDisponibleFin { get; set; }
}
