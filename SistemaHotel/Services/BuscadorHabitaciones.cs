using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Services;

public class BuscadorHabitaciones : IBuscadorHabitaciones
{
    private readonly Database _context;

    public BuscadorHabitaciones(Database context)
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

    // public IEnumerable<Habitacion> HabitacionesDisponibles(DateTime fechaInicio, DateTime fechaFin)
    // {
    //
    // }

    public IEnumerable<Habitacion> HabitacionesReservadasEnRangoFechas(DateTime fechaInicio, DateTime fechaFin)
    {
        var query =
            from reserva in _context.ReservaHabitacion
            where reserva.InicioReserva >= fechaInicio && reserva.InicioReserva <= fechaFin
            select reserva.IdHabitacion;
        return _context.Habitacion.Where(h => query.Contains(h.Id));
    }
}