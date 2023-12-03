using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers;

[Authorize (Roles = "ADMINISTRADOR")]
public class GestionUsuariosController : Controller
{
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<Rol> _roleManager;
    private readonly SignInManager<Usuario> _signInManager;

    public GestionUsuariosController(
        UserManager<Usuario> userManager,
        RoleManager<Rol> roleManager,
        SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        // Obtener la lista de usuarios con rol de "EMPLEADO"
        var empleados = await _userManager.GetUsersInRoleAsync("EMPLEADO");
        // Pasar la lista de empleados a una viewBag
        ViewBag.Empleados = empleados;
        return View();
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var usuario = await _userManager.FindByIdAsync(id.ToString());
        if (usuario == null) return NotFound();
        var rolesDictionary = await GetRolesDictionary(usuario);
        ViewBag.Roles = rolesDictionary;
        return View(usuario);
    }

    private async Task<Dictionary<string, bool>> GetRolesDictionary(Usuario user)
    {
        // Obtener una lista de todos los roles
        var roles = await _roleManager.Roles.ToListAsync();
        // Quitar el rol de "EMPLEADO" y "CLIENTE" de la lista de roles
        roles.Remove(await _roleManager.FindByNameAsync("CLIENTE"));
        roles.Remove(await _roleManager.FindByNameAsync("EMPLEADO"));
        // Crear una lista de roles que el usuario tiene
        var rolesUsuario = await _userManager.GetRolesAsync(user);
        return roles.ToDictionary(
            rol => rol.Name,
            rol => rolesUsuario.Contains(rol.Name));
    }

    private async Task ModificarRoles(
        Usuario usuario,
        Dictionary<string, bool> roles)
    {
        roles.Add("EMPLEADO", true);
        foreach (var rol in roles)
        {
            if (rol.Value)
            { await _userManager.AddToRoleAsync(usuario, rol.Key); }
            else
            { await _userManager.RemoveFromRoleAsync(usuario, rol.Key); }
        }
        await _userManager.UpdateAsync(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        Guid id,
        [Bind("Id,Nombres,Apellidos,Email")] Usuario usuario,
        Dictionary<string, bool> roles)
    {

        var usuarioActual = await _userManager.FindByIdAsync(id.ToString());
        if (usuarioActual == null) return NotFound();
        if (ModelState.IsValid)
        {
            usuarioActual.Nombres = usuario.Nombres;
            usuarioActual.Apellidos = usuario.Apellidos;
            usuarioActual.Email = usuario.Email;
            try
            {
                await _userManager.UpdateAsync(usuarioActual);
                await ModificarRoles(usuarioActual, roles);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        // Desloguear al usuario actual antes de redirigir al formulario de registro
        // _signInManager.SignOutAsync();

        // Redirigir a la página de registro con el rol "EMPLEADO" como parámetro
        return Redirect($"/Identity/Account/Register?rol=EMPLEADO");
    }

}