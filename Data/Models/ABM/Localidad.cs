namespace BlazorVentas.Data.Models.ABM;

public class Localidad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int ProvinciaId { get; set; }
    public Provincia? Provincia { get; set; }
}

