using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data.Models;

public class Bitacora
{
    public int Id { get; set; }
    
    [Required]
    public int UsuarioId { get; set; }
    
    [Required]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    
    [Required]
    [StringLength(100)]
    public string Modulo { get; set; } = string.Empty; // Ej: "Login", "ABM Articulos", "ABM Clientes"
    
    [Required]
    [StringLength(200)]
    public string Accion { get; set; } = string.Empty; // Ej: "Login exitoso", "Alta de artículo", "Eliminación de cliente"
    
    [StringLength(1000)]
    public string? Detalle { get; set; } // Información adicional sobre la acción
    
    // Relación con Usuario
    public Usuario? Usuario { get; set; }
}

