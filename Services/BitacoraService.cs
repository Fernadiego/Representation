using BlazorVentas.Data;
using BlazorVentas.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorVentas.Services;

public class BitacoraService
{
    private readonly CommerceDbContext _db;

    public BitacoraService(CommerceDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Registra un evento en la bitácora del sistema
    /// </summary>
    /// <param name="usuarioId">ID del usuario que realiza la acción</param>
    /// <param name="modulo">Módulo donde ocurre la acción (ej: "Login", "ABM Articulos", "ABM Clientes")</param>
    /// <param name="accion">Descripción de la acción realizada (ej: "Login exitoso", "Alta de artículo")</param>
    /// <param name="detalle">Información adicional sobre la acción (opcional)</param>
    public async Task RegistrarEventoAsync(int usuarioId, string modulo, string accion, string? detalle = null)
    {
        try
        {
            var bitacora = new Bitacora
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.UtcNow,
                Modulo = modulo,
                Accion = accion,
                Detalle = detalle
            };

            _db.Bitacoras.Add(bitacora);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log del error pero no interrumpir el flujo de la aplicación
            Console.WriteLine($"Error al registrar evento en bitácora: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtiene los eventos de la bitácora con filtros opcionales
    /// </summary>
    public async Task<List<Bitacora>> ObtenerEventosAsync(int? usuarioId = null, string? modulo = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, int? cantidad = null)
    {
        var query = _db.Bitacoras
            .Include(b => b.Usuario)
            .AsQueryable();

        if (usuarioId.HasValue)
            query = query.Where(b => b.UsuarioId == usuarioId.Value);

        if (!string.IsNullOrEmpty(modulo))
            query = query.Where(b => b.Modulo == modulo);

        if (fechaDesde.HasValue)
            query = query.Where(b => b.Fecha >= fechaDesde.Value);

        if (fechaHasta.HasValue)
            query = query.Where(b => b.Fecha <= fechaHasta.Value);

        query = query.OrderByDescending(b => b.Fecha);

        if (cantidad.HasValue)
            query = query.Take(cantidad.Value);

        return await query.ToListAsync();
    }
}

