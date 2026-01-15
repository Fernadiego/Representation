namespace BlazorVentas.Data.Models.ABM;

public class TipoCliente
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string CondicionIva { get; set; } = string.Empty;
    public bool RequiereCuit { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public DateTime FechaModificacion { get; set; } = DateTime.Now;
}
