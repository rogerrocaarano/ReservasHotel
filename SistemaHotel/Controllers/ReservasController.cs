using Microsoft.AspNetCore.Mvc;
using SistemaHotel.Data;
using SistemaHotel.Models;
using SistemaHotel.Services;

namespace SistemaHotel.Controllers;

public class ReservasController : Controller
{
    private readonly Database _context;
    private readonly IClienteService _clienteService;
    private readonly IBuscadorHabitaciones _buscadorHabitaciones;
    private readonly string _controllerName;
    public string ControllerName => ControllerContext.ActionDescriptor.ControllerName;

    public ReservasController(
        Database context,
        IClienteService clienteService,
        IBuscadorHabitaciones buscadorHabitaciones)
    {
        _context = context;
        _clienteService = clienteService;
        _buscadorHabitaciones = buscadorHabitaciones;
    }



    public IActionResult Index()
    {
        ViewBag.controllerName = ControllerName;
        return View();
    }



    public IActionResult BuscarClientes(string? busqueda)
    {
        var clientes = _clienteService.BuscarClientes(busqueda);

        ViewBag.controllerName = ControllerName;
        return PartialView("Clientes/_ListaClientes", clientes);
    }



    public IActionResult BuscarHabitaciones(int busqueda)
    {
        var habitaciones = _buscadorHabitaciones.HabitacionesPorTipo(busqueda);

        ViewBag.controllerName = ControllerName;
        ViewBag.rutaBuscador = ControllerName + "/BuscarHabitaciones";
        return PartialView("Habitaciones/_ResultadosBuscador", habitaciones);
    }



    public IActionResult Cliente()
    {
        var clientes = new List<Cliente>();

        ViewBag.controllerName = ControllerName;
        return View(clientes);
    }



    public IActionResult Habitaciones(int id)
    {
        var tiposHabitacion = _context.TipoHabitacion.ToDictionary(t => t.Id.ToString(), t => t.Nombre);
        var habitaciones = _buscadorHabitaciones.HabitacionesPorTipo(1);

        ViewBag.idCliente = id;
        ViewBag.controllerName = ControllerName;
        ViewBag.selectOptions = tiposHabitacion;
        ViewBag.rutaBuscador = ControllerName + "/BuscarHabitaciones";
        return View(habitaciones);
    }
}