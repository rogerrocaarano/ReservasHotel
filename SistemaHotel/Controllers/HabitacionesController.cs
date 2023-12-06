using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers;

[Authorize (Roles = "EMPLEADO")]
public class HabitacionesController : Controller
{
    private readonly Database _context;

    public HabitacionesController(Database context)
    {
        _context = context;
    }

    private async Task<List<TipoHabitacion>> GetTiposHabitacion()
    {
        return await _context.TipoHabitacion.ToListAsync();
    }

    // GET: Habitaciones
    public async Task<IActionResult> Index()
    {
        var habitaciones = await _context.Habitacion
            .OrderBy(habitacion => habitacion.Id)
            .ToListAsync();
        ViewBag.TiposHabitacion = await GetTiposHabitacion();
        return View(habitaciones);
    }
    // GET: Habitaciones/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.TiposHabitacion = await GetTiposHabitacion();
        return View();
    }

    // POST: Habitaciones/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id," +
              "IdTipoHabitacion," +
              "Piso," +
              "Ubicacion")]
        Habitacion habitacion)
    {
        habitacion.Habilitado = true;
        habitacion.Disponible = true;
        habitacion.IdTipoHabitacionNavigation = await _context.TipoHabitacion.FindAsync(habitacion.IdTipoHabitacion);
        // Verificar si existe habitación con el mismo id
        if (_context.Habitacion.Any(h => h.Id == habitacion.Id))
        {
            Console.WriteLine("Ya existe una habitación con el mismo id.");
            ModelState.AddModelError("Id", "Ya existe una habitación con el mismo id.");
        }
        // Verificar si existen errores en el modelo
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Existen errores en el modelo.");
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                }
            }
            ViewBag.TiposHabitacion = await GetTiposHabitacion();
            return View(habitacion);
        }
        // Agregar la habitación a la base de datos
        _context.Add(habitacion);
        await _context.SaveChangesAsync();
        // Redireccionar a la lista de habitaciones
        return RedirectToAction(nameof(Index));
    }

    // GET: Habitaciones/Edit/Id
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var habitacion = await _context.Habitacion.FindAsync(id);
        if (habitacion == null)
        {
            return NotFound();
        }
        ViewBag.TiposHabitacion = await GetTiposHabitacion();
        return View(habitacion);
    }

    // POST: Habitaciones/Edit/Id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id," +
              "IdTipoHabitacion," +
              "Piso," +
              "Ubicacion," +
              "Habilitado")]
        Habitacion habitacion)
    {
        if (id != habitacion.Id)
        {
            return NotFound();
        }
        // Verificar si existen errores en el modelo
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Existen errores en el modelo.");
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                }
            }
            ViewBag.TiposHabitacion = await GetTiposHabitacion();
            return View(habitacion);
        }
        // Actualizar la habitación en la base de datos
        _context.Update(habitacion);
        await _context.SaveChangesAsync();
        // Redireccionar a la lista de habitaciones
        return RedirectToAction(nameof(Index));
    }
    
    // GET: Habitaciones/Delete/Id
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var habitacion = await _context.Habitacion.FindAsync(id);
        if (habitacion == null)
        {
            return NotFound();
        }
        ViewBag.TiposHabitacion = await GetTiposHabitacion();
        return View(habitacion);
    }

    // POST: Habitaciones/Delete/Id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var habitacion = await _context.Habitacion.FindAsync(id);
        _context.Habitacion.Remove(habitacion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}