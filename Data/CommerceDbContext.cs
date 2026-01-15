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
    public DbSet<TipoComprobante> TiposComprobante { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    
    // Percepciones y Descuentos (AMRO)
    public DbSet<Percepcion> Percepciones { get; set; }
    public DbSet<PercepcionCliente> PercepcionClientes { get; set; }
    public DbSet<TipoPercepcion> TiposPercepcion { get; set; }
    public DbSet<DescuentoABM> DescuentosABM { get; set; }
    
    // Ofertas
    public DbSet<Oferta> Ofertas { get; set; }
    
    // Ventas AMRO (facturación)
    public DbSet<VentaAMRO> VentasAMRO { get; set; }
    public DbSet<VentaDetalleAMRO> VentasDetalleAMRO { get; set; }
    public DbSet<VentaPercepcionAMRO> VentasPercepcionesAMRO { get; set; }

    // Usuarios
    public DbSet<Usuario> Usuarios { get; set; }
    
    // Bitácora
    public DbSet<Bitacora> Bitacoras { get; set; }

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
            e.ToTable("Companies");
            e.HasKey(c => c.Id);
            e.Property(c => c.Codigo).HasMaxLength(20);
            e.Property(c => c.RazonSocial).IsRequired().HasMaxLength(200);
            e.Property(c => c.CUIT).HasMaxLength(20);
            e.Property(c => c.Direccion).HasMaxLength(300);
            e.Property(c => c.Telefono).HasMaxLength(50);
            e.Property(c => c.Email).HasMaxLength(100);
            e.Property(c => c.Color).HasMaxLength(20);
        });

        // Configuración de Cliente
        modelBuilder.Entity<Cliente>(e =>
        {
            e.ToTable("Clientes");
            e.HasKey(c => c.Id);
            e.Property(c => c.NombreCliente).IsRequired().HasMaxLength(200);
            e.Property(c => c.NombreSucursal).HasMaxLength(200);
            e.Property(c => c.DomicilioEntrega).HasMaxLength(300);
            e.Property(c => c.DomicilioLegal).HasMaxLength(300);
            e.Property(c => c.Telefono).HasMaxLength(50);
            e.Property(c => c.Mail).HasMaxLength(100);
            e.Property(c => c.Web).HasMaxLength(200);
            e.Property(c => c.Contacto).HasMaxLength(100);
            e.Property(c => c.CUIT).HasMaxLength(20);
            e.Property(c => c.MensajeSobreCliente).HasMaxLength(500);
            
            // Relaciones
            e.HasOne(c => c.Company).WithMany(c => c.Clientes).HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(c => c.Zona).WithMany().HasForeignKey(c => c.ZonaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Vendedor).WithMany().HasForeignKey(c => c.VendedorId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Cobrador).WithMany().HasForeignKey(c => c.CobradorId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Localidad).WithMany().HasForeignKey(c => c.LocalidadId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.Provincia).WithMany().HasForeignKey(c => c.ProvinciaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.CodigoDescuento).WithMany().HasForeignKey(c => c.CodigoDescuentoId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.TipoCliente).WithMany().HasForeignKey(c => c.TipoClienteId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(c => c.ClaseCliente).WithMany().HasForeignKey(c => c.ClaseClienteId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de Articulo
        modelBuilder.Entity<Articulo>(e =>
        {
            e.ToTable("Articulos");
            e.HasKey(a => a.Id);
            e.HasIndex(a => new { a.CompanyId, a.CodigoArticulo }).IsUnique();
            e.Property(a => a.Descripcion).IsRequired().HasMaxLength(300);
            // Configuración de precisión para decimales
            e.Property(a => a.PesoNeto).HasPrecision(18, 2);
            e.Property(a => a.PesoEscurrido).HasPrecision(18, 2);
            e.Property(a => a.PrecioLista1).HasPrecision(18, 2);
            e.Property(a => a.PrecioLista2).HasPrecision(18, 2);
            e.Property(a => a.PrecioLista3).HasPrecision(18, 2);
            e.Property(a => a.PrecioLista4).HasPrecision(18, 2);
            e.Property(a => a.TamañoUnidadAlto).HasPrecision(18, 2);
            e.Property(a => a.TamañoUnidadAncho).HasPrecision(18, 2);
            e.Property(a => a.TamañoUnidadProfundo).HasPrecision(18, 2);
            e.Property(a => a.TamañoBultoAlto).HasPrecision(18, 2);
            e.Property(a => a.TamañoBultoAncho).HasPrecision(18, 2);
            e.Property(a => a.TamañoBultoProfundo).HasPrecision(18, 2);
            e.Property(a => a.TamañoPaletAlto).HasPrecision(18, 2);
            e.Property(a => a.TamañoPaletAncho).HasPrecision(18, 2);
            e.Property(a => a.TamañoPaletProfundo).HasPrecision(18, 2);
            e.Property(a => a.PesoBulto).HasPrecision(18, 2);
            e.Property(a => a.PesoPalet).HasPrecision(18, 2);

            e.HasOne(a => a.Company).WithMany(c => c.Articulos).HasForeignKey(a => a.CompanyId);
            e.HasOne(a => a.Marca).WithMany().HasForeignKey(a => a.MarcaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(a => a.Origen).WithMany().HasForeignKey(a => a.OrigenId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(a => a.TipoEnvase).WithMany().HasForeignKey(a => a.TipoEnvaseId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de Venta
        modelBuilder.Entity<Venta>(e =>
        {
            e.ToTable("Ventas");
            e.HasKey(v => v.Id);
            e.Property(v => v.NumeroComprobante).HasMaxLength(50);
            e.Property(v => v.ImpuestoProvincial).HasPrecision(18, 2);
            e.Property(v => v.ImpuestoNacional).HasPrecision(18, 2);
            e.Property(v => v.Subtotal).HasPrecision(18, 2);
            e.Property(v => v.Total).HasPrecision(18, 2);

            e.HasOne(v => v.Company).WithMany(c => c.Ventas).HasForeignKey(v => v.CompanyId);
            e.HasOne(v => v.Cliente).WithMany(c => c.Ventas).HasForeignKey(v => v.ClienteId);
            e.HasOne(v => v.Vendedor).WithMany(v => v.Ventas).HasForeignKey(v => v.VendedorId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de VentaLinea
        modelBuilder.Entity<VentaLinea>(e =>
        {
            e.ToTable("VentaLineas");
            e.HasKey(vl => vl.Id);
            e.Property(vl => vl.PrecioUnitario).HasPrecision(18, 2);
            e.Property(vl => vl.Descuento).HasPrecision(18, 2);
            e.Property(vl => vl.Subtotal).HasPrecision(18, 2);
            e.Property(vl => vl.ImpuestoProvincial).HasPrecision(18, 2);
            e.Property(vl => vl.ImpuestoNacional).HasPrecision(18, 2);
            e.Property(vl => vl.Total).HasPrecision(18, 2);
            e.HasOne(vl => vl.Venta).WithMany(v => v.Lineas).HasForeignKey(vl => vl.VentaId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(vl => vl.Articulo).WithMany(a => a.VentasLineas).HasForeignKey(vl => vl.ArticuloId).OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de ClientePrecio
        modelBuilder.Entity<ClientePrecio>(e =>
        {
            e.ToTable("ClientePrecios");
            e.HasKey(cp => cp.Id);
            e.Property(cp => cp.Precio).HasPrecision(18, 2);
            e.HasOne(cp => cp.Cliente).WithMany(c => c.PreciosEspeciales).HasForeignKey(cp => cp.ClienteId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(cp => cp.Articulo).WithMany(a => a.PreciosEspeciales).HasForeignKey(cp => cp.ArticuloId);
        });

        // Configuración de ABMs - usando convenciones estándar
        modelBuilder.Entity<Provincia>(e =>
        {
            e.ToTable("Provincias");
            e.HasKey(p => p.Id);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Localidad>(e =>
        {
            e.ToTable("Localidades");
            e.HasKey(l => l.Id);
            e.Property(l => l.Nombre).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Descuento>(e =>
        {
            e.ToTable("Descuentos");
            e.HasKey(d => d.Id);
            e.Property(d => d.Codigo).IsRequired().HasMaxLength(20);
            e.Property(d => d.Descripcion).HasMaxLength(200);
            e.Property(d => d.Porcentaje).HasPrecision(18, 2);
        });

        modelBuilder.Entity<TipoCliente>(e =>
        {
            e.ToTable("TipoClientes");
            e.HasKey(t => t.Id);
            e.Property(t => t.Codigo).IsRequired().HasMaxLength(20);
            e.Property(t => t.Descripcion).IsRequired().HasMaxLength(100);
            e.Property(t => t.CondicionIva).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Zona>(e =>
        {
            e.ToTable("Zonas");
            e.HasKey(z => z.Id);
            e.Property(z => z.Codigo).IsRequired().HasMaxLength(20);
            e.Property(z => z.Descripcion).IsRequired().HasMaxLength(100);
            e.Property(z => z.Activo).IsRequired();
            e.Property(z => z.FechaAlta).IsRequired();
        });

        modelBuilder.Entity<ClaseCliente>(e =>
        {
            e.ToTable("ClaseClientes");
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Marca>(e =>
        {
            e.ToTable("Marcas");
            e.HasKey(m => m.Id);
            e.Property(m => m.Nombre).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Pais>(e =>
        {
            e.ToTable("Paises");
            e.HasKey(p => p.Id);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<TipoEnvase>(e =>
        {
            e.ToTable("TipoEnvases");
            e.HasKey(t => t.Id);
            e.Property(t => t.Nombre).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Cobrador>(e =>
        {
            e.ToTable("Cobradores");
            e.HasKey(c => c.Id);
            e.Property(c => c.Codigo).IsRequired().HasMaxLength(20);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            e.Property(c => c.Activo).IsRequired();
            e.Property(c => c.FechaAlta).IsRequired();
        });

        modelBuilder.Entity<Comprobante>(e =>
        {
            e.ToTable("AMRO_Comprobantes");
            e.HasKey(c => c.Id);
            e.Property(c => c.Codigo).IsRequired().HasMaxLength(10);
            e.Property(c => c.Descripcion).IsRequired().HasMaxLength(100);
            e.Property(c => c.Letra).HasMaxLength(1);
            e.Property(c => c.CodigoAfip).HasMaxLength(10);
            e.HasIndex(c => c.Codigo).IsUnique();
        });

        modelBuilder.Entity<TipoComprobante>(e =>
        {
            e.ToTable("TipoComprobante");
            e.HasKey(t => t.Id);
            e.Property(t => t.Codigo).IsRequired().HasMaxLength(20);
            e.Property(t => t.Descripcion).IsRequired().HasMaxLength(100);
            e.Property(t => t.Letra).HasMaxLength(1);
            e.Property(t => t.CodigoAfip).HasMaxLength(10);
        });

        modelBuilder.Entity<Vendedor>(e =>
        {
            e.ToTable("Vendedores");
            e.HasKey(v => v.Id);
            e.Property(v => v.Codigo).IsRequired().HasMaxLength(20);
            e.Property(v => v.Nombre).IsRequired().HasMaxLength(100);
            e.Property(v => v.ComisionPorcentaje).HasPrecision(18, 2);
            e.Property(v => v.Activo).IsRequired();
            e.Property(v => v.FechaAlta).IsRequired();
        });

        // Configuración de Oferta
        modelBuilder.Entity<Oferta>(e =>
        {
            e.ToTable("Ofertas");
            e.HasKey(o => o.Id);
            e.Property(o => o.Nombre).IsRequired().HasMaxLength(100);
            e.Property(o => o.Descripcion).HasMaxLength(500);
            e.Property(o => o.PorcentajeDescuento).HasPrecision(18, 2);
            e.Property(o => o.MontoFijo).HasPrecision(18, 2);
            e.HasOne(o => o.Articulo).WithMany().HasForeignKey(o => o.ArticuloId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(o => o.Cliente).WithMany().HasForeignKey(o => o.ClienteId).OnDelete(DeleteBehavior.SetNull);
            e.HasIndex(o => o.CompanyId);
            e.HasIndex(o => o.ArticuloId);
            e.HasIndex(o => o.ClienteId);
        });

        // Configuración de Usuario
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(u => u.Id);
            e.Property(u => u.Email).IsRequired().HasMaxLength(100);
            e.Property(u => u.Login).IsRequired().HasMaxLength(100);
            e.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
            e.Property(u => u.Nombre).HasMaxLength(200);
            e.Property(u => u.Apellido).HasMaxLength(200);
            
            e.HasIndex(u => u.Email).IsUnique();
            e.HasIndex(u => u.Login).IsUnique();
        });

        // Configuración de Bitácora
        modelBuilder.Entity<Bitacora>(e =>
        {
            e.ToTable("Bitacoras");
            e.HasKey(b => b.Id);
            e.Property(b => b.Modulo).IsRequired().HasMaxLength(100);
            e.Property(b => b.Accion).IsRequired().HasMaxLength(200);
            e.Property(b => b.Detalle).HasMaxLength(1000);
            e.HasOne(b => b.Usuario).WithMany().HasForeignKey(b => b.UsuarioId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(b => b.Fecha);
            e.HasIndex(b => b.UsuarioId);
        });

        // Configuración de Tipo Percepción
        modelBuilder.Entity<TipoPercepcion>(e =>
        {
            e.ToTable("AMRO_Tipo_Percepcion");
            e.HasKey(t => t.Id);
            e.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
        });

        // Configuración de Percepción
        modelBuilder.Entity<Percepcion>(e =>
        {
            e.ToTable("AMRO_Percepcion");
            e.HasKey(p => p.Id);
            e.Property(p => p.Codigo).IsRequired().HasMaxLength(50);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            e.Property(p => p.TipoAfip).HasMaxLength(50);
            e.Property(p => p.IvaPercepcion).HasMaxLength(50);
            e.Property(p => p.PercepMinima).HasPrecision(18, 2);
            e.Property(p => p.PorcentPercepcion).HasPrecision(18, 2);
            e.HasOne(p => p.TipoPercepcion).WithMany(t => t.Percepciones).HasForeignKey(p => p.TipoPercepcionId).OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de Percepción-Cliente
        modelBuilder.Entity<PercepcionCliente>(e =>
        {
            e.ToTable("AMRO_Percepcion_Cliente");
            e.HasKey(pc => pc.Id);
            e.HasOne(pc => pc.Percepcion).WithMany(p => p.PercepcionClientes).HasForeignKey(pc => pc.PercepcionId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(pc => pc.Cliente).WithMany().HasForeignKey(pc => pc.IdCliente).OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de Descuento ABM
        modelBuilder.Entity<DescuentoABM>(e =>
        {
            e.ToTable("AMRO_Descuentos");
            e.HasKey(d => d.Id);
            e.Property(d => d.Codigo).IsRequired().HasMaxLength(50);
            e.Property(d => d.Nombre).IsRequired().HasMaxLength(100);
            e.Property(d => d.Descripcion).HasMaxLength(500);
            e.Property(d => d.PorcentajeDescuento).HasColumnType("decimal(18,2)");
        });
        
        // Configuración de la relación Cliente -> DescuentoABM
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.DescuentoABM)
            .WithMany()
            .HasForeignKey(c => c.DescuentoABMId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Configuración de Ofertas
        modelBuilder.Entity<Oferta>(e =>
        {
            e.ToTable("Ofertas");
            e.HasKey(o => o.Id);
            e.Property(o => o.Nombre).IsRequired().HasMaxLength(100);
            e.Property(o => o.Descripcion).HasMaxLength(500);
            e.Property(o => o.PorcentajeDescuento).HasPrecision(18, 2);
            e.Property(o => o.MontoFijo).HasPrecision(18, 2);
            e.HasOne(o => o.Articulo).WithMany().HasForeignKey(o => o.ArticuloId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(o => o.Cliente).WithMany().HasForeignKey(o => o.ClienteId).OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configuración de VentaAMRO (Cabecera de factura)
        modelBuilder.Entity<VentaAMRO>(e =>
        {
            e.ToTable("AMRO_Ventas");
            e.HasKey(v => v.Id);
            e.Property(v => v.CodigoComprobante).IsRequired().HasMaxLength(10);
            e.Property(v => v.NumeroComprobante).HasMaxLength(50);
            e.Property(v => v.NombreCliente).IsRequired().HasMaxLength(200);
            e.Property(v => v.NombreSucursal).HasMaxLength(200);
            e.Property(v => v.NombreVendedor).HasMaxLength(100);
            e.Property(v => v.Estado).HasMaxLength(20);
            e.Property(v => v.Subtotal).HasPrecision(18, 2);
            e.Property(v => v.TotalDescuentos).HasPrecision(18, 2);
            e.Property(v => v.TotalIVA).HasPrecision(18, 2);
            e.Property(v => v.TotalPercepciones).HasPrecision(18, 2);
            e.Property(v => v.Total).HasPrecision(18, 2);
            e.HasIndex(v => new { v.CompanyId, v.CodigoComprobante, v.NumeroComprobante });
            
            // FK a AMRO_Comprobantes
            e.HasOne<Comprobante>()
                .WithMany()
                .HasForeignKey(v => v.ComprobanteId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        // Configuración de VentaDetalleAMRO (Detalle de factura)
        modelBuilder.Entity<VentaDetalleAMRO>(e =>
        {
            e.ToTable("AMRO_Ventas_Detalle");
            e.HasKey(d => d.Id);
            e.Property(d => d.DescripcionArticulo).IsRequired().HasMaxLength(300);
            e.Property(d => d.DescripcionAdicional).HasMaxLength(500);
            e.Property(d => d.OfertaNombre).HasMaxLength(100);
            e.Property(d => d.PrecioUnitario).HasPrecision(18, 2);
            e.Property(d => d.DescuentoPorcentaje).HasPrecision(18, 2);
            e.Property(d => d.DescuentoMonto).HasPrecision(18, 2);
            e.Property(d => d.IVAPorcentaje).HasPrecision(18, 2);
            e.Property(d => d.IVAMonto).HasPrecision(18, 2);
            e.Property(d => d.Subtotal).HasPrecision(18, 2);
            e.Property(d => d.SubtotalConIVA).HasPrecision(18, 2);
            e.HasOne(d => d.Venta).WithMany(v => v.Detalles).HasForeignKey(d => d.VentaId).OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configuración de VentaPercepcionAMRO (Percepciones de factura)
        modelBuilder.Entity<VentaPercepcionAMRO>(e =>
        {
            e.ToTable("AMRO_Ventas_Percepciones");
            e.HasKey(p => p.Id);
            e.Property(p => p.NombrePercepcion).IsRequired().HasMaxLength(100);
            e.Property(p => p.TipoAfip).HasMaxLength(50);
            e.Property(p => p.Porcentaje).HasPrecision(18, 2);
            e.Property(p => p.BaseImponible).HasPrecision(18, 2);
            e.Property(p => p.Monto).HasPrecision(18, 2);
            e.HasOne(p => p.Venta).WithMany(v => v.Percepciones).HasForeignKey(p => p.VentaId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
