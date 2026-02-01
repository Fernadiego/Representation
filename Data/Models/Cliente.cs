using BlazorVentas.Data.Models.ABM;

namespace BlazorVentas.Data.Models;

public class Cliente
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    // Campos según especificación
    public int CodigoCliente { get; set; }
    public int CodigoSucursal { get; set; }
    public int CodigoParaMostrar { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string NombreSucursal { get; set; } = string.Empty;
    public string? DomicilioEntrega { get; set; }
    public string? DomicilioLegal { get; set; }
    public int? LocalidadId { get; set; }
    public Localidad? Localidad { get; set; }
    public int? ProvinciaId { get; set; }
    public Provincia? Provincia { get; set; }
    public int? CP { get; set; }
    public string? Telefono { get; set; }
    public string? Mail { get; set; }
    public string? Web { get; set; }
    public string? Contacto { get; set; }
    public string? CUIT { get; set; }
    public int ListaPrecio { get; set; } = 1; // Lista 1,2,3,4
    public int? CodigoDescuentoId { get; set; }
    public Descuento? CodigoDescuento { get; set; }
    
    // Descuento ABM (nuevo)
    public int? DescuentoABMId { get; set; }
    public DescuentoABM? DescuentoABM { get; set; }
    
    public int? TipoClienteId { get; set; }
    public TipoCliente? TipoCliente { get; set; }
    public int CondicionPago { get; set; } // Días
    public int? ZonaId { get; set; }
    public Zona? Zona { get; set; }
    public int? VendedorId { get; set; }
    public Vendedor? Vendedor { get; set; }
    public int? CobradorId { get; set; }
    public Cobrador? Cobrador { get; set; }
    public int? ClaseClienteId { get; set; }
    public ClaseCliente? ClaseCliente { get; set; }
    public DateTime? FechaUltimaCompra { get; set; }
    public DateTime FechaAlta { get; set; } = DateTime.Today;
    public bool Inhabilitado { get; set; }
    public string? MensajeSobreCliente { get; set; }

    // Precios especiales por cliente
    public List<ClientePrecio> PreciosEspeciales { get; set; } = new();
    public List<Venta> Ventas { get; set; } = new();
}

