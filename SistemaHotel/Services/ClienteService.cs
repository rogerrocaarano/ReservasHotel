using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Services;

public class ClienteService: IClienteService
{
    private readonly Database _context;

    public ClienteService(Database context)
    {
        _context = context;
    }

    public IEnumerable<Cliente> BuscarClientes(string busqueda)
    {
        Console.Write(busqueda);
        var query = from c in _context.Cliente select c;

        if (!string.IsNullOrEmpty(busqueda))
        {
            query = query.Where(c =>
                EF.Functions.ILike(c.Id.ToString(), $"%{busqueda}%") ||
                EF.Functions.ILike(c.Nombres, $"%{busqueda}%") ||
                EF.Functions.ILike(c.Apellidos, $"%{busqueda}%") ||
                EF.Functions.ILike(c.RazonSocial, $"%{busqueda}%")
            );
        }

        return query.ToList();
    }
}