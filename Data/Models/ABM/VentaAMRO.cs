namespace BlazorVentas.Data.Models.ABM;

/// <summary>
/// Cabecera de venta - Tabla AMRO_Ventas
/// </summary>
public class VentaAMRO
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    
    // Comprobante (FK a AMRO_Comprobantes)
    public int? ComprobanteId { get; set; }
    public string CodigoComprobante { get; set; } = string.Empty;
    public string NumeroComprobante { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public DateTime? FechaVencimiento { get; set; }
    
    // Cliente
    public int ClienteId { get; set; }
    public int CodigoCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public int? SucursalId { get; set; }
    public string? NombreSucursal { get; set; }
    
    // Vendedor
    public int? VendedorId { get; set; }
    public string? NombreVendedor { get; set; }
    
    // Totales
    public decimal Subtotal { get; set; }
    public decimal TotalDescuentos { get; set; }
    public decimal DescuentoAdicionalPorcentaje { get; set; }
    public decimal DescuentoAdicionalMonto { get; set; }
    public decimal TotalIVA { get; set; }
    public decimal TotalPercepciones { get; set; }
    public decimal Total { get; set; }
    
    // Estado
    public string Estado { get; set; } = "Cargado"; // Cargado, Eliminada, Cancelada
    public bool Anulado { get; set; }
    
    // Auditoría
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioId { get; set; }
    
    // Relaciones
    public ICollection<VentaDetalleAMRO> Detalles { get; set; } = new List<VentaDetalleAMRO>();
    public ICollection<VentaPercepcionAMRO> Percepciones { get; set; } = new List<VentaPercepcionAMRO>();
}

/// <summary>
/// Detalle de venta - Tabla AMRO_Ventas_Detalle
/// </summary>
public class VentaDetalleAMRO
{
    public int Id { get; set; }
    
    // Relación con cabecera
    public int VentaId { get; set; }
    public VentaAMRO? Venta { get; set; }
    
    // Artículo
    public int ArticuloId { get; set; }
    public int CodigoArticulo { get; set; }
    public string DescripcionArticulo { get; set; } = string.Empty;
    public string? DescripcionAdicional { get; set; }
    
    // Cantidades y precios
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int ListaPrecio { get; set; } = 1;
    
    // Descuentos (de oferta)
    public decimal DescuentoPorcentaje { get; set; }
    public decimal DescuentoMonto { get; set; }
    public string? OfertaNombre { get; set; }
    public bool EsOfertaLibre { get; set; }
    
    // IVA
    public decimal IVAPorcentaje { get; set; } = 21m;
    public decimal IVAMonto { get; set; }
    
    // Subtotales de línea
    public decimal Subtotal { get; set; }
    public decimal SubtotalConIVA { get; set; }
    
    // Orden
    public int NumeroLinea { get; set; }
}

/// <summary>
/// Percepciones aplicadas a una venta - Tabla AMRO_Ventas_Percepciones
/// </summary>
public class VentaPercepcionAMRO
{
    public int Id { get; set; }
    
    // Relación con cabecera
    public int VentaId { get; set; }
    public VentaAMRO? Venta { get; set; }
    
    // Percepción
    public int PercepcionId { get; set; }
    public string NombrePercepcion { get; set; } = string.Empty;
    public string? TipoAfip { get; set; }
    
    // Valores
    public decimal Porcentaje { get; set; }
    public decimal BaseImponible { get; set; }
    public decimal Monto { get; set; }
}
