using SistemaHotel.Models;

namespace SistemaHotel.Services;

public interface IBuscadorHabitaciones
{
    IEnumerable<Habitacion> HabitacionesPorTipo(int idTipoHabitacion);
    IEnumerable<Habitacion> HabitacionesPorTipo(TipoHabitacion tipoHabitacion);
    // IEnumerable<Habitacion> HabitacionesDisponibles(DateTime fechaInicio, DateTime fechaFin);

}