using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Services;

public class HabitationFinder : IHabitationFinder
{
    private readonly Database _context;

    public HabitationFinder(Database context)
    {
        _context = context;
    }

    public IEnumerable<Habitacion> HabitacionesPorTipo(int idTipoHabitacion)
    {
        return _context.Habitacion.Where(h => h.IdTipoHabitacion == idTipoHabitacion);
    }

    public IEnumerable<Habitacion> HabitacionesPorTipo(TipoHabitacion tipoHabitacion)
    {
        return _context.Habitacion.Where(h => h.IdTipoHabitacion == tipoHabitacion.Id);
    }
}