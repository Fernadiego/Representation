using System.Security.Cryptography;
using System.Text;
using BlazorVentas.Data;
using BlazorVentas.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorVentas.Services;

public class SessionService
{
    private readonly CommerceDbContext _db;
    private readonly IConfiguration _configuration;
    private readonly Dictionary<string, SesionToken> _activeSessions = new();

    // Tiempo de inactividad en minutos (configurable)
    private int InactivityTimeoutMinutes => _configuration.GetValue<int>("Session:InactivityTimeoutMinutes", 30);

    public SessionService(CommerceDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public Task<string> CreateSessionAsync(int usuarioId)
    {
        var token = GenerateToken();
        var now = DateTime.UtcNow;

        var session = new SesionToken
        {
            Token = token,
            UsuarioId = usuarioId,
            FechaCreacion = now,
            UltimaActividad = now,
            FechaExpiracion = now.AddMinutes(InactivityTimeoutMinutes),
            Activo = true
        };

        _activeSessions[token] = session;
        return Task.FromResult(token);
    }

    public Task<SesionToken?> GetSessionAsync(string token)
    {
        if (_activeSessions.TryGetValue(token, out var session))
        {
            // Verificar si expiró
            if (DateTime.UtcNow > session.FechaExpiracion)
            {
                _activeSessions.Remove(token);
                return Task.FromResult<SesionToken?>(null);
            }
            return Task.FromResult<SesionToken?>(session);
        }
        return Task.FromResult<SesionToken?>(null);
    }
    
    public Task<int?> GetUsuarioIdAsync(string token)
    {
        if (_activeSessions.TryGetValue(token, out var session))
        {
            // Verificar si expiró
            if (DateTime.UtcNow > session.FechaExpiracion)
            {
                _activeSessions.Remove(token);
                return Task.FromResult<int?>(null);
            }
            return Task.FromResult<int?>(session.UsuarioId);
        }
        return Task.FromResult<int?>(null);
    }

    public Task UpdateActivityAsync(string token)
    {
        if (_activeSessions.TryGetValue(token, out var session))
        {
            session.UltimaActividad = DateTime.UtcNow;
            session.FechaExpiracion = DateTime.UtcNow.AddMinutes(InactivityTimeoutMinutes);
        }
        return Task.CompletedTask;
    }

    public Task InvalidateSessionAsync(string token)
    {
        if (_activeSessions.TryGetValue(token, out var session))
        {
            session.Activo = false;
            _activeSessions.Remove(token);
        }
        return Task.CompletedTask;
    }

    private string GenerateToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }
}

