namespace BlazorVentas.Data.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Cliente> Clientes { get; set; } = new();
    public List<Articulo> Articulos { get; set; } = new();
    public List<Venta> Ventas { get; set; } = new();
}

