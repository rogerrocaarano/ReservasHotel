using Microsoft.AspNetCore.Mvc;
using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers;

public class ReservasController : Controller
{
    private readonly Database _context;

    public ReservasController(Database context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        ViewBag.controllerName = "Reservas";
        var clientes = new List<Cliente>();
        return View(clientes);
    }

    public async Task<IActionResult> Create(int? idCliente)
    {
        if (idCliente == null) return NotFound();
        var cliente = await _context.Cliente.FindAsync(idCliente);
        if (cliente == null) return NotFound();
        // return NotFound();

        return View();
    }

    public void AdicionarHabitacion(
        int idReserva,
        int idHabitacion,
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        var reserva = _context.Reserva.Find(idReserva);
        var habitacion = _context.Habitacion.Find(idHabitacion);
        // if (reserva == null || habitacion == null) return null;
        //
        // if (GetReservasHabitacion(idHabitacion, fechaInicio, fechaFin).Count > 0) return null;
        var reservaHabitacion = new ReservaHabitacion
        {
            InicioReserva = fechaInicio,
            FinReserva = fechaFin,
            IdHabitacion = idHabitacion,
            IdReserva = idReserva
        };
        _context.ReservaHabitacion.Add(reservaHabitacion);

    }



    /// <summary>
    /// Retorna las reservas de una habitacion dentro de un rango de fechas.
    /// </summary>
    /// <param name="idHabitacion"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <returns></returns>
    public List<ReservaHabitacion> GetReservasHabitacion(
        int idHabitacion,
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        return _context.ReservaHabitacion
            .Where(rh =>
                rh.IdHabitacion == idHabitacion && rh.InicioReserva >= fechaInicio && rh.InicioReserva <= fechaFin)
            .ToList();
    }



    /// <summary>
    /// Retorna las habitaciones de un tipo determinado que no estan reservadas dentro de un rango de fechas.
    /// </summary>
    /// <param name="tipoHabitacion"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <returns></returns>
    public List<Habitacion> GetHabitacionesDisponibles(
        TipoHabitacion tipoHabitacion,
        DateTime fechaInicio,
        DateTime fechaFin)
    {
        var habitaciones = _context.Habitacion
            .Where(h => h.IdTipoHabitacion == tipoHabitacion.Id)
            .ToList();
        return habitaciones.Where(habitacion =>
            GetReservasHabitacion(habitacion.Id, fechaInicio, fechaFin).Count == 0).ToList();
    }
}