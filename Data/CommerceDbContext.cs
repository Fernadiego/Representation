using Microsoft.EntityFrameworkCore;
using BlazorVentas.Data.Models;
using BlazorVentas.Data.Models.ABM;

namespace BlazorVentas.Data;

public class CommerceDbContext : DbContext
{
    public CommerceDbContext(DbContextOptions<CommerceDbContext> options) : base(options)
    {
    }

    // Empresas
    public DbSet<Company> Companies { get; set; }

    // ABMs
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Localidad> Localidades { get; set; }
    public DbSet<Descuento> Descuentos { get; set; }
    public DbSet<TipoCliente> TipoClientes { get; set; }
    public DbSet<Zona> Zonas { get; set; }
    public DbSet<ClaseCliente> ClaseClientes { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<TipoEnvase> TipoEnvases { get; set; }
    public DbSet<Cobrador> Cobradores { get; set; }
    public DbSet<Comprobante> Comprobantes { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }

    // Entidades principales
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<VentaLinea> VentaLineas { get; set; }
    public DbSet<ClientePrecio> ClientePrecios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Company
        modelBuilder.Entity<Company>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });

        // Configuración de Cliente
        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasKey(c => c.Id);
            e.HasIndex(c => new { c.CompanyId, c.CodigoCliente, c.CodigoSucursal }).IsUnique();
            e.Property(c => c.NombreCliente).IsRequired().HasMaxLength(200);
            e.Property(c => c.NombreSucursal).HasMaxLength(200);
            e.Property(c => c.DomicilioEntrega).HasMaxLength(300);
            e.Property(c => c.DomicilioLegal).HasMaxLength(300);
            e.Property(c => c.Telefono).HasMaxLength(50);
            e.Property(c => c.Mail).HasMaxLength(100);
            e.Property(c => c.Web).HasMaxLength(200);
            e.Property(c => c.Contacto).HasMaxLength(200);
            e.Property(c => c.CUIT).HasMaxLength(20);
            e.Property(c => c.MensajeSobreCliente).HasMaxLength(500);

            e.HasOne(c => c.Company).WithMany(c => c.Clientes).HasForeignKey(c => c.CompanyId);
            e.HasOne(c => c.Localidad).WithMany().HasForeignKey(c => c.LocalidadId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Provincia).WithMany().HasForeignKey(c => c.ProvinciaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.CodigoDescuento).WithMany().HasForeignKey(c => c.CodigoDescuentoId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.TipoCliente).WithMany().HasForeignKey(c => c.TipoClienteId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Zona).WithMany().HasForeignKey(c => c.ZonaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Vendedor).WithMany().HasForeignKey(c => c.VendedorId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Cobrador).WithMany().HasForeignKey(c => c.CobradorId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.ClaseCliente).WithMany().HasForeignKey(c => c.ClaseClienteId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de Articulo
        modelBuilder.Entity<Articulo>(e =>
        {
            e.HasKey(a => a.Id);
            e.HasIndex(a => new { a.CompanyId, a.CodigoArticulo }).IsUnique();
            e.Property(a => a.Descripcion).IsRequired().HasMaxLength(300);
            e.Property(a => a.MensajeSobreArticulo).HasMaxLength(500);

            e.HasOne(a => a.Company).WithMany(c => c.Articulos).HasForeignKey(a => a.CompanyId);
            e.HasOne(a => a.Marca).WithMany().HasForeignKey(a => a.MarcaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(a => a.Origen).WithMany().HasForeignKey(a => a.OrigenId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(a => a.TipoEnvase).WithMany().HasForeignKey(a => a.TipoEnvaseId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de Venta
        modelBuilder.Entity<Venta>(e =>
        {
            e.HasKey(v => v.Id);
            e.Property(v => v.NumeroComprobante).HasMaxLength(50);

            e.HasOne(v => v.Company).WithMany(c => c.Ventas).HasForeignKey(v => v.CompanyId);
            e.HasOne(v => v.Cliente).WithMany(c => c.Ventas).HasForeignKey(v => v.ClienteId);
            e.HasOne(v => v.Vendedor).WithMany(v => v.Ventas).HasForeignKey(v => v.VendedorId);
            e.HasOne(v => v.Comprobante).WithMany(c => c.Ventas).HasForeignKey(v => v.ComprobanteId);
        });

        // Configuración de VentaLinea
        modelBuilder.Entity<VentaLinea>(e =>
        {
            e.HasKey(vl => vl.Id);
            e.HasOne(vl => vl.Venta).WithMany(v => v.Lineas).HasForeignKey(vl => vl.VentaId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(vl => vl.Articulo).WithMany(a => a.VentasLineas).HasForeignKey(vl => vl.ArticuloId);
        });

        // Configuración de ClientePrecio
        modelBuilder.Entity<ClientePrecio>(e =>
        {
            e.HasKey(cp => cp.Id);
            e.HasOne(cp => cp.Cliente).WithMany(c => c.PreciosEspeciales).HasForeignKey(cp => cp.ClienteId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(cp => cp.Articulo).WithMany(a => a.PreciosEspeciales).HasForeignKey(cp => cp.ArticuloId);
        });

        // Configuración de ABMs
        modelBuilder.Entity<Provincia>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Localidad>(e =>
        {
            e.HasKey(l => l.Id);
            e.Property(l => l.Nombre).IsRequired().HasMaxLength(100);
            e.HasOne(l => l.Provincia).WithMany(p => p.Localidades).HasForeignKey(l => l.ProvinciaId);
        });

        modelBuilder.Entity<Descuento>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Codigo).IsRequired().HasMaxLength(20);
            e.Property(d => d.Descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<TipoCliente>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Zona>(e =>
        {
            e.HasKey(z => z.Id);
            e.Property(z => z.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<ClaseCliente>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Marca>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Pais>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<TipoEnvase>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Nombre).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Cobrador>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Comprobante>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Codigo).IsRequired().HasMaxLength(20);
            e.Property(c => c.Descripcion).HasMaxLength(200);
            e.Property(c => c.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<Vendedor>(e =>
        {
            e.HasKey(v => v.Id);
            e.Property(v => v.Nombre).IsRequired().HasMaxLength(200);
        });
    }
}

