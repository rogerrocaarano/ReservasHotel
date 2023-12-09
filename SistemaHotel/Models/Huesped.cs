using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Huesped
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string DocIdentidad { get; set; } = null!;

    public string TipoDocIdentidad { get; set; } = null!;

    public string Pais { get; set; } = null!;
}
