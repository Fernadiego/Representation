namespace BlazorVentas.Data.Models;

public class Comprobante
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // Factura, Nota de Crédito, etc.
    public List<Venta> Ventas { get; set; } = new();
}

