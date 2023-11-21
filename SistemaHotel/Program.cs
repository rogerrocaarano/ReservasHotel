using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Data;
using SistemaHotel.Models;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("IdentityDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDatabaseConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionDb = Environment.GetEnvironmentVariable("ReservasHotelDb");
var connectionIdentityDb = Environment.GetEnvironmentVariable("ReservasHotelIdentityDb");

Console.WriteLine("Conectando la base de datos");
builder.Services.AddDbContext<Database>(options => options.UseNpgsql(connectionDb));

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityDatabase>();
builder.Services.AddDbContext<IdentityDatabase>(options => options.UseNpgsql(connectionIdentityDb));

// // Configuración del servicio de Identity
// builder.Services.AddIdentity<Usuario,  IdentityRole<Guid>>()
//     .AddEntityFrameworkStores<IdentityDatabase>()
//     .AddDefaultTokenProviders();

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