using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data.Models.ABM;

public class TipoComprobante
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;
}

