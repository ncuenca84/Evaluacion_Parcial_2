using Evaluacion_Parcial_2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Conexión a SQL Server desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json");
}

// 2) Registrar el DbContext (con Identity)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 3) Identity (login/registro). Recomendación: en modo evaluación, puedes dejar RequireConfirmedAccount = false
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // evita bloqueos por confirmación de correo
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// 4) MVC + Vistas (para Controllers/Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 5) Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity necesita autenticación + autorización
app.UseAuthentication();
app.UseAuthorization();

// 6) Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 7) Rutas Razor Pages (Identity UI)
app.MapRazorPages();

app.Run();