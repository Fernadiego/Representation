namespace BlazorVentas.Data.Models;

// Los vendedores son compartidos entre todas las empresas (no tienen CompanyId)
public class Vendedor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Comision { get; set; }
    public List<Venta> Ventas { get; set; } = new();
}

