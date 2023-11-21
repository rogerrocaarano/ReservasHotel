using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;

// Definir parámetros de construcción de la aplicación
var builder = WebApplication.CreateBuilder(args);

// SERVICIOS

// Añadir los controladores y sus respectivas vistas
builder.Services.AddControllersWithViews();

// Añadir las bases de datos
// Definir las cadenas de conexión de las bases de datos desde variables de entorno
var connectionDb = Environment.GetEnvironmentVariable("ReservasHotelDb");
var connectionIdentityDb = Environment.GetEnvironmentVariable("ReservasHotelIdentityDb");
// Conectar base de datos de almacenamiento
builder.Services.AddDbContext<Database>(options =>
    options.UseNpgsql(connectionDb));
// Conectar base de datos de autenticación
builder.Services.AddDefaultIdentity<Usuario>(options =>
    options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityDatabase>();
builder.Services.AddDbContext<IdentityDatabase>(options =>
    options.UseNpgsql(connectionIdentityDb));

// Añadir soporte a RazorPages para la autenticación
builder.Services.AddRazorPages();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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