using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Models;

// Asegúrate de ajustar el espacio de nombres según tu estructura de carpetas

namespace SistemaHotel.Data;

public class IdentityDatabase : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
{
    public IdentityDatabase(DbContextOptions<IdentityDatabase> options) : base(options)
    {
    }

    // Puedes agregar propiedades adicionales o personalizaciones aquí si es necesario
}