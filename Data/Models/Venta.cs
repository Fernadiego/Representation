namespace BlazorVentas.Data.Models;

public class Venta
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public int? VendedorId { get; set; }
    public Vendedor? Vendedor { get; set; }
    public int? ComprobanteId { get; set; }
    public Comprobante? Comprobante { get; set; }
    public string? NumeroComprobante { get; set; }
    
    // Impuestos
    public decimal ImpuestoProvincial { get; set; }
    public decimal ImpuestoNacional { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }

    public List<VentaLinea> Lineas { get; set; } = new();
}

