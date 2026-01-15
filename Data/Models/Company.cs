namespace BlazorVentas.Data.Models;

public class Company
{
    public int Id { get; set; }
    public string? Codigo { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public string? CUIT { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string? Color { get; set; }
    
    public List<Cliente> Clientes { get; set; } = new();
    public List<Articulo> Articulos { get; set; } = new();
    public List<Venta> Ventas { get; set; } = new();
}
