using Microsoft.AspNetCore.Identity;

namespace SistemaHotel.Models;

public partial class Usuario : IdentityUser<Guid>
{
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
}