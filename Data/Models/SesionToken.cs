namespace BlazorVentas.Data.Models;

public class SesionToken
{
    public string Token { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public DateTime UltimaActividad { get; set; }
    public bool Activo { get; set; } = true;
}

