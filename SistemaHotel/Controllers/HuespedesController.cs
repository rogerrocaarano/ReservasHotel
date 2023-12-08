using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaHotel.Data;
using SistemaHotel.Deserializers;
using SistemaHotel.Models;
using SistemaHotel.Services;

namespace SistemaHotel.Controllers;

[Authorize (Roles = "EMPLEADO")]
public class HuespedesController : Controller
{
    private readonly Database _context;
    private IWebHostEnvironment _hostingEnvironment;

    public HuespedesController(Database context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }



    // GET: /Huespedes
    /// <summary>
    /// Muestra la lista de huéspedes registrados en el sistema
    /// </summary>
    /// <returns>Vista Index.cshtml</returns>
    public async Task<IActionResult> Index()
    {
        var huespedes = await _context.Huesped.ToListAsync();
        return View(huespedes);
    }



    // GET: /Huespedes/Edit/Id
    /// <summary>
    /// Muestra el formulario de edición de un huésped específico
    /// </summary>
    /// <param name="id">Id del huesped</param>
    /// <returns>Vista Edit.cshtml</returns>
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var huesped = await _context.Huesped.FindAsync(id);
        if (huesped == null) return NotFound();
        return View(huesped);
    }



    // POST: /Huespedes/Edit/Id
    /// <summary>
    /// Envía el formulario de edición de un huésped específico
    /// </summary>
    /// <param name="id">Id del huesped.</param>
    /// <param name="huesped">Datos del huesped a modificar.</param>
    /// <returns>Vista Index.cshtml, Edit.cshtml si hay errores.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id," +
              "Nombres," +
              "Apellidos," +
              "DocIdentidad," +
              "TipoDocumento," +
              "Pais")]Huesped huesped)
    {
        if (id != huesped.Id) return NotFound();
        if (!await _context.Huesped.AnyAsync(h => h.Id == id)) return NotFound();
        huesped = NormalizeHuesped(huesped);
        if (!ModelState.IsValid)
        {
            UtilityFunctions.ConsolePrintModelErrors(ModelState);
            return View(huesped);
        }
        try
        {
            _context.Update(huesped);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View(huesped);
        }
        return RedirectToAction(nameof(Index));
    }



    // GET: /Huespedes/Create
    /// <summary>
    /// Muestra el formulario para crear un nuevo huésped
    /// </summary>
    /// <returns>Vista Create.cshtml</returns>
    public async Task<IActionResult> Create()
    {
        ViewBag.Paises = GetPaises();
        return View();
    }



    // POST: /Huespedes/Create
    /// <summary>
    /// Envía el formulario para crear un nuevo huésped
    /// </summary>
    /// <param name="huesped">Nuevo huésped</param>
    /// <returns>Vista Index.cshtml, Create.cshtml en caso de error.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Nombres," +
              "Apellidos," +
              "DocIdentidad," +
              "TipoDocIdentidad," +
              "Pais")]Huesped huesped)
    {
        // Normalizar los datos del huésped y verificar si existen errores en el modelo
        huesped = NormalizeHuesped(huesped);
        if (!ModelState.IsValid)
        {
            UtilityFunctions.ConsolePrintModelErrors(ModelState);
            ViewBag.Paises = GetPaises();
            return View(huesped);
        }
        try
        {
            _context.Add(huesped);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View(huesped);
        }
        return RedirectToAction(nameof(Index));
    }



    // GET: /Huespedes/Delete/Id
    /// <summary>
    /// Muestra el formulario de confirmación de eliminación de un huésped específico.
    /// </summary>
    /// <param name="id">Id del huesped</param>
    /// <returns>Vista Index.cshtml</returns>
    [Authorize (Roles = "ADMINISTRADOR")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var huesped = await _context.Huesped.FirstOrDefaultAsync(m => m.Id == id);
        if (huesped == null) return NotFound();
        return View(huesped);
    }



    // POST: /Huespedes/Delete/5
    /// <summary>
    /// Elimina un huésped específico de la base de datos
    /// </summary>
    /// <param name="id">Huesped a eliminar</param>
    /// <returns>Vista Index.cshtml</returns>
    [Authorize (Roles = "ADMINISTRADOR")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var huesped = await _context.Huesped.FindAsync(id);
        if (huesped == null) return NotFound();
        try
        {
            _context.Huesped.Remove(huesped);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View(huesped);
        }
        return RedirectToAction(nameof(Index));
    }



    /// <summary>
    /// Obtiene la lista de países desde el archivo nacionalidades.json
    /// </summary>
    /// <returns>Lista de países</returns>
    private List<string> GetPaises()
    {
        var jsonFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "nacionalidades.json");
        var jsonData = System.IO.File.ReadAllText(jsonFilePath);
        var data = JsonConvert.DeserializeObject<PaisesData>(jsonData);
        // Convertir la lista de paises a un diccionario
        var dictPaises = data.Paises.ToDictionary(p => p.Nombre, p => p.Nacionalidad);
        // Agregar al viewbag la lista de países
        return dictPaises.Keys.ToList();
    }



    /// <summary>
    /// Normaliza los datos de un huésped
    /// </summary>
    /// <param name="huesped">Objeto Huésped a normalizar</param>
    /// <returns>huesped normalizado</returns>
    private Huesped NormalizeHuesped(Huesped huesped)
    {
        huesped.Nombres = UtilityFunctions.NormalizeString(huesped.Nombres);
        huesped.Apellidos = UtilityFunctions.NormalizeString(huesped.Apellidos);
        huesped.DocIdentidad = UtilityFunctions.RemoveAllWhiteSpace(huesped.DocIdentidad);
        return huesped;
    }
}
