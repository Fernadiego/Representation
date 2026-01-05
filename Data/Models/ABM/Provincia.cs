namespace BlazorVentas.Data.Models.ABM;

public class Provincia
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty; // descripcion en BD
    public string? CodigoAfip { get; set; }
    public string? Pais { get; set; }
    public bool? Activo { get; set; }
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaModificacion { get; set; }
    
    // Relaciones
    public List<Localidad> Localidades { get; set; } = new();
}

