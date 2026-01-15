namespace BlazorVentas.Data.Models.ABM;

public class TipoComprobante
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string? Letra { get; set; }
    public string? CodigoAfip { get; set; }
    public bool RequiereCuit { get; set; }
    public bool RequiereStock { get; set; }
    public bool Afectacc { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaModificacion { get; set; }
}
