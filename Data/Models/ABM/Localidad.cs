namespace BlazorVentas.Data.Models.ABM;

public class Localidad
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty; // descripcion en BD
    public string? CodigoPostal { get; set; }
    public string? ProvinciaNombre { get; set; } // provincia (texto en BD, no FK)
    public string? Pais { get; set; }
    public bool? Activo { get; set; }
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaModificacion { get; set; }
    
    // Relación opcional con Provincia (si existe tabla Provincias)
    public int? ProvinciaId { get; set; }
    public Provincia? Provincia { get; set; }
}

