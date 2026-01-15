namespace BlazorVentas.Data.Models;

// Los vendedores son compartidos entre todas las empresas (no tienen CompanyId)
public class Vendedor
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal ComisionPorcentaje { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public List<Venta> Ventas { get; set; } = new();
}
