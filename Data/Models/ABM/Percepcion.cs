namespace BlazorVentas.Data.Models.ABM;

/// <summary>
/// Percepciones que se pueden aplicar a clientes
/// Tabla: AMRO_Percepcion
/// </summary>
public class Percepcion
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? TipoAfip { get; set; }
    public string? IvaPercepcion { get; set; }
    public decimal PercepMinima { get; set; }
    public decimal PorcentPercepcion { get; set; }
    public string? Mostrar { get; set; }

    // Relación con tipo de percepción
    public int? TipoPercepcionId { get; set; }
    public TipoPercepcion? TipoPercepcion { get; set; }
    
    // Estado
    public bool Activo { get; set; } = true;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    
    // Clientes asociados
    public ICollection<PercepcionCliente> PercepcionClientes { get; set; } = new List<PercepcionCliente>();
}

/// <summary>
/// Relación entre percepciones y clientes (1 cliente puede tener N percepciones)
/// Tabla: AMRO_Percepcion_Cliente
/// </summary>
public class PercepcionCliente
{
    public int Id { get; set; }
    public int PercepcionId { get; set; }
    public Percepcion? Percepcion { get; set; }
    public int IdCliente { get; set; }
    public Cliente? Cliente { get; set; }
}
