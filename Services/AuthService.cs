using System.Security.Cryptography;
using System.Text;
using BlazorVentas.Data;
using BlazorVentas.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorVentas.Services;

public class AuthService
{
    private readonly CommerceDbContext _db;
    private readonly IServiceProvider _serviceProvider;
    private Usuario? _currentUser;

    public AuthService(CommerceDbContext db, IServiceProvider serviceProvider)
    {
        _db = db;
        _serviceProvider = serviceProvider;
    }
    
    private SessionService GetSessionService()
    {
        return _serviceProvider.GetRequiredService<SessionService>();
    }
    
    private BitacoraService GetBitacoraService()
    {
        return _serviceProvider.GetRequiredService<BitacoraService>();
    }

    public Usuario? CurrentUser => _currentUser;
    public bool IsAuthenticated => _currentUser != null;

    public async Task<string?> LoginAsync(string emailOrLogin, string password)
    {
        var usuario = await _db.Usuarios
            .FirstOrDefaultAsync(u => (u.Email == emailOrLogin || u.Login == emailOrLogin) && u.Activo);

        if (usuario == null)
        {
            // Registrar intento de login fallido
            try
            {
                var bitacoraService = GetBitacoraService();
                await bitacoraService.RegistrarEventoAsync(0, "Login", "Intento de login fallido", $"Usuario no encontrado: {emailOrLogin}");
            }
            catch { }
            return null;
        }

        var passwordHash = HashPassword(password);
        if (usuario.PasswordHash != passwordHash)
        {
            // Registrar intento de login fallido
            try
            {
                var bitacoraService = GetBitacoraService();
                await bitacoraService.RegistrarEventoAsync(usuario.Id, "Login", "Intento de login fallido", "Contraseña incorrecta");
            }
            catch { }
            return null;
        }

        usuario.UltimoAcceso = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        _currentUser = usuario;
        
        // Generar token de sesión
        var sessionService = GetSessionService();
        var token = await sessionService.CreateSessionAsync(usuario.Id);
        
        // Registrar login exitoso
        try
        {
            var bitacoraService = GetBitacoraService();
            await bitacoraService.RegistrarEventoAsync(usuario.Id, "Login", "Login exitoso", $"Usuario: {usuario.Login}");
        }
        catch { }
        
        return token;
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var sessionService = GetSessionService();
        var session = await sessionService.GetSessionAsync(token);
        if (session == null || !session.Activo)
            return false;

        // Verificar si el token expiró por inactividad
        if (DateTime.UtcNow > session.FechaExpiracion)
        {
            await sessionService.InvalidateSessionAsync(token);
            return false;
        }

        // Actualizar última actividad
        await sessionService.UpdateActivityAsync(token);

        // Cargar usuario
        _currentUser = await _db.Usuarios.FindAsync(session.UsuarioId);
        return _currentUser != null && _currentUser.Activo;
    }

    public async Task LogoutAsync(string? token = null)
    {
        var usuarioId = _currentUser?.Id ?? 0;
        
        if (!string.IsNullOrEmpty(token))
        {
            var sessionService = GetSessionService();
            await sessionService.InvalidateSessionAsync(token);
        }
        
        // Registrar logout
        if (usuarioId > 0)
        {
            try
            {
                var bitacoraService = GetBitacoraService();
                await bitacoraService.RegistrarEventoAsync(usuarioId, "Login", "Logout", $"Usuario: {_currentUser?.Login}");
            }
            catch { }
        }
        
        _currentUser = null;
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public async Task<bool> CreateUserAsync(string email, string login, string password, string? nombre = null, string? apellido = null)
    {
        // Verificar si ya existe
        var exists = await _db.Usuarios
            .AnyAsync(u => u.Email == email || u.Login == login);

        if (exists)
            return false;

        var usuario = new Usuario
        {
            Email = email,
            Login = login,
            PasswordHash = HashPassword(password),
            Nombre = nombre,
            Apellido = apellido,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        return true;
    }
}

