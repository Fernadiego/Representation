using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data.Models;

public class Comprobante
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El código es requerido")]
    [StringLength(10, ErrorMessage = "El código no puede exceder 10 caracteres")]
    public string Codigo { get; set; } = string.Empty; // Cod
    
    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
    public string Descripcion { get; set; } = string.Empty; // Descrip.
    
    [Range(0, 9, ErrorMessage = "El tipo debe estar entre 0 y 9")]
    public int Tipo { get; set; } = 0; // T (número)
    
    [StringLength(10, ErrorMessage = "La numeración no puede exceder 10 caracteres")]
    public string Numeracion { get; set; } = string.Empty; // Numer (código de numeración)
    
    public List<Venta> Ventas { get; set; } = new();
}

