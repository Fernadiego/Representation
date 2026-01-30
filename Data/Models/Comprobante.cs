using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data.Models;

/// <summary>
/// Comprobantes - Tabla AMRO_Comprobantes
/// </summary>
public class Comprobante
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El código es requerido")]
    [StringLength(10, ErrorMessage = "El código no puede exceder 10 caracteres")]
    public string Codigo { get; set; } = string.Empty; // Cod (ej: FAA, FAB, NCA, etc.)
    
    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
    public string Descripcion { get; set; } = string.Empty; // Descripción completa
    
    [StringLength(1)]
    public string? Letra { get; set; } // A, B, C
    
    [StringLength(10)]
    public string? CodigoAfip { get; set; } // Código AFIP (001, 006, etc.)
    
    public bool RequiereCuit { get; set; } = true;
    public bool RequiereStock { get; set; } = true;
    public bool Afectacc { get; set; } = true; // Afecta Cuenta Corriente
    public int SignoCC { get; set; } = 1; // 1 = Suma al saldo (Facturas), -1 = Resta al saldo (NC, Recibos)
    public bool Activo { get; set; } = true;
    
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaModificacion { get; set; }
}

