namespace BlazorVentas.Data.Models;

// Precios especiales o promociones por cliente
public class ClientePrecio
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public int ArticuloId { get; set; }
    public Articulo? Articulo { get; set; }
    public decimal Precio { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public bool EsPromocion { get; set; }
}

