namespace BlazorVentas.Data.Models.ABM;

/// <summary>
/// Numeración de comprobantes - Tabla AMRO_Num_Comprobantes
/// Almacena el último número utilizado por cada tipo de comprobante
/// </summary>
public class NumeroComprobante
{
    public int Id { get; set; }
    
    /// <summary>
    /// FK a AMRO_Comprobantes
    /// </summary>
    public int ComprobanteId { get; set; }
    
    /// <summary>
    /// Empresa (para multiempresa)
    /// </summary>
    public int CompanyId { get; set; }
    
    /// <summary>
    /// Último número de comprobante utilizado
    /// </summary>
    public int Numero { get; set; }
    
    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime FechaModificacion { get; set; } = DateTime.Now;
}
