using BlazorVentas.Data.Models.ABM;

namespace BlazorVentas.Data.Models;

public class Articulo
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    // Campos según especificación
    public int CodigoArticulo { get; set; }
    public int CodigoParaMostrar { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int? MarcaId { get; set; }
    public Marca? Marca { get; set; }
    public int? OrigenId { get; set; }
    public Pais? Origen { get; set; }
    public decimal? PesoNeto { get; set; } // Gramos
    public decimal? PesoEscurrido { get; set; } // Gramos
    public int? TipoEnvaseId { get; set; }
    public TipoEnvase? TipoEnvase { get; set; }
    public int? UnidadXBulto { get; set; }
    public long? EAUN13 { get; set; }
    public long? DUN14 { get; set; }
    public decimal PrecioLista1 { get; set; }
    public decimal PrecioLista2 { get; set; }
    public decimal PrecioLista3 { get; set; }
    public decimal PrecioLista4 { get; set; }
    
    // Tamaños (Alto, Ancho, Profundo en cm)
    public decimal? TamañoUnidadAlto { get; set; }
    public decimal? TamañoUnidadAncho { get; set; }
    public decimal? TamañoUnidadProfundo { get; set; }
    public decimal? TamañoBultoAlto { get; set; }
    public decimal? TamañoBultoAncho { get; set; }
    public decimal? TamañoBultoProfundo { get; set; }
    public decimal? TamañoPaletAlto { get; set; }
    public decimal? TamañoPaletAncho { get; set; }
    public decimal? TamañoPaletProfundo { get; set; }
    
    public decimal? PesoBulto { get; set; }
    public decimal? PesoPalet { get; set; }
    public int? BultosXCamada { get; set; }
    public int? BultosXPalet { get; set; }
    public bool Inhabilitado { get; set; }
    public string? MensajeSobreArticulo { get; set; }

    public List<ClientePrecio> PreciosEspeciales { get; set; } = new();
    public List<VentaLinea> VentasLineas { get; set; } = new();
}

