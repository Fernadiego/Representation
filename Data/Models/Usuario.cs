using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data.Models;

public class Usuario
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string Login { get; set; } = string.Empty;
    
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? Nombre { get; set; }
    
    [StringLength(200)]
    public string? Apellido { get; set; }
    
    public bool Activo { get; set; } = true;
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    public DateTime? UltimoAcceso { get; set; }
}

