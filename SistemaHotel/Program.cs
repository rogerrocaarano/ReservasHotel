using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SistemaHotel.Data;
using SistemaHotel.Models;
using SistemaHotel.Services;

var builder = WebApplication.CreateBuilder(args);

/*
 * VARIABLES DE ENTORNO
 * Deben estar definidas en el sistema operativo las siguientes variables de entorno:
 * - ReservasHotelDb: Cadena de conexión a la base de datos del sistema
 * - ReservasHotelEmailAccount: Cuenta de correo electrónico para el envío de mensajes
 * - ReservasHotelAdminEmail: Cuenta de correo del usuario ADministrador
 */
var sbAppDb = new StringBuilder();
sbAppDb.Append("Host=");
sbAppDb.Append(Environment.GetEnvironmentVariable("AppDbHost"));
sbAppDb.Append(";Database=");
sbAppDb.Append(Environment.GetEnvironmentVariable("AppDb"));
sbAppDb.Append(";Username=");
sbAppDb.Append(Environment.GetEnvironmentVariable("AppDbUser"));
sbAppDb.Append(";Password=");
sbAppDb.Append(Environment.GetEnvironmentVariable("AppDbPass"));
var connAppDb = sbAppDb.ToString();

var sbAuthDb = new StringBuilder();
sbAuthDb.Append("Host=");
sbAuthDb.Append(Environment.GetEnvironmentVariable("AuthDbHost"));
sbAuthDb.Append(";Database=");
sbAuthDb.Append(Environment.GetEnvironmentVariable("AuthDb"));
sbAuthDb.Append(";Username=");
sbAuthDb.Append(Environment.GetEnvironmentVariable("AuthDbUser"));
sbAuthDb.Append(";Password=");
sbAuthDb.Append(Environment.GetEnvironmentVariable("AuthDbPass"));
var connAuthDb = sbAuthDb.ToString();

var adminEmail = Environment.GetEnvironmentVariable("AdminEmail");

//Servicios de la aplicación
var services = builder.Services;
/*
 * - ControllersWithViews: Controladores con vistas
 * - Database: Base de datos del sistema
 * - IdentityDatabase: Base de datos de identidad
 * - Identity: Soporte para la autenticación de usuarios y roles
 * - RazorPages: Páginas Razor
 * - EmailSender: Servicio de envío de correos electrónicos
 */

services.AddDbContext<Database>(options => options.UseNpgsql(connAppDb));
services.AddDbContext<IdentityDatabase>(options => options.UseNpgsql(connAuthDb));


services.AddIdentity<Usuario, Rol>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<IdentityDatabase>()
    .AddDefaultTokenProviders();

services.AddControllersWithViews();
services.ConfigureApplicationCookie(options =>
    {
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
});

var emailSender = new EmailSender(
    host: Environment.GetEnvironmentVariable("EmailHost"),
    port: int.Parse(Environment.GetEnvironmentVariable("EmailPort")),
    useSsl: bool.Parse(Environment.GetEnvironmentVariable("EmailSsl")),
    username: Environment.GetEnvironmentVariable("EmailUser"),
    password: Environment.GetEnvironmentVariable("EmailPass")
);

services.AddTransient<IEmailSender>(sp => emailSender);

services.AddScoped<IClienteService, ClienteService>();
services.AddScoped<IBuscadorHabitaciones, BuscadorHabitaciones>();


var app = builder.Build();

// Crear roles de usuario si no existen
var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;

var identityDbContext = serviceProvider.GetRequiredService<IdentityDatabase>();
identityDbContext.Database.Migrate();

var roleManager = serviceProvider.GetRequiredService<RoleManager<Rol>>();
string[] roleNames = { "ADMINISTRADOR", "EMPLEADO", "CLIENTE", "CAJA", "RESERVA", "INVENTARIO" };
foreach (var roleName in roleNames)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new Rol { Name = roleName });
    }
}



// Asignar roles al usuario administrador
var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
if (adminEmail != null)
{
    var user = await userManager.FindByEmailAsync(adminEmail);
    var adminRoles = new List<string>
    {
        "ADMINISTRADOR",
        "EMPLEADO"
    };
    if (user != null) await userManager.AddToRolesAsync(user, adminRoles);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();