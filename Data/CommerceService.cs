using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BlazorVentas.Services;
using BlazorVentas.Data.Models;
using BlazorVentas.Data.Models.ABM;

namespace BlazorVentas.Data;

public class CommerceService
{
    private readonly IServiceProvider _serviceProvider;

    public CommerceService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    private async Task RegistrarBitacoraAsync(int? usuarioId, string modulo, string accion, string? detalle = null)
    {
        if (!usuarioId.HasValue || usuarioId.Value == 0)
            return;
            
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var bitacoraService = scope.ServiceProvider.GetService<BitacoraService>();
            if (bitacoraService != null)
            {
                await bitacoraService.RegistrarEventoAsync(usuarioId.Value, modulo, accion, detalle);
            }
        }
        catch
        {
            // Ignorar errores de bitácora para no interrumpir el flujo
        }
    }

    #region ABM Listas

    public async Task<List<MarcaItem>> GetMarcasAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var marcas = await db.Marcas.ToListAsync();
        return marcas.Select(m => new MarcaItem { Id = m.Id, Nombre = m.Nombre }).ToList();
    }

    public async Task SaveMarcaAsync(MarcaItem marca)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        if (marca.Id == 0)
        {
            var entity = new Marca { Nombre = marca.Nombre };
            db.Marcas.Add(entity);
            await db.SaveChangesAsync();
            marca.Id = entity.Id;
        }
        else
        {
            var existing = await db.Marcas.FindAsync(marca.Id);
            if (existing != null)
            {
                existing.Nombre = marca.Nombre;
                await db.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteMarcaAsync(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var marca = await db.Marcas.FindAsync(id);
        if (marca != null)
        {
            db.Marcas.Remove(marca);
            await db.SaveChangesAsync();
        }
    }

    public async Task<List<PaisItem>> GetPaisesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var paises = await db.Paises.ToListAsync();
        return paises.Select(p => new PaisItem { Id = p.Id, Nombre = p.Nombre }).ToList();
    }

    public async Task SavePaisAsync(PaisItem pais)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        if (pais.Id == 0)
        {
            var entity = new Pais { Nombre = pais.Nombre };
            db.Paises.Add(entity);
            await db.SaveChangesAsync();
            pais.Id = entity.Id;
        }
        else
        {
            var existing = await db.Paises.FindAsync(pais.Id);
            if (existing != null)
            {
                existing.Nombre = pais.Nombre;
                await db.SaveChangesAsync();
            }
        }
    }

    public async Task DeletePaisAsync(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var pais = await db.Paises.FindAsync(id);
        if (pais != null)
        {
            db.Paises.Remove(pais);
            await db.SaveChangesAsync();
        }
    }

    public async Task<List<TipoEnvaseItem>> GetTiposEnvaseAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var tipos = await db.TipoEnvases.ToListAsync();
        return tipos.Select(t => new TipoEnvaseItem { Id = t.Id, Nombre = t.Nombre }).ToList();
    }

    public async Task SaveTipoEnvaseAsync(TipoEnvaseItem tipoEnvase)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        if (tipoEnvase.Id == 0)
        {
            var entity = new TipoEnvase { Nombre = tipoEnvase.Nombre };
            db.TipoEnvases.Add(entity);
            await db.SaveChangesAsync();
            tipoEnvase.Id = entity.Id;
        }
        else
        {
            var existing = await db.TipoEnvases.FindAsync(tipoEnvase.Id);
            if (existing != null)
            {
                existing.Nombre = tipoEnvase.Nombre;
                await db.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteTipoEnvaseAsync(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var tipo = await db.TipoEnvases.FindAsync(id);
        if (tipo != null)
        {
            db.TipoEnvases.Remove(tipo);
            await db.SaveChangesAsync();
        }
    }

    // ABMs para Clientes
    public async Task<List<ProvinciaItem>> GetProvinciasAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var provincias = await db.Provincias.ToListAsync();
        return provincias.Select(p => new ProvinciaItem { Id = p.Id, Nombre = p.Nombre }).ToList();
    }

    public async Task<List<LocalidadItem>> GetLocalidadesAsync(int? provinciaId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var query = db.Localidades.AsQueryable();
        if (provinciaId.HasValue)
            query = query.Where(l => l.ProvinciaId == provinciaId.Value);
        
        var localidades = await query.ToListAsync();
        return localidades.Select(l => new LocalidadItem { Id = l.Id, Nombre = l.Nombre, ProvinciaId = l.ProvinciaId ?? 0 }).ToList();
    }

    public async Task<List<DescuentoItem>> GetDescuentosAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var descuentos = await db.Descuentos.ToListAsync();
        return descuentos.Select(d => new DescuentoItem 
        { 
            Id = d.Id, 
            Codigo = d.Codigo, 
            Descripcion = d.Descripcion, 
            Porcentaje = d.Porcentaje 
        }).ToList();
    }

    public async Task<List<TipoClienteItem>> GetTiposClienteAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var tipos = await db.TipoClientes.Where(t => t.Activo).ToListAsync();
        return tipos.Select(t => new TipoClienteItem 
        { 
            Id = t.Id, 
            Codigo = t.Codigo,
            Nombre = t.Descripcion,
            CondicionIva = t.CondicionIva,
            RequiereCuit = t.RequiereCuit,
            Activo = t.Activo,
            FechaAlta = t.FechaAlta,
            FechaModificacion = t.FechaModificacion
        }).ToList();
    }

    public async Task SaveTipoClienteAsync(TipoClienteItem tipoCliente, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var isNew = tipoCliente.Id == 0;
        
        if (isNew)
        {
            var entity = new TipoCliente
            {
                Codigo = tipoCliente.Codigo,
                Descripcion = tipoCliente.Nombre,
                CondicionIva = tipoCliente.CondicionIva,
                RequiereCuit = tipoCliente.RequiereCuit,
                Activo = tipoCliente.Activo,
                FechaAlta = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            db.TipoClientes.Add(entity);
            await db.SaveChangesAsync();
            tipoCliente.Id = entity.Id;
        }
        else
        {
            var existing = await db.TipoClientes.FindAsync(tipoCliente.Id)
                ?? throw new InvalidOperationException("Tipo de cliente no encontrado");
            
            existing.Codigo = tipoCliente.Codigo;
            existing.Descripcion = tipoCliente.Nombre;
            existing.CondicionIva = tipoCliente.CondicionIva;
            existing.RequiereCuit = tipoCliente.RequiereCuit;
            existing.Activo = tipoCliente.Activo;
            existing.FechaModificacion = DateTime.Now;
            
            await db.SaveChangesAsync();
        }
        
        var accion = isNew ? "Alta de tipo de cliente" : "Modificación de tipo de cliente";
        var detalle = $"Tipo Cliente: {tipoCliente.Nombre} (Código: {tipoCliente.Codigo})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Tipo Cliente", accion, detalle);
    }

    public async Task DeleteTipoClienteAsync(int tipoClienteId, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var tipoCliente = await db.TipoClientes.FindAsync(tipoClienteId);
        if (tipoCliente != null)
        {
            var nombre = tipoCliente.Descripcion;
            var codigo = tipoCliente.Codigo;
            
            db.TipoClientes.Remove(tipoCliente);
            await db.SaveChangesAsync();
            
            var detalle = $"Tipo Cliente: {nombre} (Código: {codigo})";
            await RegistrarBitacoraAsync(usuarioId, "ABM Tipo Cliente", "Baja de tipo de cliente", detalle);
        }
    }

    public async Task<List<ZonaItem>> GetZonasAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var zonas = await db.Zonas.ToListAsync();
        return zonas.Select(z => new ZonaItem 
        { 
            Id = z.Id, 
            Codigo = z.Codigo,
            Descripcion = z.Descripcion,
            Activo = z.Activo,
            FechaAlta = z.FechaAlta
        }).ToList();
    }

    public async Task SaveZonaAsync(ZonaItem zona, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        string accion;
        string detalle;
        
        if (zona.Id == 0)
        {
            var nuevaZona = new Zona
            {
                Codigo = zona.Codigo,
                Descripcion = zona.Descripcion,
                Activo = zona.Activo,
                FechaAlta = DateTime.Now
            };
            db.Zonas.Add(nuevaZona);
            await db.SaveChangesAsync();
            zona.Id = nuevaZona.Id;
            
            accion = "Alta de zona";
            detalle = $"Código: {zona.Codigo}, Descripción: {zona.Descripcion}";
        }
        else
        {
            var existing = await db.Zonas.FindAsync(zona.Id);
            if (existing != null)
            {
                existing.Codigo = zona.Codigo;
                existing.Descripcion = zona.Descripcion;
                existing.Activo = zona.Activo;
                
                await db.SaveChangesAsync();
            }
            
            accion = "Modificación de zona";
            detalle = $"Código: {zona.Codigo}, Descripción: {zona.Descripcion}, Activo: {zona.Activo}";
        }
        
        await RegistrarBitacoraAsync(usuarioId, "ABM Zonas", accion, detalle);
    }

    public async Task DeleteZonaAsync(int zonaId, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var zona = await db.Zonas.FindAsync(zonaId);
        if (zona != null)
        {
            var detalle = $"Código: {zona.Codigo}, Descripción: {zona.Descripcion}";
            
            db.Zonas.Remove(zona);
            await db.SaveChangesAsync();
            
            await RegistrarBitacoraAsync(usuarioId, "ABM Zonas", "Eliminación de zona", detalle);
        }
    }

    public async Task<List<VendedorItem>> GetVendedoresAbmAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var vendedores = await db.Vendedores.ToListAsync();
        return vendedores.Select(v => new VendedorItem 
        { 
            Id = v.Id, 
            Codigo = v.Codigo,
            Nombre = v.Nombre,
            ComisionPorcentaje = v.ComisionPorcentaje,
            Activo = v.Activo,
            FechaAlta = v.FechaAlta
        }).ToList();
    }

    public async Task SaveVendedorAsync(VendedorItem vendedor, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        string accion;
        string detalle;
        
        if (vendedor.Id == 0)
        {
            var nuevoVendedor = new Vendedor
            {
                Codigo = vendedor.Codigo,
                Nombre = vendedor.Nombre,
                ComisionPorcentaje = vendedor.ComisionPorcentaje,
                Activo = vendedor.Activo,
                FechaAlta = DateTime.Now
            };
            db.Vendedores.Add(nuevoVendedor);
            await db.SaveChangesAsync();
            vendedor.Id = nuevoVendedor.Id;
            
            accion = "Alta de vendedor";
            detalle = $"Código: {vendedor.Codigo}, Nombre: {vendedor.Nombre}, Comisión: {vendedor.ComisionPorcentaje}%";
        }
        else
        {
            var existing = await db.Vendedores.FindAsync(vendedor.Id);
            if (existing != null)
            {
                existing.Codigo = vendedor.Codigo;
                existing.Nombre = vendedor.Nombre;
                existing.ComisionPorcentaje = vendedor.ComisionPorcentaje;
                existing.Activo = vendedor.Activo;
                
                await db.SaveChangesAsync();
            }
            
            accion = "Modificación de vendedor";
            detalle = $"Código: {vendedor.Codigo}, Nombre: {vendedor.Nombre}, Comisión: {vendedor.ComisionPorcentaje}%, Activo: {vendedor.Activo}";
        }
        
        await RegistrarBitacoraAsync(usuarioId, "ABM Vendedores", accion, detalle);
    }

    public async Task DeleteVendedorAsync(int vendedorId, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var vendedor = await db.Vendedores.FindAsync(vendedorId);
        if (vendedor != null)
        {
            var detalle = $"Código: {vendedor.Codigo}, Nombre: {vendedor.Nombre}";
            
            db.Vendedores.Remove(vendedor);
            await db.SaveChangesAsync();
            
            await RegistrarBitacoraAsync(usuarioId, "ABM Vendedores", "Eliminación de vendedor", detalle);
        }
    }

    public async Task<List<CobradorItem>> GetCobradoresAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var cobradores = await db.Cobradores.ToListAsync();
        return cobradores.Select(c => new CobradorItem 
        { 
            Id = c.Id, 
            Codigo = c.Codigo,
            Nombre = c.Nombre,
            Activo = c.Activo,
            FechaAlta = c.FechaAlta
        }).ToList();
    }

    public async Task SaveCobradorAsync(CobradorItem cobrador, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        string accion;
        string detalle;
        
        if (cobrador.Id == 0)
        {
            var nuevoCobrador = new Cobrador
            {
                Codigo = cobrador.Codigo,
                Nombre = cobrador.Nombre,
                Activo = cobrador.Activo,
                FechaAlta = DateTime.Now
            };
            db.Cobradores.Add(nuevoCobrador);
            await db.SaveChangesAsync();
            cobrador.Id = nuevoCobrador.Id;
            
            accion = "Alta de cobrador";
            detalle = $"Código: {cobrador.Codigo}, Nombre: {cobrador.Nombre}";
        }
        else
        {
            var existing = await db.Cobradores.FindAsync(cobrador.Id);
            if (existing != null)
            {
                existing.Codigo = cobrador.Codigo;
                existing.Nombre = cobrador.Nombre;
                existing.Activo = cobrador.Activo;
                
                await db.SaveChangesAsync();
            }
            
            accion = "Modificación de cobrador";
            detalle = $"Código: {cobrador.Codigo}, Nombre: {cobrador.Nombre}, Activo: {cobrador.Activo}";
        }
        
        await RegistrarBitacoraAsync(usuarioId, "ABM Cobradores", accion, detalle);
    }

    public async Task DeleteCobradorAsync(int cobradorId, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var cobrador = await db.Cobradores.FindAsync(cobradorId);
        if (cobrador != null)
        {
            var detalle = $"Código: {cobrador.Codigo}, Nombre: {cobrador.Nombre}";
            
            db.Cobradores.Remove(cobrador);
            await db.SaveChangesAsync();
            
            await RegistrarBitacoraAsync(usuarioId, "ABM Cobradores", "Eliminación de cobrador", detalle);
        }
    }

    public async Task<List<ClaseClienteItem>> GetClasesClienteAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var clases = await db.ClaseClientes.ToListAsync();
        return clases.Select(c => new ClaseClienteItem { Id = c.Id, Nombre = c.Nombre }).ToList();
    }

    // ABMs para Comprobantes (AMRO_Comprobantes)
    public async Task<List<ComprobanteItem>> GetComprobantesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var comprobantes = await db.Comprobantes.ToListAsync();
        return comprobantes.Select(c => new ComprobanteItem 
        { 
            Id = c.Id, 
            Codigo = c.Codigo, 
            Descripcion = c.Descripcion,
            Letra = c.Letra,
            CodigoAfip = c.CodigoAfip,
            RequiereCuit = c.RequiereCuit,
            RequiereStock = c.RequiereStock,
            Afectacc = c.Afectacc,
            Activo = c.Activo,
            FechaAlta = c.FechaAlta,
            FechaModificacion = c.FechaModificacion
        }).ToList();
    }

    public async Task SaveComprobanteAsync(ComprobanteItem comprobante, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var isNew = comprobante.Id == 0;
        
        if (isNew)
        {
            var entity = new Comprobante 
            { 
                Codigo = comprobante.Codigo, 
                Descripcion = comprobante.Descripcion,
                Letra = comprobante.Letra,
                CodigoAfip = comprobante.CodigoAfip,
                RequiereCuit = comprobante.RequiereCuit,
                RequiereStock = comprobante.RequiereStock,
                Afectacc = comprobante.Afectacc,
                Activo = comprobante.Activo,
                FechaAlta = DateTime.Now
            };
            db.Comprobantes.Add(entity);
            await db.SaveChangesAsync();
            comprobante.Id = entity.Id;
        }
        else
        {
            var existing = await db.Comprobantes.FindAsync(comprobante.Id);
            if (existing != null)
            {
                existing.Codigo = comprobante.Codigo;
                existing.Descripcion = comprobante.Descripcion;
                existing.Letra = comprobante.Letra;
                existing.CodigoAfip = comprobante.CodigoAfip;
                existing.RequiereCuit = comprobante.RequiereCuit;
                existing.RequiereStock = comprobante.RequiereStock;
                existing.Afectacc = comprobante.Afectacc;
                existing.Activo = comprobante.Activo;
                existing.FechaModificacion = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
        
        var accion = isNew ? "Alta de comprobante" : "Modificación de comprobante";
        var detalle = $"Comprobante: {comprobante.Descripcion} (Código: {comprobante.Codigo})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Comprobantes", accion, detalle);
    }

    public async Task DeleteComprobanteAsync(int id, int? usuarioId = null)
    {
        string? descripcion = null;
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var comprobante = await db.Comprobantes.FindAsync(id);
        if (comprobante != null)
        {
            descripcion = comprobante.Descripcion;
            db.Comprobantes.Remove(comprobante);
            await db.SaveChangesAsync();
        }
        
        var detalle = descripcion != null ? $"Comprobante eliminado: {descripcion} (ID: {id})" : $"Comprobante eliminado (ID: {id})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Comprobantes", "Eliminación de comprobante", detalle);
    }

    // ABMs para Tipos Comprobantes
    public async Task<List<TipoComprobanteItem>> GetTiposComprobanteAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var tipos = await db.TiposComprobante.Where(t => t.Activo).ToListAsync();
        return tipos.Select(t => new TipoComprobanteItem 
        { 
            Id = t.Id, 
            Codigo = t.Codigo,
            Descripcion = t.Descripcion,
            Letra = t.Letra,
            CodigoAfip = t.CodigoAfip,
            RequiereCuit = t.RequiereCuit,
            RequiereStock = t.RequiereStock,
            Afectacc = t.Afectacc,
            Activo = t.Activo
        }).ToList();
    }

    public async Task SaveTipoComprobanteAsync(TipoComprobanteItem tipoComprobante, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var isNew = tipoComprobante.Id == 0;
        
        if (isNew)
        {
            var entity = new TipoComprobante 
            { 
                Codigo = tipoComprobante.Codigo,
                Descripcion = tipoComprobante.Descripcion,
                Letra = tipoComprobante.Letra,
                CodigoAfip = tipoComprobante.CodigoAfip,
                RequiereCuit = tipoComprobante.RequiereCuit,
                RequiereStock = tipoComprobante.RequiereStock,
                Afectacc = tipoComprobante.Afectacc,
                Activo = tipoComprobante.Activo,
                FechaAlta = DateTime.Now
            };
            db.TiposComprobante.Add(entity);
            await db.SaveChangesAsync();
            tipoComprobante.Id = entity.Id;
        }
        else
        {
            var existing = await db.TiposComprobante.FindAsync(tipoComprobante.Id);
            if (existing != null)
            {
                existing.Codigo = tipoComprobante.Codigo;
                existing.Descripcion = tipoComprobante.Descripcion;
                existing.Letra = tipoComprobante.Letra;
                existing.CodigoAfip = tipoComprobante.CodigoAfip;
                existing.RequiereCuit = tipoComprobante.RequiereCuit;
                existing.RequiereStock = tipoComprobante.RequiereStock;
                existing.Afectacc = tipoComprobante.Afectacc;
                existing.Activo = tipoComprobante.Activo;
                existing.FechaModificacion = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
        
        var accion = isNew ? "Alta de tipo comprobante" : "Modificación de tipo comprobante";
        var detalle = $"Tipo Comprobante: {tipoComprobante.Codigo} - {tipoComprobante.Descripcion}";
        await RegistrarBitacoraAsync(usuarioId, "ABM Tipos Comprobantes", accion, detalle);
    }

    public async Task DeleteTipoComprobanteAsync(int id, int? usuarioId = null)
    {
        string? descripcionCompleta = null;
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var tipo = await db.TiposComprobante.FindAsync(id);
        if (tipo != null)
        {
            descripcionCompleta = $"{tipo.Codigo} - {tipo.Descripcion}";
            db.TiposComprobante.Remove(tipo);
            await db.SaveChangesAsync();
        }
        
        var detalle = descripcionCompleta != null ? $"Tipo Comprobante eliminado: {descripcionCompleta} (ID: {id})" : $"Tipo Comprobante eliminado (ID: {id})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Tipos Comprobantes", "Eliminación de tipo comprobante", detalle);
    }

    #endregion

    #region Companies

    public async Task<List<Company>> GetCompaniesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        return await db.Companies.ToListAsync();
    }

    public async Task SaveCompanyAsync(Company company)
    {
        ArgumentNullException.ThrowIfNull(company);

        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        if (company.Id == 0)
        {
            company.FechaAlta = DateTime.Now;
            db.Companies.Add(company);
        }
        else
        {
            var existing = await db.Companies.FindAsync(company.Id) 
                ?? throw new InvalidOperationException("Empresa no encontrada");
            existing.RazonSocial = company.RazonSocial;
            existing.CUIT = company.CUIT;
            existing.Direccion = company.Direccion;
            existing.Telefono = company.Telefono;
            existing.Email = company.Email;
            existing.Activo = company.Activo;
            existing.FechaModificacion = DateTime.Now;
            existing.Color = company.Color;
        }
        
        await db.SaveChangesAsync();
    }

    public async Task DeleteCompanyAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var company = await db.Companies.FindAsync(companyId);
        if (company != null)
        {
            db.Companies.Remove(company);
            await db.SaveChangesAsync();
        }
    }

    #endregion

    #region Suppliers

    public async Task<List<Supplier>> GetSuppliersAsync(int companyId)
    {
        // Los proveedores aún están en memoria - se pueden migrar a BD si se necesita
        return await Task.FromResult(new List<Supplier>());
    }

    public async Task SaveSupplierAsync(int companyId, Supplier supplier)
    {
        // Implementación pendiente para BD
        await Task.CompletedTask;
    }

    public async Task DeleteSupplierAsync(int companyId, int supplierId)
    {
        // Implementación pendiente para BD
        await Task.CompletedTask;
    }

    #endregion

    #region Vendors

    public async Task<List<Vendor>> GetVendorsAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var vendedores = await db.Vendedores
            .Where(v => v.Activo)
            .OrderBy(v => v.Nombre)
            .ToListAsync();
        
        return vendedores.Select(v => new Vendor
        {
            Id = v.Id,
            CompanyId = companyId,
            Name = $"{v.Codigo} - {v.Nombre}",
            CommissionRate = v.ComisionPorcentaje
        }).ToList();
    }

    public async Task SaveVendorAsync(int companyId, Vendor vendor)
    {
        // Implementación pendiente para BD
        await Task.CompletedTask;
    }

    public async Task DeleteVendorAsync(int companyId, int vendorId)
    {
        // Implementación pendiente para BD
        await Task.CompletedTask;
    }

    #endregion

    #region Customers

    public async Task<List<Customer>> GetCustomersAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var clientes = await db.Clientes
            .Include(c => c.Company)
            .Include(c => c.Zona)
            .Include(c => c.Vendedor)
            .Include(c => c.Cobrador)
            .Include(c => c.Provincia)
            .Include(c => c.Localidad)
            .Include(c => c.CodigoDescuento)
            .Include(c => c.TipoCliente)
            .Include(c => c.ClaseCliente)
            .Include(c => c.DescuentoABM)
            .Where(c => c.CompanyId == companyId)
            .ToListAsync();
        
        return clientes.Select(c => new Customer
        {
            Id = c.Id,
            CompanyId = c.CompanyId,
            CompanyNombre = c.Company?.RazonSocial,
            CodigoCliente = c.CodigoCliente,
            CodigoSucursal = c.CodigoSucursal,
            CodigoParaMostrar = c.CodigoParaMostrar,
            Name = c.NombreCliente,
            NombreSucursal = c.NombreSucursal,
            DomicilioEntrega = c.DomicilioEntrega,
            DomicilioLegal = c.DomicilioLegal,
            LocalidadId = c.LocalidadId,
            ProvinciaId = c.ProvinciaId,
            ProvinciaNombre = c.Provincia?.Nombre,
            CP = c.CP,
            Phone = c.Telefono,
            Email = c.Mail,
            Web = c.Web,
            Contacto = c.Contacto,
            Cuit = c.CUIT,
            ListaPrecio = c.ListaPrecio,
            CodigoDescuentoId = c.CodigoDescuentoId,
            CodigoDescuentoNombre = c.CodigoDescuento?.Descripcion,
            DescuentoABMId = c.DescuentoABMId,
            DescuentoABMNombre = c.DescuentoABM?.Nombre,
            DescuentoABMPorcentaje = c.DescuentoABM?.PorcentajeDescuento ?? 0,
            TipoClienteId = c.TipoClienteId,
            TipoClienteNombre = c.TipoCliente?.Descripcion,
            CondicionPago = c.CondicionPago,
            ZonaId = c.ZonaId,
            ZonaNombre = c.Zona?.Descripcion,
            VendedorId = c.VendedorId,
            VendedorNombre = c.Vendedor?.Nombre,
            CobradorId = c.CobradorId,
            CobradorNombre = c.Cobrador?.Nombre,
            ClaseClienteId = c.ClaseClienteId,
            ClaseClienteNombre = c.ClaseCliente?.Nombre,
            FechaUltimaCompra = c.FechaUltimaCompra,
            FechaAlta = c.FechaAlta,
            Inhabilitado = c.Inhabilitado,
            MensajeSobreCliente = c.MensajeSobreCliente,
            TieneMensaje = !string.IsNullOrEmpty(c.MensajeSobreCliente)
        }).ToList();
    }

    public async Task SaveCustomerAsync(int companyId, Customer customer, int? usuarioId = null)
    {
        ArgumentNullException.ThrowIfNull(customer);

        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var isNew = customer.Id == 0;
        
        if (isNew)
        {
            var entity = new Cliente
            {
                CompanyId = companyId,
                CodigoCliente = customer.CodigoCliente,
                CodigoSucursal = customer.CodigoSucursal,
                CodigoParaMostrar = customer.CodigoParaMostrar,
                NombreCliente = customer.Name,
                NombreSucursal = customer.NombreSucursal,
                DomicilioEntrega = customer.DomicilioEntrega,
                DomicilioLegal = customer.DomicilioLegal,
                LocalidadId = customer.LocalidadId,
                ProvinciaId = customer.ProvinciaId,
                CP = customer.CP,
                Telefono = customer.Phone,
                Mail = customer.Email,
                Web = customer.Web,
                Contacto = customer.Contacto,
                CUIT = customer.Cuit,
                ListaPrecio = customer.ListaPrecio,
                CodigoDescuentoId = customer.CodigoDescuentoId,
                DescuentoABMId = customer.DescuentoABMId,
                TipoClienteId = customer.TipoClienteId,
                CondicionPago = customer.CondicionPago,
                ZonaId = customer.ZonaId,
                VendedorId = customer.VendedorId,
                CobradorId = customer.CobradorId,
                ClaseClienteId = customer.ClaseClienteId,
                FechaUltimaCompra = customer.FechaUltimaCompra,
                FechaAlta = customer.FechaAlta == default ? DateTime.Today : customer.FechaAlta,
                Inhabilitado = customer.Inhabilitado,
                MensajeSobreCliente = customer.MensajeSobreCliente
            };
            
            db.Clientes.Add(entity);
            await db.SaveChangesAsync();
            customer.Id = entity.Id;
        }
        else
        {
            var existing = await db.Clientes.FindAsync(customer.Id) 
                ?? throw new InvalidOperationException("Cliente no encontrado");
            
            existing.CodigoCliente = customer.CodigoCliente;
            existing.CodigoSucursal = customer.CodigoSucursal;
            existing.CodigoParaMostrar = customer.CodigoParaMostrar;
            existing.NombreCliente = customer.Name;
            existing.NombreSucursal = customer.NombreSucursal;
            existing.DomicilioEntrega = customer.DomicilioEntrega;
            existing.DomicilioLegal = customer.DomicilioLegal;
            existing.LocalidadId = customer.LocalidadId;
            existing.ProvinciaId = customer.ProvinciaId;
            existing.CP = customer.CP;
            existing.Telefono = customer.Phone;
            existing.Mail = customer.Email;
            existing.Web = customer.Web;
            existing.Contacto = customer.Contacto;
            existing.CUIT = customer.Cuit;
            existing.ListaPrecio = customer.ListaPrecio;
            existing.CodigoDescuentoId = customer.CodigoDescuentoId;
            existing.DescuentoABMId = customer.DescuentoABMId;
            existing.TipoClienteId = customer.TipoClienteId;
            existing.CondicionPago = customer.CondicionPago;
            existing.ZonaId = customer.ZonaId;
            existing.VendedorId = customer.VendedorId;
            existing.CobradorId = customer.CobradorId;
            existing.ClaseClienteId = customer.ClaseClienteId;
            existing.FechaUltimaCompra = customer.FechaUltimaCompra;
            existing.FechaAlta = customer.FechaAlta;
            existing.Inhabilitado = customer.Inhabilitado;
            existing.MensajeSobreCliente = customer.MensajeSobreCliente;
            
            await db.SaveChangesAsync();
        }
        
        var accion = isNew ? "Alta de cliente" : "Modificación de cliente";
        var detalle = $"Cliente: {customer.Name} (Código: {customer.CodigoCliente})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Clientes", accion, detalle);
    }

    public async Task DeleteCustomerAsync(int companyId, int customerId, int? usuarioId = null)
    {
        string nombreCliente = string.Empty;
        
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var customer = await db.Clientes.FindAsync(customerId);
        if (customer != null)
        {
            nombreCliente = customer.NombreCliente;
            db.Clientes.Remove(customer);
            await db.SaveChangesAsync();
        }
        
        var detalle = !string.IsNullOrEmpty(nombreCliente) 
            ? $"Cliente eliminado: {nombreCliente} (ID: {customerId})" 
            : $"Cliente eliminado (ID: {customerId})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Clientes", "Eliminación de cliente", detalle);
    }

    #endregion

    #region Products

    public async Task<List<Product>> GetProductsAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var articulos = await db.Articulos
            .Include(a => a.Marca)
            .Include(a => a.Origen)
            .Include(a => a.TipoEnvase)
            .Where(a => a.CompanyId == companyId)
            .ToListAsync();
        
        return articulos.Select(a => new Product
        {
            Id = a.Id,
            CompanyId = a.CompanyId,
            CodigoArticulo = a.CodigoArticulo,
            CodigoParaMostrar = a.CodigoParaMostrar,
            Name = a.Descripcion,
            MarcaId = a.MarcaId,
            MarcaNombre = a.Marca?.Nombre,
            OrigenId = a.OrigenId,
            OrigenNombre = a.Origen?.Nombre,
            PesoNeto = a.PesoNeto,
            PesoEscurrido = a.PesoEscurrido,
            TipoEnvaseId = a.TipoEnvaseId,
            TipoEnvaseNombre = a.TipoEnvase?.Nombre,
            UnidadXBulto = a.UnidadXBulto,
            EAUN13 = a.EAUN13,
            DUN14 = a.DUN14,
            PrecioLista1 = a.PrecioLista1,
            PrecioLista2 = a.PrecioLista2,
            PrecioLista3 = a.PrecioLista3,
            PrecioLista4 = a.PrecioLista4,
            UnitPrice = a.PrecioLista1,
            TamañoUnidadAlto = a.TamañoUnidadAlto,
            TamañoUnidadAncho = a.TamañoUnidadAncho,
            TamañoUnidadProfundo = a.TamañoUnidadProfundo,
            TamañoBultoAlto = a.TamañoBultoAlto,
            TamañoBultoAncho = a.TamañoBultoAncho,
            TamañoBultoProfundo = a.TamañoBultoProfundo,
            TamañoPaletAlto = a.TamañoPaletAlto,
            TamañoPaletAncho = a.TamañoPaletAncho,
            TamañoPaletProfundo = a.TamañoPaletProfundo,
            PesoBulto = a.PesoBulto,
            PesoPalet = a.PesoPalet,
            BultosXCamada = a.BultosXCamada,
            BultosXPalet = a.BultosXPalet,
            Inhabilitado = a.Inhabilitado
        }).ToList();
    }

    public async Task SaveProductAsync(int companyId, Product product, int? usuarioId = null)
    {
        ArgumentNullException.ThrowIfNull(product);

        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var isNew = product.Id == 0;
        
        if (isNew)
        {
            var entity = new Articulo
            {
                CompanyId = companyId,
                CodigoArticulo = product.CodigoArticulo,
                CodigoParaMostrar = product.CodigoParaMostrar,
                Descripcion = product.Name,
                MarcaId = product.MarcaId,
                OrigenId = product.OrigenId,
                PesoNeto = product.PesoNeto,
                PesoEscurrido = product.PesoEscurrido,
                TipoEnvaseId = product.TipoEnvaseId,
                UnidadXBulto = product.UnidadXBulto,
                EAUN13 = product.EAUN13,
                DUN14 = product.DUN14,
                PrecioLista1 = product.PrecioLista1 > 0 ? product.PrecioLista1 : product.UnitPrice,
                PrecioLista2 = product.PrecioLista2 > 0 ? product.PrecioLista2 : product.UnitPrice,
                PrecioLista3 = product.PrecioLista3 > 0 ? product.PrecioLista3 : product.UnitPrice,
                PrecioLista4 = product.PrecioLista4 > 0 ? product.PrecioLista4 : product.UnitPrice,
                TamañoUnidadAlto = product.TamañoUnidadAlto,
                TamañoUnidadAncho = product.TamañoUnidadAncho,
                TamañoUnidadProfundo = product.TamañoUnidadProfundo,
                TamañoBultoAlto = product.TamañoBultoAlto,
                TamañoBultoAncho = product.TamañoBultoAncho,
                TamañoBultoProfundo = product.TamañoBultoProfundo,
                TamañoPaletAlto = product.TamañoPaletAlto,
                TamañoPaletAncho = product.TamañoPaletAncho,
                TamañoPaletProfundo = product.TamañoPaletProfundo,
                PesoBulto = product.PesoBulto,
                PesoPalet = product.PesoPalet,
                BultosXCamada = product.BultosXCamada,
                BultosXPalet = product.BultosXPalet,
                Inhabilitado = product.Inhabilitado
            };
            
            db.Articulos.Add(entity);
            await db.SaveChangesAsync();
            product.Id = entity.Id;
        }
        else
        {
            var existing = await db.Articulos.FindAsync(product.Id) 
                ?? throw new InvalidOperationException("Producto no encontrado");
            
            existing.CodigoArticulo = product.CodigoArticulo;
            existing.CodigoParaMostrar = product.CodigoParaMostrar;
            existing.Descripcion = product.Name;
            existing.MarcaId = product.MarcaId;
            existing.OrigenId = product.OrigenId;
            existing.PesoNeto = product.PesoNeto;
            existing.PesoEscurrido = product.PesoEscurrido;
            existing.TipoEnvaseId = product.TipoEnvaseId;
            existing.UnidadXBulto = product.UnidadXBulto;
            existing.EAUN13 = product.EAUN13;
            existing.DUN14 = product.DUN14;
            existing.PrecioLista1 = product.PrecioLista1;
            existing.PrecioLista2 = product.PrecioLista2;
            existing.PrecioLista3 = product.PrecioLista3;
            existing.PrecioLista4 = product.PrecioLista4;
            existing.TamañoUnidadAlto = product.TamañoUnidadAlto;
            existing.TamañoUnidadAncho = product.TamañoUnidadAncho;
            existing.TamañoUnidadProfundo = product.TamañoUnidadProfundo;
            existing.TamañoBultoAlto = product.TamañoBultoAlto;
            existing.TamañoBultoAncho = product.TamañoBultoAncho;
            existing.TamañoBultoProfundo = product.TamañoBultoProfundo;
            existing.TamañoPaletAlto = product.TamañoPaletAlto;
            existing.TamañoPaletAncho = product.TamañoPaletAncho;
            existing.TamañoPaletProfundo = product.TamañoPaletProfundo;
            existing.PesoBulto = product.PesoBulto;
            existing.PesoPalet = product.PesoPalet;
            existing.BultosXCamada = product.BultosXCamada;
            existing.BultosXPalet = product.BultosXPalet;
            existing.Inhabilitado = product.Inhabilitado;
            
            await db.SaveChangesAsync();
        }
        
        var accion = isNew ? "Alta de artículo" : "Modificación de artículo";
        var detalle = $"Artículo: {product.Name} (Código: {product.CodigoArticulo})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Articulos", accion, detalle);
    }

    public async Task DeleteProductAsync(int companyId, int productId, int? usuarioId = null)
    {
        string? nombreProducto = null;
        
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var product = await db.Articulos.FindAsync(productId);
        if (product != null)
        {
            nombreProducto = product.Descripcion;
            db.Articulos.Remove(product);
            await db.SaveChangesAsync();
        }
        
        var detalle = nombreProducto != null 
            ? $"Artículo eliminado: {nombreProducto} (ID: {productId})" 
            : $"Artículo eliminado (ID: {productId})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Articulos", "Eliminación de artículo", detalle);
    }

    #endregion

    #region Sales

    public async Task<List<Sale>> GetSalesAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var ventas = await db.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Vendedor)
            .Include(v => v.Comprobante)
            .Include(v => v.Lineas)
                .ThenInclude(l => l.Articulo)
            .Where(v => v.CompanyId == companyId)
            .ToListAsync();
        
        return ventas.Select(v => new Sale
        {
            Id = v.Id,
            CompanyId = v.CompanyId,
            Date = v.Fecha,
            CustomerId = v.ClienteId,
            Customer = v.Cliente != null ? new Customer { Id = v.Cliente.Id, Name = v.Cliente.NombreCliente } : null,
            VendorId = v.VendedorId ?? 0,
            Vendor = v.Vendedor != null ? new Vendor { Id = v.Vendedor.Id, Name = v.Vendedor.Nombre } : null,
            TipoComprobante = v.Comprobante?.Codigo ?? string.Empty,
            NumeroComprobante = v.NumeroComprobante ?? string.Empty,
            Lines = v.Lineas.Select(l => new SaleLine
            {
                ProductId = l.ArticuloId,
                Product = l.Articulo != null ? new Product { Id = l.Articulo.Id, Name = l.Articulo.Descripcion } : null,
                Quantity = l.Cantidad,
                UnitPrice = l.PrecioUnitario
            }).ToList()
        }).ToList();
    }

    /// <summary>
    /// Obtiene las ventas de la tabla AMRO_Ventas (nueva estructura)
    /// </summary>
    public async Task<List<VentaAMRO>> GetVentasAMROAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        return await db.VentasAMRO
            .Include(v => v.Detalles)
            .Include(v => v.Percepciones)
            .Where(v => v.CompanyId == companyId)
            .OrderByDescending(v => v.Fecha)
            .ToListAsync();
    }

    public async Task<Sale> CreateSaleAsync(int companyId, Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale);
        if (!sale.Lines.Any())
        {
            throw new InvalidOperationException("La venta requiere al menos un detalle.");
        }

        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        // Buscar el comprobante si se especificó
        int? comprobanteId = null;
        if (!string.IsNullOrEmpty(sale.TipoComprobante))
        {
            var comprobante = await db.Comprobantes.FirstOrDefaultAsync(c => c.Codigo == sale.TipoComprobante);
            comprobanteId = comprobante?.Id ?? 0;
        }
        
        var venta = new Venta
        {
            CompanyId = companyId,
            Fecha = sale.Date,
            ClienteId = sale.CustomerId,
            VendedorId = sale.VendorId,
            ComprobanteId = comprobanteId ?? 0,
            NumeroComprobante = sale.NumeroComprobante,
            Subtotal = sale.Lines.Sum(l => l.Quantity * l.UnitPrice),
            Total = sale.Lines.Sum(l => l.Quantity * l.UnitPrice)
        };
        
        db.Ventas.Add(venta);
        await db.SaveChangesAsync();
        
        // Agregar líneas
        foreach (var line in sale.Lines)
        {
            var ventaLinea = new VentaLinea
            {
                VentaId = venta.Id,
                ArticuloId = line.ProductId,
                Cantidad = line.Quantity,
                PrecioUnitario = line.UnitPrice,
                Subtotal = line.Quantity * line.UnitPrice,
                Total = line.Quantity * line.UnitPrice
            };
            db.VentaLineas.Add(ventaLinea);
        }
        
        await db.SaveChangesAsync();
        
        sale.Id = venta.Id;
        return sale;
    }

    public async Task<Sale?> GetSaleByComprobanteAsync(int companyId, string tipoComprobante, string numeroComprobante)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var venta = await db.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Vendedor)
            .Include(v => v.Comprobante)
            .Include(v => v.Lineas)
                .ThenInclude(l => l.Articulo)
            .FirstOrDefaultAsync(v => 
                v.CompanyId == companyId && 
                v.Comprobante != null && 
                v.Comprobante.Codigo == tipoComprobante && 
                v.NumeroComprobante == numeroComprobante);
        
        if (venta == null) return null;
        
        return new Sale
        {
            Id = venta.Id,
            CompanyId = venta.CompanyId,
            Date = venta.Fecha,
            CustomerId = venta.ClienteId,
            Customer = venta.Cliente != null ? new Customer { Id = venta.Cliente.Id, Name = venta.Cliente.NombreCliente } : null,
            VendorId = venta.VendedorId ?? 0,
            Vendor = venta.Vendedor != null ? new Vendor { Id = venta.Vendedor.Id, Name = venta.Vendedor.Nombre } : null,
            TipoComprobante = venta.Comprobante?.Codigo ?? string.Empty,
            NumeroComprobante = venta.NumeroComprobante ?? string.Empty,
            Lines = venta.Lineas.Select(l => new SaleLine
            {
                ProductId = l.ArticuloId,
                Product = l.Articulo != null ? new Product { Id = l.Articulo.Id, Name = l.Articulo.Descripcion } : null,
                Quantity = l.Cantidad,
                UnitPrice = l.PrecioUnitario
            }).ToList()
        };
    }

    #endregion

    public async Task<CommerceSnapshot> GetSnapshotAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        
        var articulos = await db.Articulos.Where(a => a.CompanyId == companyId).ToListAsync();
        var ventas = await db.Ventas
            .Where(v => v.CompanyId == companyId)
            .ToListAsync();
        var vendedores = await db.Vendedores.CountAsync();
        var clientes = await db.Clientes.Where(c => c.CompanyId == companyId).CountAsync();
        
        var monthlySales = ventas.Where(s => s.Fecha >= monthStart).Sum(s => s.Total);
        var avgTicket = ventas.Any() ? Math.Round(ventas.Average(s => s.Total), 2) : 0m;
        var inventoryValue = articulos.Sum(p => p.PrecioLista1); // Simplificado

        return new CommerceSnapshot
        {
            InventoryValue = inventoryValue,
            MonthlySales = monthlySales,
            ActiveVendors = vendedores,
            CustomerCount = clientes,
            AverageTicket = avgTicket
        };
    }

    #region Cobranzas

    public async Task<List<Cobranza>> GetCobranzasAsync(int companyId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        return await db.Cobranzas
            .Include(c => c.Detalles)
            .Include(c => c.ComprobantesAplicados)
            .Where(c => c.CompanyId == companyId)
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();
    }

    public async Task<List<VentaPendiente>> GetFacturasPendientesClienteAsync(int companyId, int clienteId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        
        // Obtener comprobantes que afectan cuenta corriente con signo positivo (facturas)
        var comprobantesQueAfectan = await db.Comprobantes
            .Where(c => c.Afectacc && c.SignoCC > 0)
            .Select(c => c.Codigo)
            .ToListAsync();

        // Obtener ventas pendientes
        var ventas = await db.VentasAMRO
            .Where(v => v.CompanyId == companyId && 
                       v.ClienteId == clienteId && 
                       !v.Anulado &&
                       comprobantesQueAfectan.Contains(v.CodigoComprobante))
            .OrderBy(v => v.Fecha)
            .ToListAsync();

        // Obtener pagos aplicados a cada venta
        var ventaIds = ventas.Select(v => v.Id).ToList();
        var pagosAplicados = await db.CobranzasComprobantes
            .Where(cc => ventaIds.Contains(cc.VentaId))
            .GroupBy(cc => cc.VentaId)
            .Select(g => new { VentaId = g.Key, TotalPagado = g.Sum(cc => cc.MontoAplicado) })
            .ToListAsync();

        var pagosDict = pagosAplicados.ToDictionary(p => p.VentaId, p => p.TotalPagado);

        return ventas.Select(v => new VentaPendiente
        {
            Id = v.Id,
            CodigoComprobante = v.CodigoComprobante,
            NumeroComprobante = v.NumeroComprobante,
            Fecha = v.Fecha,
            FechaVencimiento = v.FechaVencimiento,
            Total = v.Total,
            TotalPagado = pagosDict.GetValueOrDefault(v.Id, 0)
        }).Where(v => v.Total - v.TotalPagado > 0.01m).ToList();
    }

    public async Task SaveCobranzaAsync(Cobranza cobranza)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();

        if (cobranza.Id == 0)
        {
            // Nueva cobranza - generar número
            var ultimoNumero = await db.Cobranzas
                .Where(c => c.CompanyId == cobranza.CompanyId)
                .MaxAsync(c => (int?)c.Id) ?? 0;
            cobranza.NumeroComprobante = $"REC-{(ultimoNumero + 1):D6}";
            cobranza.FechaAlta = DateTime.Now;
            
            db.Cobranzas.Add(cobranza);
            await db.SaveChangesAsync();

            // Crear movimiento en cuenta corriente
            var ultimoMovimiento = await db.MovimientosCuentaCorriente
                .Where(m => m.CompanyId == cobranza.CompanyId && m.ClienteId == cobranza.ClienteId)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();

            var saldoAnterior = ultimoMovimiento?.Saldo ?? 0;
            var nuevoSaldo = saldoAnterior - cobranza.Total; // Cobranza resta del saldo

            var movimiento = new MovimientoCuentaCorriente
            {
                CompanyId = cobranza.CompanyId,
                ClienteId = cobranza.ClienteId,
                Fecha = cobranza.Fecha,
                TipoMovimiento = "COBRANZA",
                CodigoComprobante = "REC",
                NumeroComprobante = cobranza.NumeroComprobante,
                CobranzaId = cobranza.Id,
                Descripcion = $"Cobranza - {cobranza.NombreCliente}",
                Debe = 0,
                Haber = cobranza.Total,
                Saldo = nuevoSaldo,
                UsuarioId = cobranza.UsuarioId
            };

            db.MovimientosCuentaCorriente.Add(movimiento);
            await db.SaveChangesAsync();
        }
        else
        {
            // Actualizar cobranza existente
            cobranza.FechaModificacion = DateTime.Now;
            db.Cobranzas.Update(cobranza);
            await db.SaveChangesAsync();
        }
    }

    public async Task AnularCobranzaAsync(int cobranzaId, int? usuarioId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();

        var cobranza = await db.Cobranzas.FindAsync(cobranzaId);
        if (cobranza == null) return;

        cobranza.Estado = "Anulado";
        cobranza.FechaModificacion = DateTime.Now;

        // Crear movimiento reverso en cuenta corriente
        var ultimoMovimiento = await db.MovimientosCuentaCorriente
            .Where(m => m.CompanyId == cobranza.CompanyId && m.ClienteId == cobranza.ClienteId)
            .OrderByDescending(m => m.Id)
            .FirstOrDefaultAsync();

        var saldoAnterior = ultimoMovimiento?.Saldo ?? 0;
        var nuevoSaldo = saldoAnterior + cobranza.Total; // Anulación suma al saldo

        var movimiento = new MovimientoCuentaCorriente
        {
            CompanyId = cobranza.CompanyId,
            ClienteId = cobranza.ClienteId,
            Fecha = DateTime.Now,
            TipoMovimiento = "ANULACION",
            CodigoComprobante = "ANU",
            NumeroComprobante = cobranza.NumeroComprobante,
            CobranzaId = cobranza.Id,
            Descripcion = $"Anulación Cobranza - {cobranza.NombreCliente}",
            Debe = cobranza.Total,
            Haber = 0,
            Saldo = nuevoSaldo,
            UsuarioId = usuarioId
        };

        db.MovimientosCuentaCorriente.Add(movimiento);
        await db.SaveChangesAsync();
    }

    public async Task<decimal> GetSaldoClienteAsync(int companyId, int clienteId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();

        var ultimoMovimiento = await db.MovimientosCuentaCorriente
            .Where(m => m.CompanyId == companyId && m.ClienteId == clienteId)
            .OrderByDescending(m => m.Id)
            .FirstOrDefaultAsync();

        return ultimoMovimiento?.Saldo ?? 0;
    }

    public async Task<List<MovimientoCuentaCorriente>> GetMovimientosCCClienteAsync(int companyId, int clienteId, DateTime? desde = null, DateTime? hasta = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();

        var query = db.MovimientosCuentaCorriente
            .Where(m => m.CompanyId == companyId && m.ClienteId == clienteId);

        if (desde.HasValue)
            query = query.Where(m => m.Fecha >= desde.Value);
        if (hasta.HasValue)
            query = query.Where(m => m.Fecha <= hasta.Value);

        return await query.OrderBy(m => m.Fecha).ThenBy(m => m.Id).ToListAsync();
    }

    /// <summary>
    /// Genera movimiento en cuenta corriente al guardar una venta (si el comprobante afecta CC)
    /// </summary>
    public async Task GenerarMovimientoCCVentaAsync(VentaAMRO venta)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();

        // Verificar si el comprobante afecta cuenta corriente
        var comprobante = await db.Comprobantes
            .FirstOrDefaultAsync(c => c.Codigo == venta.CodigoComprobante);

        if (comprobante == null || !comprobante.Afectacc) return;

        var ultimoMovimiento = await db.MovimientosCuentaCorriente
            .Where(m => m.CompanyId == venta.CompanyId && m.ClienteId == venta.ClienteId)
            .OrderByDescending(m => m.Id)
            .FirstOrDefaultAsync();

        var saldoAnterior = ultimoMovimiento?.Saldo ?? 0;
        decimal debe = 0, haber = 0;

        if (comprobante.SignoCC > 0)
        {
            debe = venta.Total;
            // Factura suma al saldo
        }
        else
        {
            haber = venta.Total;
            // NC resta del saldo
        }

        var nuevoSaldo = saldoAnterior + debe - haber;

        var movimiento = new MovimientoCuentaCorriente
        {
            CompanyId = venta.CompanyId,
            ClienteId = venta.ClienteId,
            Fecha = venta.Fecha,
            TipoMovimiento = "VENTA",
            CodigoComprobante = venta.CodigoComprobante,
            NumeroComprobante = venta.NumeroComprobante,
            VentaId = venta.Id,
            Descripcion = $"{comprobante.Descripcion} - {venta.NombreCliente}",
            Debe = debe,
            Haber = haber,
            Saldo = nuevoSaldo,
            UsuarioId = venta.UsuarioId
        };

        db.MovimientosCuentaCorriente.Add(movimiento);
        await db.SaveChangesAsync();
    }

    #endregion
}
