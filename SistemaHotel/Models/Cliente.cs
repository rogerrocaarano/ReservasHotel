using System;
using System.Collections.Generic;

namespace SistemaHotel.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public string? RazonSocial { get; set; }

    public string? NroRazonSocial { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Reserva> Reserva { get; set; } = new List<Reserva>();
}
