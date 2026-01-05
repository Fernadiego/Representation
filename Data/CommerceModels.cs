using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data;

public class Company
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Color { get; set; }
}

public class Supplier
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(80)]
    public string ContactName { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}

public class Vendor
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 100)]
    public decimal CommissionRate { get; set; } = 5;
}

public class CustomerBranch
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    public string? DeliveryAddress { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    // Códigos
    public int CodigoCliente { get; set; }
    public int CodigoSucursal { get; set; }
    public int CodigoParaMostrar { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty; // NombreCliente

    [StringLength(200)]
    public string? NombreSucursal { get; set; }

    public int? NumeroProveedor { get; set; }

    [StringLength(300)]
    public string? DomicilioEntrega { get; set; }

    [StringLength(300)]
    public string? DomicilioLegal { get; set; }

    // Localidad (ABM)
    public int? LocalidadId { get; set; }
    public string? LocalidadNombre { get; set; }

    // Provincia (ABM)
    public int? ProvinciaId { get; set; }
    public string? ProvinciaNombre { get; set; }

    public int? CP { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(200)]
    public string? Web { get; set; }

    [StringLength(200)]
    public string? Contacto { get; set; }
    
    [StringLength(20)]
    public string? Cuit { get; set; }

    // Lista de Precio (1, 2, 3, 4)
    public int ListaPrecio { get; set; } = 1;

    // Descuento (ABM)
    public int? CodigoDescuentoId { get; set; }
    public string? CodigoDescuentoNombre { get; set; }

    // Tipo Cliente (ABM)
    public int? TipoClienteId { get; set; }
    public string? TipoClienteNombre { get; set; }

    // Condición de Pago (días)
    public int CondicionPago { get; set; }

    // Zona de Venta (ABM)
    public int? ZonaId { get; set; }
    public string? ZonaNombre { get; set; }

    // Vendedor (ABM)
    public int? VendedorId { get; set; }
    public string? VendedorNombre { get; set; }

    // Cobrador (ABM)
    public int? CobradorId { get; set; }
    public string? CobradorNombre { get; set; }

    // Clase de Cliente (ABM)
    public int? ClaseClienteId { get; set; }
    public string? ClaseClienteNombre { get; set; }

    // Fechas
    public DateTime? FechaUltimaCompra { get; set; }
    public DateTime FechaAlta { get; set; } = DateTime.Today;

    // Estado
    public bool Inhabilitado { get; set; }
    public string? MensajeSobreCliente { get; set; }
    public bool TieneMensaje { get; set; }
    
    [StringLength(200)]
    public string? Address { get; set; }
    
    public List<CustomerBranch> Branches { get; set; } = new();
}

public class Product
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    // Códigos
    public int CodigoArticulo { get; set; }
    public int CodigoParaMostrar { get; set; }

    [Required, StringLength(300)]
    public string Name { get; set; } = string.Empty; // Descripción

    [StringLength(30)]
    public string? Sku { get; set; }

    [StringLength(50)]
    public string? Category { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, 999999)]
    public decimal UnitPrice { get; set; } = 1;

    [Range(0, 999999)]
    public int Stock { get; set; } = 0;

    [Range(0, 999999)]
    public int MinStock { get; set; } = 5;

    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    // Marca (ABM)
    public int? MarcaId { get; set; }
    public string? MarcaNombre { get; set; }

    // Origen/País (ABM)
    public int? OrigenId { get; set; }
    public string? OrigenNombre { get; set; }

    // Pesos
    public decimal? PesoNeto { get; set; } // Gramos
    public decimal? PesoEscurrido { get; set; } // Gramos

    // Tipo Envase (ABM)
    public int? TipoEnvaseId { get; set; }
    public string? TipoEnvaseNombre { get; set; }

    // Unidades y códigos de barras
    public int? UnidadXBulto { get; set; }
    public long? EAUN13 { get; set; }
    public long? DUN14 { get; set; }

    // Listas de precios
    public decimal PrecioLista1 { get; set; }
    public decimal PrecioLista2 { get; set; }
    public decimal PrecioLista3 { get; set; }
    public decimal PrecioLista4 { get; set; }

    // Tamaño Unidad (cm)
    public decimal? TamañoUnidadAlto { get; set; }
    public decimal? TamañoUnidadAncho { get; set; }
    public decimal? TamañoUnidadProfundo { get; set; }

    // Tamaño Bulto (cm)
    public decimal? TamañoBultoAlto { get; set; }
    public decimal? TamañoBultoAncho { get; set; }
    public decimal? TamañoBultoProfundo { get; set; }

    // Tamaño Palet (cm)
    public decimal? TamañoPaletAlto { get; set; }
    public decimal? TamañoPaletAncho { get; set; }
    public decimal? TamañoPaletProfundo { get; set; }

    // Pesos adicionales
    public decimal? PesoBulto { get; set; }
    public decimal? PesoPalet { get; set; }

    // Bultos
    public int? BultosXCamada { get; set; }
    public int? BultosXPalet { get; set; }

    // Estado
    public bool Inhabilitado { get; set; }
    public string? MensajeSobreArticulo { get; set; }
    public bool TieneMensaje { get; set; }

    // Imagen del producto
    public string? ImagePath { get; set; }
}

// Clases auxiliares para ABMs en memoria
public class MarcaItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class PaisItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class TipoEnvaseItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

// ABMs para Clientes
public class LocalidadItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int ProvinciaId { get; set; }
}

public class ProvinciaItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class DescuentoItem
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Porcentaje { get; set; }
}

public class TipoClienteItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class ZonaItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class VendedorItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class CobradorItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class ClaseClienteItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

// ABM para Comprobantes
public class ComprobanteItem
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Tipo { get; set; }
    public string Numeracion { get; set; } = string.Empty;
}

// ABM para Tipos Comprobantes
public class TipoComprobanteItem
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class SaleLine
{
    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Range(1, 9999)]
    public int Quantity { get; set; } = 1;

    [Range(0, 999999)]
    public decimal UnitPrice { get; set; }

    public decimal LineTotal => Quantity * UnitPrice;
}

public class Sale
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    [Required]
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    [Required]
    public int VendorId { get; set; }

    public Vendor? Vendor { get; set; }

    public string TipoComprobante { get; set; } = "NPV"; // NPV o Factura
    public string NumeroComprobante { get; set; } = string.Empty;
    public int? SucursalId { get; set; }
    public DateTime? Vencimiento { get; set; }
    public string? ComprobOriginado { get; set; }
    public string? RemitoAsociado { get; set; }
    public string Estado { get; set; } = "Cargado"; // Cargado, Eliminada, Cancelada

    public List<SaleLine> Lines { get; set; } = new();

    public decimal Total => Lines.Sum(l => l.LineTotal);
}

public class CommerceSnapshot
{
    public decimal InventoryValue { get; set; }
    public decimal MonthlySales { get; set; }
    public int ActiveVendors { get; set; }
    public int CustomerCount { get; set; }
    public decimal AverageTicket { get; set; }
}

