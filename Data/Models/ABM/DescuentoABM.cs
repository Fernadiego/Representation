namespace BlazorVentas.Data.Models.ABM;

/// <summary>
/// Descuentos que se pueden aplicar a clientes
/// Tabla: AMRO_Descuentos
/// </summary>
public class DescuentoABM
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal PorcentajeDescuento { get; set; }
    
    // Estado
    public bool Activo { get; set; } = true;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
}
