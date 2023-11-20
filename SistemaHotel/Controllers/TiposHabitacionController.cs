using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class TiposHabitacionController : Controller
    {
        private readonly Database _context;

        public TiposHabitacionController(Database context)
        {
            _context = context;
        }

        // GET: TiposHabitacion
        public async Task<IActionResult> Index()
        {
              return _context.TipoHabitacion != null ? 
                          View(await _context.TipoHabitacion.ToListAsync()) :
                          Problem("Entity set 'Database.TipoHabitacion'  is null.");
        }

        // GET: TiposHabitacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoHabitacion == null)
            {
                return NotFound();
            }

            var tipoHabitacion = await _context.TipoHabitacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }

            return View(tipoHabitacion);
        }

        // GET: TiposHabitacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposHabitacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,HuespedesPermitidos,PrecioNoche")] TipoHabitacion tipoHabitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoHabitacion);
        }

        // GET: TiposHabitacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoHabitacion == null)
            {
                return NotFound();
            }

            var tipoHabitacion = await _context.TipoHabitacion.FindAsync(id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }
            return View(tipoHabitacion);
        }

        // POST: TiposHabitacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,HuespedesPermitidos,PrecioNoche")] TipoHabitacion tipoHabitacion)
        {
            if (id != tipoHabitacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoHabitacionExists(tipoHabitacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoHabitacion);
        }

        // GET: TiposHabitacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoHabitacion == null)
            {
                return NotFound();
            }

            var tipoHabitacion = await _context.TipoHabitacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }

            return View(tipoHabitacion);
        }

        // POST: TiposHabitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoHabitacion == null)
            {
                return Problem("Entity set 'Database.TipoHabitacion'  is null.");
            }
            var tipoHabitacion = await _context.TipoHabitacion.FindAsync(id);
            if (tipoHabitacion != null)
            {
                _context.TipoHabitacion.Remove(tipoHabitacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoHabitacionExists(int id)
        {
          return (_context.TipoHabitacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
