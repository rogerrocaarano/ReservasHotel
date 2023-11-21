using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Models;

namespace SistemaHotel.Data;

public class IdentityDatabase : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
{
    public IdentityDatabase(DbContextOptions<IdentityDatabase> options) : base(options)
    {

    }
}
