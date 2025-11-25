namespace BlazorVentas.Data.Models.ABM;

public class Provincia
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<Localidad> Localidades { get; set; } = new();
}

