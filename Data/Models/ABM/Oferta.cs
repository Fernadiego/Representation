namespace BlazorVentas.Data.Models.ABM;

/// <summary>
/// Oferta: descuento aplicable a un artículo para un cliente específico
/// </summary>
public class Oferta
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    
    // Artículo al que aplica la oferta
    public int ArticuloId { get; set; }
    public Articulo? Articulo { get; set; }
    
    // Cliente al que aplica la oferta (null = aplica a todos)
    public int? ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    
    // Datos de la oferta
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal PorcentajeDescuento { get; set; }
    public decimal? MontoFijo { get; set; } // Alternativa: descuento en monto fijo
    
    // Vigencia
    public DateTime FechaInicio { get; set; } = DateTime.Today;
    public DateTime? FechaFin { get; set; }
    
    // Estado
    public bool Activo { get; set; } = true;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public DateTime? FechaModificacion { get; set; }
}
