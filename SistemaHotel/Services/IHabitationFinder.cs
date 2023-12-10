using SistemaHotel.Models;

namespace SistemaHotel.Services;

public interface IHabitationFinder
{
    IEnumerable<Habitacion> HabitacionesPorTipo(int idTipoHabitacion);
    IEnumerable<Habitacion> HabitacionesPorTipo(TipoHabitacion tipoHabitacion);

}