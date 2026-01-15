namespace BlazorVentas.Data.Models.ABM;

/// <summary>
/// Tipos de percepción
/// Tabla: AMRO_Tipo_Percepcion
/// </summary>
public class TipoPercepcion
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Nombre { get; set; } = string.Empty;
    
    // Estado
    public bool Activo { get; set; } = true;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    
    // Percepciones de este tipo
    public ICollection<Percepcion> Percepciones { get; set; } = new List<Percepcion>();
}
