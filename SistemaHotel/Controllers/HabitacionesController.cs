using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers;

public class HabitacionesController : Controller
{
    private readonly Database _context;

    public HabitacionesController(Database context)
    {
        _context = context;
    }

    // GET: Habitaciones
    public async Task<IActionResult> Index()
    {
        var database = _context.Habitacion.Include(h => h.IdTipoHabitacionNavigation);
        return View(await database.ToListAsync());
    }

    // GET: Habitaciones/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Habitacion == null) return NotFound();

        var habitacion = await _context.Habitacion
            .Include(h => h.IdTipoHabitacionNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (habitacion == null) return NotFound();

        return View(habitacion);
    }

    // GET: Habitaciones/Create
    public IActionResult Create()
    {
        ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacion, "Id", "Nombre");
        return View();
    }

    // POST: Habitaciones/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,IdTipoHabitacion,Habilitado,Reservado,Nro")]
        Habitacion habitacion)
    {
        // Cargar el TipoHabitacion asociado al IdTipoHabitacion desde la base de datos
        // var tipoHabitacion = _context.TipoHabitacion.Find(habitacion.IdTipoHabitacion);
        // habitacion.IdTipoHabitacionNavigation = tipoHabitacion;
        if (ModelState.IsValid)
        {
            _context.Add(habitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        ViewData["IdTipoHabitacion"] =
            new SelectList(_context.TipoHabitacion, "Id", "Nombre", habitacion.IdTipoHabitacion);
        return View(habitacion);
    }

    // GET: Habitaciones/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Habitacion == null) return NotFound();

        var habitacion = await _context.Habitacion.FindAsync(id);
        if (habitacion == null) return NotFound();
        ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacion, "Id", "Nombre" +
            "" +
            "", habitacion.IdTipoHabitacion);
        return View(habitacion);
    }

    // POST: Habitaciones/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Nro,IdTipoHabitacion,Habilitado,Reservado")]
        Habitacion habitacion)
    {
        if (id != habitacion.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(habitacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(habitacion.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors);
        ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacion, "Id", "Id", habitacion.IdTipoHabitacion);
        return View(habitacion);
    }

    // GET: Habitaciones/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Habitacion == null) return NotFound();

        var habitacion = await _context.Habitacion
            .Include(h => h.IdTipoHabitacionNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (habitacion == null) return NotFound();

        return View(habitacion);
    }

    // POST: Habitaciones/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Habitacion == null) return Problem("Entity set 'Database.Habitacion'  is null.");
        var habitacion = await _context.Habitacion.FindAsync(id);
        if (habitacion != null) _context.Habitacion.Remove(habitacion);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HabitacionExists(int id)
    {
        return (_context.Habitacion?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}