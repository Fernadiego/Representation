using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using BlazorVentas.Data;
using BlazorVentas.Data.Models;
using BlazorVentas.Data.Models.ABM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Database - SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json");
}

builder.Services.AddDbContext<CommerceDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrar IServiceScopeFactory para CommerceService
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

// Inicializar base de datos y datos semilla
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var authService = scope.ServiceProvider.GetRequiredService<BlazorVentas.Services.AuthService>();
        
        // Crear base de datos y tablas si no existen
        Console.WriteLine("Verificando base de datos...");
        await db.Database.EnsureCreatedAsync();
        Console.WriteLine("Base de datos verificada/creada correctamente.");
        
        // Seed de datos iniciales deshabilitado
        // await SeedDataAsync(db, authService);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}

app.Run();

// Método para sembrar datos iniciales
static async Task SeedDataAsync(CommerceDbContext db, BlazorVentas.Services.AuthService authService)
{
    // Crear usuario por defecto si no existe
    try
    {
        var usuarioExists = await db.Usuarios.AnyAsync(u => u.Login == "fer" || u.Email == "fer@example.com");
        if (!usuarioExists)
        {
            Console.WriteLine("Creando usuario por defecto 'fer'...");
            await authService.CreateUserAsync("fer@example.com", "fer", "fer", "Fernando", null);
            Console.WriteLine("Usuario 'fer' creado correctamente.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear usuario: {ex.Message}");
    }

    // Seed de Provincias
    if (!await db.Provincias.AnyAsync())
    {
        Console.WriteLine("Sembrando provincias...");
        db.Provincias.AddRange(
            new Provincia { Nombre = "Buenos Aires" },
            new Provincia { Nombre = "CABA" },
            new Provincia { Nombre = "Córdoba" },
            new Provincia { Nombre = "Santa Fe" },
            new Provincia { Nombre = "Mendoza" }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Localidades
    if (!await db.Localidades.AnyAsync())
    {
        Console.WriteLine("Sembrando localidades...");
        var buenosAires = await db.Provincias.FirstOrDefaultAsync(p => p.Nombre == "Buenos Aires");
        var caba = await db.Provincias.FirstOrDefaultAsync(p => p.Nombre == "CABA");
        var cordoba = await db.Provincias.FirstOrDefaultAsync(p => p.Nombre == "Córdoba");
        var santaFe = await db.Provincias.FirstOrDefaultAsync(p => p.Nombre == "Santa Fe");
        var mendoza = await db.Provincias.FirstOrDefaultAsync(p => p.Nombre == "Mendoza");

        db.Localidades.AddRange(
            new Localidad { Nombre = "La Plata", ProvinciaId = buenosAires?.Id },
            new Localidad { Nombre = "Mar del Plata", ProvinciaId = buenosAires?.Id },
            new Localidad { Nombre = "Quilmes", ProvinciaId = buenosAires?.Id },
            new Localidad { Nombre = "Palermo", ProvinciaId = caba?.Id },
            new Localidad { Nombre = "Recoleta", ProvinciaId = caba?.Id },
            new Localidad { Nombre = "Córdoba Capital", ProvinciaId = cordoba?.Id },
            new Localidad { Nombre = "Rosario", ProvinciaId = santaFe?.Id },
            new Localidad { Nombre = "Mendoza Capital", ProvinciaId = mendoza?.Id }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Marcas
    if (!await db.Marcas.AnyAsync())
    {
        Console.WriteLine("Sembrando marcas...");
        db.Marcas.AddRange(
            new Marca { Nombre = "La Campagnola" },
            new Marca { Nombre = "Arcor" },
            new Marca { Nombre = "Marolio" },
            new Marca { Nombre = "Knorr" },
            new Marca { Nombre = "Hellmann's" }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Países
    if (!await db.Paises.AnyAsync())
    {
        Console.WriteLine("Sembrando países...");
        db.Paises.AddRange(
            new Pais { Nombre = "Argentina" },
            new Pais { Nombre = "Brasil" },
            new Pais { Nombre = "Chile" },
            new Pais { Nombre = "Uruguay" },
            new Pais { Nombre = "Paraguay" },
            new Pais { Nombre = "España" },
            new Pais { Nombre = "Italia" }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Tipos de Envase
    if (!await db.TipoEnvases.AnyAsync())
    {
        Console.WriteLine("Sembrando tipos de envase...");
        db.TipoEnvases.AddRange(
            new TipoEnvase { Nombre = "Frasco" },
            new TipoEnvase { Nombre = "Lata" },
            new TipoEnvase { Nombre = "Caja" },
            new TipoEnvase { Nombre = "Bolsa" },
            new TipoEnvase { Nombre = "Botella" },
            new TipoEnvase { Nombre = "Sachet" }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Zonas
    if (!await db.Zonas.AnyAsync())
    {
        Console.WriteLine("Sembrando zonas...");
        db.Zonas.AddRange(
            new Zona { Codigo = "ZN", Descripcion = "Zona Norte", Activo = true },
            new Zona { Codigo = "ZS", Descripcion = "Zona Sur", Activo = true },
            new Zona { Codigo = "ZE", Descripcion = "Zona Este", Activo = true },
            new Zona { Codigo = "ZO", Descripcion = "Zona Oeste", Activo = true },
            new Zona { Codigo = "CE", Descripcion = "Centro", Activo = true }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Descuentos
    if (!await db.Descuentos.AnyAsync())
    {
        Console.WriteLine("Sembrando descuentos...");
        db.Descuentos.AddRange(
            new Descuento { Codigo = "DESC01", Descripcion = "Descuento 5%", Porcentaje = 5 },
            new Descuento { Codigo = "DESC02", Descripcion = "Descuento 10%", Porcentaje = 10 },
            new Descuento { Codigo = "DESC03", Descripcion = "Descuento 15%", Porcentaje = 15 },
            new Descuento { Codigo = "DESC04", Descripcion = "Descuento Mayorista", Porcentaje = 20 }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Tipos de Cliente
    if (!await db.TipoClientes.AnyAsync())
    {
        Console.WriteLine("Sembrando tipos de cliente...");
        db.TipoClientes.AddRange(
            new TipoCliente { Codigo = "RI", Descripcion = "Responsable Inscripto", CondicionIva = "Responsable Inscripto", RequiereCuit = true, Activo = true },
            new TipoCliente { Codigo = "MO", Descripcion = "Monotributista", CondicionIva = "Monotributo", RequiereCuit = true, Activo = true },
            new TipoCliente { Codigo = "EX", Descripcion = "Exento", CondicionIva = "Exento", RequiereCuit = true, Activo = true },
            new TipoCliente { Codigo = "CF", Descripcion = "Consumidor Final", CondicionIva = "Consumidor Final", RequiereCuit = false, Activo = true }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Clases de Cliente
    if (!await db.ClaseClientes.AnyAsync())
    {
        Console.WriteLine("Sembrando clases de cliente...");
        db.ClaseClientes.AddRange(
            new ClaseCliente { Nombre = "A - Premium" },
            new ClaseCliente { Nombre = "B - Estándar" },
            new ClaseCliente { Nombre = "C - Básico" }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Vendedores
    if (!await db.Vendedores.AnyAsync())
    {
        Console.WriteLine("Sembrando vendedores...");
        db.Vendedores.AddRange(
            new Vendedor { Codigo = "V001", Nombre = "Juan Pérez", ComisionPorcentaje = 3, Activo = true },
            new Vendedor { Codigo = "V002", Nombre = "María García", ComisionPorcentaje = 4, Activo = true },
            new Vendedor { Codigo = "V003", Nombre = "Carlos López", ComisionPorcentaje = 3.5m, Activo = true },
            new Vendedor { Codigo = "V004", Nombre = "Ana Martínez", ComisionPorcentaje = 4.5m, Activo = true }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Cobradores
    if (!await db.Cobradores.AnyAsync())
    {
        Console.WriteLine("Sembrando cobradores...");
        db.Cobradores.AddRange(
            new Cobrador { Codigo = "C001", Nombre = "Roberto Sánchez", Activo = true },
            new Cobrador { Codigo = "C002", Nombre = "Laura Fernández", Activo = true },
            new Cobrador { Codigo = "C003", Nombre = "Diego Rodríguez", Activo = true }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Tipos de Comprobante
    if (!await db.TiposComprobante.AnyAsync())
    {
        Console.WriteLine("Sembrando tipos de comprobante...");
        db.TiposComprobante.AddRange(
            new TipoComprobante { Codigo = "FA", Descripcion = "Factura A", Letra = "A", CodigoAfip = "001", RequiereCuit = true, Activo = true },
            new TipoComprobante { Codigo = "FB", Descripcion = "Factura B", Letra = "B", CodigoAfip = "006", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "FC", Descripcion = "Factura C", Letra = "C", CodigoAfip = "011", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "NCA", Descripcion = "Nota de Crédito A", Letra = "A", CodigoAfip = "003", RequiereCuit = true, Activo = true },
            new TipoComprobante { Codigo = "NCB", Descripcion = "Nota de Crédito B", Letra = "B", CodigoAfip = "008", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "NCC", Descripcion = "Nota de Crédito C", Letra = "C", CodigoAfip = "013", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "NDA", Descripcion = "Nota de Débito A", Letra = "A", CodigoAfip = "002", RequiereCuit = true, Activo = true },
            new TipoComprobante { Codigo = "NDB", Descripcion = "Nota de Débito B", Letra = "B", CodigoAfip = "007", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "NDC", Descripcion = "Nota de Débito C", Letra = "C", CodigoAfip = "012", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "NPV", Descripcion = "Nota de Pedido de Venta", Letra = "", CodigoAfip = "", RequiereCuit = false, Activo = true },
            new TipoComprobante { Codigo = "REM", Descripcion = "Remito", Letra = "R", CodigoAfip = "091", RequiereCuit = false, Activo = true }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Comprobantes (AMRO_Comprobantes)
    if (!await db.Comprobantes.AnyAsync())
    {
        Console.WriteLine("Sembrando comprobantes...");
        var now = DateTime.Now;
        db.Comprobantes.AddRange(
            // Facturas: Afectan CC, SignoCC = 1 (suman al saldo del cliente)
            new Comprobante { Codigo = "FAA", Descripcion = "FacturaA", Letra = "A", CodigoAfip = "001", RequiereCuit = true, RequiereStock = true, Afectacc = true, SignoCC = 1, FechaAlta = now },
            new Comprobante { Codigo = "FAB", Descripcion = "FacturaB", Letra = "B", CodigoAfip = "006", RequiereCuit = true, RequiereStock = true, Afectacc = true, SignoCC = 1, FechaAlta = now },
            new Comprobante { Codigo = "FAC", Descripcion = "FacturaC", Letra = "C", CodigoAfip = "011", RequiereCuit = false, RequiereStock = false, Afectacc = true, SignoCC = 1, FechaAlta = now },
            // Notas de Crédito: Afectan CC, SignoCC = -1 (restan del saldo del cliente)
            new Comprobante { Codigo = "NCA", Descripcion = "Nota de Crédito A", Letra = "A", CodigoAfip = "003", RequiereCuit = true, RequiereStock = true, Afectacc = true, SignoCC = -1, FechaAlta = now },
            new Comprobante { Codigo = "NCB", Descripcion = "Nota de Crédito B", Letra = "B", CodigoAfip = "008", RequiereCuit = true, RequiereStock = true, Afectacc = true, SignoCC = -1, FechaAlta = now },
            new Comprobante { Codigo = "NCC", Descripcion = "Nota de Crédito C", Letra = "C", CodigoAfip = "013", RequiereCuit = false, RequiereStock = true, Afectacc = true, SignoCC = -1, FechaAlta = now },
            // Notas de Débito: Afectan CC, SignoCC = 1 (suman al saldo del cliente)
            new Comprobante { Codigo = "NDA", Descripcion = "Nota de Débito A", Letra = "A", CodigoAfip = "002", RequiereCuit = true, RequiereStock = false, Afectacc = true, SignoCC = 1, FechaAlta = now },
            new Comprobante { Codigo = "NDB", Descripcion = "Nota de Débito B", Letra = "B", CodigoAfip = "007", RequiereCuit = true, RequiereStock = false, Afectacc = true, SignoCC = 1, FechaAlta = now },
            new Comprobante { Codigo = "NDC", Descripcion = "Nota de Débito C", Letra = "C", CodigoAfip = "012", RequiereCuit = false, RequiereStock = false, Afectacc = true, SignoCC = 1, FechaAlta = now },
            // Remitos: No afectan CC
            new Comprobante { Codigo = "REMA", Descripcion = "Remito A", Letra = "A", CodigoAfip = null, RequiereCuit = true, RequiereStock = true, Afectacc = false, SignoCC = 0, FechaAlta = now },
            new Comprobante { Codigo = "REMB", Descripcion = "Remito B", Letra = "B", CodigoAfip = null, RequiereCuit = true, RequiereStock = true, Afectacc = false, SignoCC = 0, FechaAlta = now },
            // Recibo: Afecta CC, SignoCC = -1 (reduce el saldo del cliente - cobranza)
            new Comprobante { Codigo = "REC", Descripcion = "Recibo", Letra = "", CodigoAfip = null, RequiereCuit = false, RequiereStock = false, Afectacc = true, SignoCC = -1, FechaAlta = now }
        );
        await db.SaveChangesAsync();
    }

    // Seed de Companies
    if (!await db.Companies.AnyAsync())
    {
        Console.WriteLine("Sembrando empresas...");
        db.Companies.AddRange(
            new BlazorVentas.Data.Models.Company { RazonSocial = "Cafeterías Regionales", Color = "#3498db", Activo = true, FechaAlta = DateTime.Now },
            new BlazorVentas.Data.Models.Company { RazonSocial = "Distribuciones Express", Color = "#e74c3c", Activo = true, FechaAlta = DateTime.Now }
        );
        await db.SaveChangesAsync();
    }

    Console.WriteLine("Datos iniciales sembrados correctamente.");
}
