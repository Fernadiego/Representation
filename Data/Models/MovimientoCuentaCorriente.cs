using System.ComponentModel.DataAnnotations;
using BlazorVentas.Data.Models.ABM;

namespace BlazorVentas.Data.Models;

/// <summary>
/// Movimientos de Cuenta Corriente de Clientes - Tabla AMRO_Movimientos_CC
/// </summary>
public class MovimientoCuentaCorriente
{
    public int Id { get; set; }
    
    public int CompanyId { get; set; }
    public int ClienteId { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    [Required]
    [StringLength(20)]
    public string TipoMovimiento { get; set; } = string.Empty; // VENTA, COBRANZA, NC, ND, AJUSTE
    
    [StringLength(10)]
    public string? CodigoComprobante { get; set; } // FAA, FAB, REC, NCA, etc.
    
    [StringLength(50)]
    public string? NumeroComprobante { get; set; }
    
    public int? VentaId { get; set; } // Referencia a AMRO_Ventas si aplica
    public int? CobranzaId { get; set; } // Referencia a AMRO_Cobranzas si aplica
    
    [StringLength(500)]
    public string? Descripcion { get; set; }
    
    public decimal Debe { get; set; } // Aumenta saldo (Facturas, ND)
    public decimal Haber { get; set; } // Disminuye saldo (NC, Recibos, Pagos)
    public decimal Saldo { get; set; } // Saldo acumulado después de este movimiento
    
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public int? UsuarioId { get; set; }
    
    // Navegación
    public virtual Cliente? Cliente { get; set; }
}

/// <summary>
/// Cobranza - Tabla AMRO_Cobranzas
/// </summary>
public class Cobranza
{
    public int Id { get; set; }
    
    public int CompanyId { get; set; }
    public int ClienteId { get; set; }
    public int? SucursalId { get; set; }
    public int? CobradorId { get; set; }
    
    [Required]
    [StringLength(10)]
    public string CodigoComprobante { get; set; } = "REC"; // REC = Recibo
    
    [StringLength(50)]
    public string? NumeroComprobante { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    [StringLength(200)]
    public string NombreCliente { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? NombreSucursal { get; set; }
    
    [StringLength(100)]
    public string? NombreCobrador { get; set; }
    
    public decimal TotalEfectivo { get; set; }
    public decimal TotalCheques { get; set; }
    public decimal TotalTransferencia { get; set; }
    public decimal TotalRetencion { get; set; }
    public decimal TotalOtros { get; set; }
    public decimal Total { get; set; }
    
    [StringLength(20)]
    public string Estado { get; set; } = "Activo"; // Activo, Anulado
    
    [StringLength(500)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioId { get; set; }
    
    // Navegación
    public virtual Cliente? Cliente { get; set; }
    public virtual Cobrador? Cobrador { get; set; }
    public virtual ICollection<CobranzaDetalle> Detalles { get; set; } = new List<CobranzaDetalle>();
    public virtual ICollection<CobranzaComprobante> ComprobantesAplicados { get; set; } = new List<CobranzaComprobante>();
}

/// <summary>
/// Detalle de Cobranza (formas de pago) - Tabla AMRO_Cobranzas_Detalle
/// </summary>
public class CobranzaDetalle
{
    public int Id { get; set; }
    
    public int CobranzaId { get; set; }
    
    [Required]
    [StringLength(20)]
    public string TipoPago { get; set; } = string.Empty; // EFECTIVO, CHEQUE, TRANSFERENCIA, RETENCION, OTRO
    
    [StringLength(200)]
    public string? Descripcion { get; set; }
    
    // Para cheques
    [StringLength(50)]
    public string? BancoCheque { get; set; }
    [StringLength(50)]
    public string? NumeroCheque { get; set; }
    public DateTime? FechaCheque { get; set; }
    
    // Para transferencias
    [StringLength(50)]
    public string? NumeroTransferencia { get; set; }
    [StringLength(50)]
    public string? BancoOrigen { get; set; }
    
    // Para retenciones
    [StringLength(50)]
    public string? TipoRetencion { get; set; } // IIBB, Ganancias, etc.
    [StringLength(50)]
    public string? NumeroRetencion { get; set; }
    
    public decimal Monto { get; set; }
    
    // Navegación
    public virtual Cobranza? Cobranza { get; set; }
}

/// <summary>
/// Comprobantes aplicados a la cobranza - Tabla AMRO_Cobranzas_Comprobantes
/// </summary>
public class CobranzaComprobante
{
    public int Id { get; set; }
    
    public int CobranzaId { get; set; }
    public int VentaId { get; set; } // Referencia a la venta/factura aplicada
    
    [StringLength(10)]
    public string CodigoComprobante { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? NumeroComprobante { get; set; }
    
    public DateTime FechaComprobante { get; set; }
    public decimal TotalComprobante { get; set; }
    public decimal MontoAplicado { get; set; }
    
    // Navegación
    public virtual Cobranza? Cobranza { get; set; }
    public virtual VentaAMRO? Venta { get; set; }
}

/// <summary>
/// DTO para facturas pendientes de pago
/// </summary>
public class VentaPendiente
{
    public int Id { get; set; }
    public string CodigoComprobante { get; set; } = "";
    public string? NumeroComprobante { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPagado { get; set; }
}
