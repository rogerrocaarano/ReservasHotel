using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SistemaHotel.Deserializers;

namespace SistemaHotel.Controllers
{
    [Authorize (Roles = "EMPLEADO")]
    public class HuespedesController : Controller
    {
        private readonly Database _context;
        private IWebHostEnvironment _hostingEnvironment;

        public HuespedesController(Database context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: Huespedes
        public async Task<IActionResult> Index()
        {
              return _context.Huesped != null ?
                          View(await _context.Huesped.ToListAsync()) :
                          Problem("Entity set 'Database.Huesped'  is null.");
        }

        // GET: Huespedes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Huesped == null)
            {
                return NotFound();
            }

            var huesped = await _context.Huesped
                .FirstOrDefaultAsync(m => m.Id == id);
            if (huesped == null)
            {
                return NotFound();
            }

            return View(huesped);
        }

        // GET: Huespedes/Create
        public IActionResult Create()
        {
            var jsonFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "json", "nacionalidades.json");
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var data = JsonConvert.DeserializeObject<PaisesData>(jsonData);
            // Convertir la lista de paises a un diccionario
            var dictPaises = data.Paises.ToDictionary(p => p.Nombre, p => p.Nacionalidad);
            var nacionalidades = dictPaises.Values.ToList();
            ViewBag.Nacionalidades = nacionalidades;
            return View();
        }

        // POST: Huespedes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,DocIdentidad,TipoDocIdentidad,Nacionalidad")] Huesped huesped)
        {
            if (ModelState.IsValid)
            {
                _context.Add(huesped);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(huesped);
        }

        // GET: Huespedes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Huesped == null)
            {
                return NotFound();
            }

            var huesped = await _context.Huesped.FindAsync(id);
            if (huesped == null)
            {
                return NotFound();
            }
            return View(huesped);
        }

        // POST: Huespedes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,DocIdentidad,TipoDocIdentidad,Nacionalidad")] Huesped huesped)
        {
            if (id != huesped.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(huesped);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuespedExists(huesped.Id))
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
            return View(huesped);
        }

        // GET: Huespedes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Huesped == null)
            {
                return NotFound();
            }

            var huesped = await _context.Huesped
                .FirstOrDefaultAsync(m => m.Id == id);
            if (huesped == null)
            {
                return NotFound();
            }

            return View(huesped);
        }

        // POST: Huespedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Huesped == null)
            {
                return Problem("Entity set 'Database.Huesped'  is null.");
            }
            var huesped = await _context.Huesped.FindAsync(id);
            if (huesped != null)
            {
                _context.Huesped.Remove(huesped);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HuespedExists(int id)
        {
          return (_context.Huesped?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
