using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using BlazorVentas.Data;
using BlazorVentas.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Database - opcional, solo si hay connection string configurado
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<CommerceDbContext>(options =>
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(CommerceDbContext).Assembly.FullName);
        }));
}
else
{
    // Usar base de datos en memoria si no hay connection string
    //builder.Services.AddDbContext<CommerceDbContext>(options =>
    //    options.UseInMemoryDatabase("BlazorVentas"));
}

builder.Services.AddSingleton<CommerceService>(sp => new CommerceService(sp));
builder.Services.AddScoped<TenantState>();
builder.Services.AddScoped<BlazorVentas.Services.ImageService>();
builder.Services.AddScoped<BlazorVentas.Services.SessionService>();
builder.Services.AddScoped<BlazorVentas.Services.AuthService>();
builder.Services.AddScoped<BlazorVentas.Services.BitacoraService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Inicializar usuario por defecto (solo si hay BD configurada)
try
{
    var connectionString1 = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrEmpty(connectionString))
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
            var authService = scope.ServiceProvider.GetRequiredService<BlazorVentas.Services.AuthService>();
            
            // Crear usuario fer/fer si no existe
            try
            {
                await db.Database.EnsureCreatedAsync();
                var usuarioExists = await db.Usuarios.AnyAsync(u => u.Login == "fer" || u.Email == "fer");
                if (!usuarioExists)
                {
                    await authService.CreateUserAsync("fer@example.com", "fer", "fer", "Fernando", null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
            }
        }
    }
}
catch (Exception ex)
{
    // Log error pero no detener la aplicación
    Console.WriteLine($"Error al inicializar usuario: {ex.Message}");
}

app.Run();
