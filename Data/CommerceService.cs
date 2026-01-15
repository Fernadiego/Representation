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
        return zonas.Select(z => new ZonaItem { Id = z.Id, Nombre = z.Nombre }).ToList();
    }

    public async Task<List<VendedorItem>> GetVendedoresAbmAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var vendedores = await db.Vendedores.ToListAsync();
        return vendedores.Select(v => new VendedorItem { Id = v.Id, Nombre = v.Nombre }).ToList();
    }

    public async Task<List<CobradorItem>> GetCobradoresAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var cobradores = await db.Cobradores.ToListAsync();
        return cobradores.Select(c => new CobradorItem { Id = c.Id, Nombre = c.Nombre }).ToList();
    }

    public async Task<List<ClaseClienteItem>> GetClasesClienteAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommerceDbContext>();
        var clases = await db.ClaseClientes.ToListAsync();
        return clases.Select(c => new ClaseClienteItem { Id = c.Id, Nombre = c.Nombre }).ToList();
    }

    // ABMs para Comprobantes
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
            Tipo = c.Tipo, 
            Numeracion = c.Numeracion 
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
                Tipo = comprobante.Tipo, 
                Numeracion = comprobante.Numeracion 
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
                existing.Tipo = comprobante.Tipo;
                existing.Numeracion = comprobante.Numeracion;
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
        // Los vendedores de ventas aún están en memoria - se pueden migrar a BD si se necesita
        return await Task.FromResult(new List<Vendor>());
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
            NumeroProveedor = c.NumeroProveedor,
            DomicilioEntrega = c.DomicilioEntrega,
            DomicilioLegal = c.DomicilioLegal,
            LocalidadId = c.LocalidadId,
            LocalidadNombre = c.Localidad?.Nombre,
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
            TipoClienteId = c.TipoClienteId,
            TipoClienteNombre = c.TipoCliente?.Descripcion,
            CondicionPago = c.CondicionPago,
            ZonaId = c.ZonaId,
            ZonaNombre = c.Zona?.Nombre,
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
                NumeroProveedor = customer.NumeroProveedor,
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
            existing.NumeroProveedor = customer.NumeroProveedor;
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
            .Include(a => a.Cliente)
            .Where(a => a.CompanyId == companyId)
            .ToListAsync();
        
        return articulos.Select(a => new Product
        {
            Id = a.Id,
            CompanyId = a.CompanyId,
            CodigoArticulo = a.CodigoArticulo,
            CodigoParaMostrar = a.CodigoParaMostrar,
            Name = a.Descripcion,
            Description = a.MensajeSobreArticulo,
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
            Inhabilitado = a.Inhabilitado,
            MensajeSobreArticulo = a.MensajeSobreArticulo,
            TieneMensaje = !string.IsNullOrEmpty(a.MensajeSobreArticulo),
            ClienteId = a.ClienteId,
            ClienteNombre = a.Cliente?.NombreCliente
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
                Inhabilitado = product.Inhabilitado,
                MensajeSobreArticulo = product.MensajeSobreArticulo,
                ClienteId = product.ClienteId
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
            existing.MensajeSobreArticulo = product.MensajeSobreArticulo;
            existing.ClienteId = product.ClienteId;
            
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
}
