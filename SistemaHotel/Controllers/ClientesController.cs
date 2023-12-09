using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers;

[Authorize (Roles = "EMPLEADO")]
public class ClientesController : Controller
{
    private readonly Database _context;

    public ClientesController(Database context)
    {
        _context = context;
    }



    /// <summary>
    /// GET: Clientes
    /// </summary>
    /// <returns>
    /// Vista con la lista de clientes
    /// </returns>
    public async Task<IActionResult> Index()
    {
        ViewBag.controller = "CLiente";
        var clientes = new List<Cliente>();
        return View(clientes);
    }



    /// <summary>
    /// GET partial: Clientes/_ListaClientes
    /// </summary>
    /// <param name="busqueda">
    /// Texto a buscar en los clientes
    /// </param>
    /// <returns>
    /// Vista parcial con la lista de clientes
    /// </returns>
    public IActionResult BuscarClientes(string? busqueda)
    {
        var clientes = from c in _context.Cliente select c;
        if (!string.IsNullOrEmpty(busqueda))
        {
            clientes = clientes.Where(c =>
                EF.Functions.ILike(c.Id.ToString(), $"%{busqueda}%") ||
                EF.Functions.ILike(c.Nombres, $"%{busqueda}%") ||
                EF.Functions.ILike(c.Apellidos, $"%{busqueda}%") ||
                EF.Functions.ILike(c.RazonSocial, $"%{busqueda}%")
            );
        }
        return PartialView("Clientes/_ListaClientes", clientes.ToList());
    }



    /// <summary>
    /// GET: Clientes/Create
    /// </summary>
    /// <returns>
    /// Vista para crear un cliente
    /// </returns>
    public IActionResult Create()
    {
        return View();
    }



    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    /// <summary>
    /// POST: Clientes/Create
    /// </summary>
    /// <param name="cliente">
    /// Cliente a crear
    /// </param>
    /// <returns>
    /// Vista Index si el cliente se crea correctamente, sino la vista Create
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id," +
              "Nombres," +
              "Apellidos," +
              "Genero," +
              "RazonSocial," +
              "NroRazonSocial," +
              "Email," +
              "Telefono")] Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    // GET: Clientes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Cliente == null)
        {
            return NotFound();
        }

        var cliente = await _context.Cliente.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }



    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    /// <summary>
    /// POST: Clientes/Edit/5
    /// </summary>
    /// <param name="id">
    /// Id del cliente a editar
    /// </param>
    /// <param name="cliente">
    /// Datos del cliente a editar
    /// </param>
    /// <returns>
    /// Vista Index si el cliente se edita correctamente, sino la vista Edit
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id," +
              "Nombres," +
              "Apellidos," +
              "Genero," +
              "RazonSocial," +
              "NroRazonSocial," +
              "Email," +
              "Telefono")] Cliente cliente)
    {
        if (id != cliente.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
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
        return View(cliente);
    }



    /// <summary>
    /// GET: Clientes/Delete/5
    /// </summary>
    /// <param name="id">
    /// Id del cliente a eliminar
    /// </param>
    /// <returns>
    /// Vista para confirmar la eliminacion del cliente
    /// </returns>
    [Authorize (Roles = "ADMINISTRADOR")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Cliente == null)
        {
            return NotFound();
        }

        var cliente = await _context.Cliente
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }



    /// <summary>
    /// POST: Clientes/Delete/5
    /// </summary>
    /// <param name="id">
    /// Id del cliente a eliminar
    /// </param>
    /// <returns>
    /// Vista Index si el cliente se elimina correctamente
    /// </returns>
    [Authorize (Roles = "ADMINISTRADOR")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Cliente == null)
        {
            return Problem("Entity set 'Database.Cliente'  is null.");
        }
        var cliente = await _context.Cliente.FindAsync(id);
        if (cliente != null)
        {
            _context.Cliente.Remove(cliente);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    /// <summary>
    /// Verifica si existe un cliente con el id dado
    /// </summary>
    /// <param name="id">
    /// Id del cliente a verificar
    /// </param>
    /// <returns>
    /// True si existe un cliente con el id dado, sino false
    /// </returns>
    private bool ClienteExists(int id)
    {
      return (_context.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
