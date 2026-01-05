using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using BlazorVentas.Services;
using BlazorVentas.Data.Models;

namespace BlazorVentas.Data;

public class CommerceService
{
    private readonly object _gate = new();
    private readonly IServiceProvider? _serviceProvider;

    private readonly List<Company> _companies = new();
    private readonly Dictionary<int, TenantData> _tenants = new();

    // Listas globales de ABM - Artículos
    private readonly List<MarcaItem> _marcas = new();
    private readonly List<PaisItem> _paises = new();
    private readonly List<TipoEnvaseItem> _tiposEnvase = new();
    private int _marcaSeq = 1;
    private int _paisSeq = 1;
    private int _tipoEnvaseSeq = 1;

    // Listas globales de ABM - Clientes
    private readonly List<ProvinciaItem> _provincias = new();
    private readonly List<LocalidadItem> _localidades = new();
    private readonly List<DescuentoItem> _descuentos = new();
    private readonly List<TipoClienteItem> _tiposCliente = new();
    private readonly List<ZonaItem> _zonas = new();
    private readonly List<VendedorItem> _vendedoresAbm = new();
    private readonly List<CobradorItem> _cobradores = new();
    private readonly List<ClaseClienteItem> _clasesCliente = new();
    private int _provinciaSeq = 1;
    private int _localidadSeq = 1;
    private int _descuentoSeq = 1;
    private int _tipoClienteSeq = 1;
    private int _zonaSeq = 1;
    private int _vendedorAbmSeq = 1;
    private int _cobradorSeq = 1;
    private int _claseClienteSeq = 1;

    // Listas globales de ABM - Comprobantes
    private readonly List<ComprobanteItem> _comprobantes = new();
    private int _comprobanteSeq = 1;

    // Listas globales de ABM - Tipos Comprobantes
    private readonly List<TipoComprobanteItem> _tiposComprobante = new();
    private int _tipoComprobanteSeq = 1;

    public CommerceService(IServiceProvider? serviceProvider = null)
    {
        _serviceProvider = serviceProvider;
        SeedAbm();
        Seed();
    }
    
    private async Task RegistrarBitacoraAsync(int? usuarioId, string modulo, string accion, string? detalle = null)
    {
        if (!usuarioId.HasValue || usuarioId.Value == 0 || _serviceProvider == null)
            return;
            
        try
        {
            var bitacoraService = _serviceProvider.GetService<BitacoraService>();
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

    private void SeedAbm()
    {
        // Marcas
        _marcas.AddRange(new[]
        {
            new MarcaItem { Id = _marcaSeq++, Nombre = "La Campagnola" },
            new MarcaItem { Id = _marcaSeq++, Nombre = "Arcor" },
            new MarcaItem { Id = _marcaSeq++, Nombre = "Marolio" },
            new MarcaItem { Id = _marcaSeq++, Nombre = "Knorr" },
            new MarcaItem { Id = _marcaSeq++, Nombre = "Hellmann's" }
        });

        // Países
        _paises.AddRange(new[]
        {
            new PaisItem { Id = _paisSeq++, Nombre = "Argentina" },
            new PaisItem { Id = _paisSeq++, Nombre = "Brasil" },
            new PaisItem { Id = _paisSeq++, Nombre = "Chile" },
            new PaisItem { Id = _paisSeq++, Nombre = "Uruguay" },
            new PaisItem { Id = _paisSeq++, Nombre = "Paraguay" },
            new PaisItem { Id = _paisSeq++, Nombre = "España" },
            new PaisItem { Id = _paisSeq++, Nombre = "Italia" }
        });

        // Tipos de Envase
        _tiposEnvase.AddRange(new[]
        {
            new TipoEnvaseItem { Id = _tipoEnvaseSeq++, Nombre = "Frasco" },
            new TipoEnvaseItem { Id = _tipoEnvaseSeq++, Nombre = "Lata" },
            new TipoEnvaseItem { Id = _tipoEnvaseSeq++, Nombre = "Caja" },
            new TipoEnvaseItem { Id = _tipoEnvaseSeq++, Nombre = "Bolsa" },
            new TipoEnvaseItem { Id = _tipoEnvaseSeq++, Nombre = "Botella" },
            new TipoEnvaseItem { Id = _tipoEnvaseSeq++, Nombre = "Sachet" }
        });

        // Provincias
        _provincias.AddRange(new[]
        {
            new ProvinciaItem { Id = _provinciaSeq++, Nombre = "Buenos Aires" },
            new ProvinciaItem { Id = _provinciaSeq++, Nombre = "CABA" },
            new ProvinciaItem { Id = _provinciaSeq++, Nombre = "Córdoba" },
            new ProvinciaItem { Id = _provinciaSeq++, Nombre = "Santa Fe" },
            new ProvinciaItem { Id = _provinciaSeq++, Nombre = "Mendoza" }
        });

        // Localidades
        _localidades.AddRange(new[]
        {
            new LocalidadItem { Id = _localidadSeq++, Nombre = "La Plata", ProvinciaId = 1 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Mar del Plata", ProvinciaId = 1 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Quilmes", ProvinciaId = 1 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Palermo", ProvinciaId = 2 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Recoleta", ProvinciaId = 2 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Córdoba Capital", ProvinciaId = 3 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Rosario", ProvinciaId = 4 },
            new LocalidadItem { Id = _localidadSeq++, Nombre = "Mendoza Capital", ProvinciaId = 5 }
        });

        // Descuentos
        _descuentos.AddRange(new[]
        {
            new DescuentoItem { Id = _descuentoSeq++, Codigo = "DESC01", Descripcion = "Descuento 5%", Porcentaje = 5 },
            new DescuentoItem { Id = _descuentoSeq++, Codigo = "DESC02", Descripcion = "Descuento 10%", Porcentaje = 10 },
            new DescuentoItem { Id = _descuentoSeq++, Codigo = "DESC03", Descripcion = "Descuento 15%", Porcentaje = 15 },
            new DescuentoItem { Id = _descuentoSeq++, Codigo = "DESC04", Descripcion = "Descuento Mayorista", Porcentaje = 20 }
        });

        // Tipos de Cliente
        _tiposCliente.AddRange(new[]
        {
            new TipoClienteItem { Id = _tipoClienteSeq++, Nombre = "Minorista" },
            new TipoClienteItem { Id = _tipoClienteSeq++, Nombre = "Mayorista" },
            new TipoClienteItem { Id = _tipoClienteSeq++, Nombre = "Distribuidor" },
            new TipoClienteItem { Id = _tipoClienteSeq++, Nombre = "Consumidor Final" }
        });

        // Zonas
        _zonas.AddRange(new[]
        {
            new ZonaItem { Id = _zonaSeq++, Nombre = "Zona Norte" },
            new ZonaItem { Id = _zonaSeq++, Nombre = "Zona Sur" },
            new ZonaItem { Id = _zonaSeq++, Nombre = "Zona Este" },
            new ZonaItem { Id = _zonaSeq++, Nombre = "Zona Oeste" },
            new ZonaItem { Id = _zonaSeq++, Nombre = "Centro" }
        });

        // Vendedores
        _vendedoresAbm.AddRange(new[]
        {
            new VendedorItem { Id = _vendedorAbmSeq++, Nombre = "Juan Pérez" },
            new VendedorItem { Id = _vendedorAbmSeq++, Nombre = "María García" },
            new VendedorItem { Id = _vendedorAbmSeq++, Nombre = "Carlos López" },
            new VendedorItem { Id = _vendedorAbmSeq++, Nombre = "Ana Martínez" }
        });

        // Cobradores
        _cobradores.AddRange(new[]
        {
            new CobradorItem { Id = _cobradorSeq++, Nombre = "Roberto Sánchez" },
            new CobradorItem { Id = _cobradorSeq++, Nombre = "Laura Fernández" },
            new CobradorItem { Id = _cobradorSeq++, Nombre = "Diego Rodríguez" }
        });

        // Clases de Cliente
        _clasesCliente.AddRange(new[]
        {
            new ClaseClienteItem { Id = _claseClienteSeq++, Nombre = "A - Premium" },
            new ClaseClienteItem { Id = _claseClienteSeq++, Nombre = "B - Estándar" },
            new ClaseClienteItem { Id = _claseClienteSeq++, Nombre = "C - Básico" }
        });

        // Comprobantes (basados en la imagen)
        _comprobantes.AddRange(new[]
        {
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "CHQ", Descripcion = "CHEQUE", Tipo = 7, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "CIN", Descripcion = "CREDITO", Tipo = 4, Numeracion = "CIN" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "DEP", Descripcion = "EFECTIVO", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "DIN", Descripcion = "DEBITO", Tipo = 3, Numeracion = "DIN" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "DLR", Descripcion = "DOLARES", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "DOC", Descripcion = "DOCUM.", Tipo = 8, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "EFC", Descripcion = "EFECTIVO", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FAC", Descripcion = "FACTURA", Tipo = 1, Numeracion = "FU" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FB", Descripcion = "FACTURA", Tipo = 1, Numeracion = "FUB" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FBC", Descripcion = "CONTADO", Tipo = 1, Numeracion = "FUB" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FCD", Descripcion = "F.CRED.", Tipo = 9, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FCE", Descripcion = "F.CRED.", Tipo = 3, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FUC", Descripcion = "CONTADO", Tipo = 1, Numeracion = "FU" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "FUE", Descripcion = "FACTURA", Tipo = 1, Numeracion = "FUE" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "LCP", Descripcion = "EFECTIVO", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "NC", Descripcion = "N.CRED.", Tipo = 4, Numeracion = "NCU" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "NCB", Descripcion = "N.CREDIT", Tipo = 4, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "ND", Descripcion = "N.DEBITO", Tipo = 3, Numeracion = "NDU" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "NDB", Descripcion = "N.DEBITO", Tipo = 3, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "NP", Descripcion = "N.PEDIDO", Tipo = 0, Numeracion = "NPU" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "OTR", Descripcion = "OTROS", Tipo = 9, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "PA", Descripcion = "ANULA NP", Tipo = 0, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "PAT", Descripcion = "PATACON", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "REC", Descripcion = "RECIBO", Tipo = 5, Numeracion = "REC" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "RGN", Descripcion = "RET.GAN", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "RIB", Descripcion = "RET.IIBB", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "RIV", Descripcion = "RET.IVA", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "TCK", Descripcion = "TICKET", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "U$$", Descripcion = "DOLARES", Tipo = 6, Numeracion = "" },
            new ComprobanteItem { Id = _comprobanteSeq++, Codigo = "VTO", Descripcion = "VENCIM.", Tipo = 3, Numeracion = "" }
        });

        // Tipos Comprobantes
        _tiposComprobante.AddRange(new[]
        {
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Factura A" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Factura B" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Factura C" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Crédito A" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Crédito B" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Crédito C" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Débito A" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Débito B" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Débito C" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Nota de Pedido" },
            new TipoComprobanteItem { Id = _tipoComprobanteSeq++, Nombre = "Remito" }
        });
    }

    #region ABM Listas

    public Task<List<MarcaItem>> GetMarcasAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_marcas.Select(m => new MarcaItem { Id = m.Id, Nombre = m.Nombre }).ToList());
        }
    }

    public Task SaveMarcaAsync(MarcaItem marca)
    {
        lock (_gate)
        {
            if (marca.Id == 0)
            {
                marca.Id = _marcaSeq++;
                _marcas.Add(new MarcaItem { Id = marca.Id, Nombre = marca.Nombre });
            }
            else
            {
                var existing = _marcas.FirstOrDefault(m => m.Id == marca.Id);
                if (existing != null) existing.Nombre = marca.Nombre;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeleteMarcaAsync(int id)
    {
        lock (_gate)
        {
            _marcas.RemoveAll(m => m.Id == id);
        }
        return Task.CompletedTask;
    }

    public Task<List<PaisItem>> GetPaisesAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_paises.Select(p => new PaisItem { Id = p.Id, Nombre = p.Nombre }).ToList());
        }
    }

    public Task SavePaisAsync(PaisItem pais)
    {
        lock (_gate)
        {
            if (pais.Id == 0)
            {
                pais.Id = _paisSeq++;
                _paises.Add(new PaisItem { Id = pais.Id, Nombre = pais.Nombre });
            }
            else
            {
                var existing = _paises.FirstOrDefault(p => p.Id == pais.Id);
                if (existing != null) existing.Nombre = pais.Nombre;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeletePaisAsync(int id)
    {
        lock (_gate)
        {
            _paises.RemoveAll(p => p.Id == id);
        }
        return Task.CompletedTask;
    }

    public Task<List<TipoEnvaseItem>> GetTiposEnvaseAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_tiposEnvase.Select(t => new TipoEnvaseItem { Id = t.Id, Nombre = t.Nombre }).ToList());
        }
    }

    public Task SaveTipoEnvaseAsync(TipoEnvaseItem tipoEnvase)
    {
        lock (_gate)
        {
            if (tipoEnvase.Id == 0)
            {
                tipoEnvase.Id = _tipoEnvaseSeq++;
                _tiposEnvase.Add(new TipoEnvaseItem { Id = tipoEnvase.Id, Nombre = tipoEnvase.Nombre });
            }
            else
            {
                var existing = _tiposEnvase.FirstOrDefault(t => t.Id == tipoEnvase.Id);
                if (existing != null) existing.Nombre = tipoEnvase.Nombre;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeleteTipoEnvaseAsync(int id)
    {
        lock (_gate)
        {
            _tiposEnvase.RemoveAll(t => t.Id == id);
        }
        return Task.CompletedTask;
    }

    // ABMs para Clientes
    public Task<List<ProvinciaItem>> GetProvinciasAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_provincias.Select(p => new ProvinciaItem { Id = p.Id, Nombre = p.Nombre }).ToList());
        }
    }

    public Task<List<LocalidadItem>> GetLocalidadesAsync(int? provinciaId = null)
    {
        lock (_gate)
        {
            var query = _localidades.AsEnumerable();
            if (provinciaId.HasValue)
                query = query.Where(l => l.ProvinciaId == provinciaId.Value);
            return Task.FromResult(query.Select(l => new LocalidadItem { Id = l.Id, Nombre = l.Nombre, ProvinciaId = l.ProvinciaId }).ToList());
        }
    }

    public Task<List<DescuentoItem>> GetDescuentosAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_descuentos.Select(d => new DescuentoItem { Id = d.Id, Codigo = d.Codigo, Descripcion = d.Descripcion, Porcentaje = d.Porcentaje }).ToList());
        }
    }

    public Task<List<TipoClienteItem>> GetTiposClienteAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_tiposCliente.Select(t => new TipoClienteItem { Id = t.Id, Nombre = t.Nombre }).ToList());
        }
    }

    public Task<List<ZonaItem>> GetZonasAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_zonas.Select(z => new ZonaItem { Id = z.Id, Nombre = z.Nombre }).ToList());
        }
    }

    public Task<List<VendedorItem>> GetVendedoresAbmAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_vendedoresAbm.Select(v => new VendedorItem { Id = v.Id, Nombre = v.Nombre }).ToList());
        }
    }

    public Task<List<CobradorItem>> GetCobradoresAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_cobradores.Select(c => new CobradorItem { Id = c.Id, Nombre = c.Nombre }).ToList());
        }
    }

    public Task<List<ClaseClienteItem>> GetClasesClienteAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_clasesCliente.Select(c => new ClaseClienteItem { Id = c.Id, Nombre = c.Nombre }).ToList());
        }
    }

    // ABMs para Comprobantes
    public Task<List<ComprobanteItem>> GetComprobantesAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_comprobantes.Select(c => new ComprobanteItem 
            { 
                Id = c.Id, 
                Codigo = c.Codigo, 
                Descripcion = c.Descripcion, 
                Tipo = c.Tipo, 
                Numeracion = c.Numeracion 
            }).ToList());
        }
    }

    public async Task SaveComprobanteAsync(ComprobanteItem comprobante, int? usuarioId = null)
    {
        lock (_gate)
        {
            if (comprobante.Id == 0)
            {
                comprobante.Id = _comprobanteSeq++;
                _comprobantes.Add(new ComprobanteItem 
                { 
                    Id = comprobante.Id, 
                    Codigo = comprobante.Codigo, 
                    Descripcion = comprobante.Descripcion, 
                    Tipo = comprobante.Tipo, 
                    Numeracion = comprobante.Numeracion 
                });
            }
            else
            {
                var existing = _comprobantes.FirstOrDefault(c => c.Id == comprobante.Id);
                if (existing != null)
                {
                    existing.Codigo = comprobante.Codigo;
                    existing.Descripcion = comprobante.Descripcion;
                    existing.Tipo = comprobante.Tipo;
                    existing.Numeracion = comprobante.Numeracion;
                }
            }
        }
        
        // Registrar en bitácora
        var accion = comprobante.Id == 0 ? "Alta de comprobante" : "Modificación de comprobante";
        var detalle = $"Comprobante: {comprobante.Descripcion} (Código: {comprobante.Codigo})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Comprobantes", accion, detalle);
    }

    public async Task DeleteComprobanteAsync(int id, int? usuarioId = null)
    {
        string? descripcion = null;
        lock (_gate)
        {
            var comprobante = _comprobantes.FirstOrDefault(c => c.Id == id);
            descripcion = comprobante?.Descripcion;
            _comprobantes.RemoveAll(c => c.Id == id);
        }
        
        // Registrar en bitácora
        var detalle = descripcion != null ? $"Comprobante eliminado: {descripcion} (ID: {id})" : $"Comprobante eliminado (ID: {id})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Comprobantes", "Eliminación de comprobante", detalle);
    }

    // ABMs para Tipos Comprobantes
    public Task<List<TipoComprobanteItem>> GetTiposComprobanteAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_tiposComprobante.Select(t => new TipoComprobanteItem { Id = t.Id, Nombre = t.Nombre }).ToList());
        }
    }

    public async Task SaveTipoComprobanteAsync(TipoComprobanteItem tipoComprobante, int? usuarioId = null)
    {
        lock (_gate)
        {
            if (tipoComprobante.Id == 0)
            {
                tipoComprobante.Id = _tipoComprobanteSeq++;
                _tiposComprobante.Add(new TipoComprobanteItem { Id = tipoComprobante.Id, Nombre = tipoComprobante.Nombre });
            }
            else
            {
                var existing = _tiposComprobante.FirstOrDefault(t => t.Id == tipoComprobante.Id);
                if (existing != null) existing.Nombre = tipoComprobante.Nombre;
            }
        }
        
        // Registrar en bitácora
        var accion = tipoComprobante.Id == 0 ? "Alta de tipo comprobante" : "Modificación de tipo comprobante";
        var detalle = $"Tipo Comprobante: {tipoComprobante.Nombre}";
        await RegistrarBitacoraAsync(usuarioId, "ABM Tipos Comprobantes", accion, detalle);
    }

    public async Task DeleteTipoComprobanteAsync(int id, int? usuarioId = null)
    {
        string? nombre = null;
        lock (_gate)
        {
            var tipo = _tiposComprobante.FirstOrDefault(t => t.Id == id);
            nombre = tipo?.Nombre;
            _tiposComprobante.RemoveAll(t => t.Id == id);
        }
        
        // Registrar en bitácora
        var detalle = nombre != null ? $"Tipo Comprobante eliminado: {nombre} (ID: {id})" : $"Tipo Comprobante eliminado (ID: {id})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Tipos Comprobantes", "Eliminación de tipo comprobante", detalle);
    }

    #endregion

    #region Companies

    public Task<List<Company>> GetCompaniesAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_companies.Select(CloneCompany).ToList());
        }
    }

    public Task SaveCompanyAsync(Company company)
    {
        ArgumentNullException.ThrowIfNull(company);

        lock (_gate)
        {
            if (company.Id == 0)
            {
                // Alta
                var nextId = _companies.Any() ? _companies.Max(c => c.Id) + 1 : 1;
                company.Id = nextId;
                _companies.Add(CloneCompany(company));

                // Crear tenant vacío para la nueva empresa
                if (!_tenants.ContainsKey(company.Id))
                {
                    _tenants[company.Id] = new TenantData();
                }
            }
            else
            {
                // Edición
                var existing = _companies.FirstOrDefault(c => c.Id == company.Id)
                    ?? throw new InvalidOperationException("Empresa no encontrada");
                existing.Name = company.Name;
                existing.Color = company.Color;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteCompanyAsync(int companyId)
    {
        lock (_gate)
        {
            _companies.RemoveAll(c => c.Id == companyId);
            if (_tenants.ContainsKey(companyId))
            {
                _tenants.Remove(companyId);
            }
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Suppliers

    public Task<List<Supplier>> GetSuppliersAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Suppliers.Select(CloneSupplier).ToList());
        }
    }

    public Task SaveSupplierAsync(int companyId, Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (supplier.Id == 0)
            {
                supplier.Id = tenant.SupplierSeq++;
                supplier.CompanyId = companyId;
                tenant.Suppliers.Add(CloneSupplier(supplier));
            }
            else
            {
                var existing = tenant.Suppliers.FirstOrDefault(s => s.Id == supplier.Id) ?? throw new InvalidOperationException("Proveedor no encontrado");
                existing.Name = supplier.Name;
                existing.ContactName = supplier.ContactName;
                existing.Phone = supplier.Phone;
                existing.Email = supplier.Email;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteSupplierAsync(int companyId, int supplierId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            tenant.Suppliers.RemoveAll(s => s.Id == supplierId);
            foreach (var product in tenant.Products.Where(p => p.SupplierId == supplierId))
            {
                product.SupplierId = 0;
            }
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Vendors

    public Task<List<Vendor>> GetVendorsAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Vendors.Select(CloneVendor).ToList());
        }
    }

    public Task SaveVendorAsync(int companyId, Vendor vendor)
    {
        ArgumentNullException.ThrowIfNull(vendor);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (vendor.Id == 0)
            {
                vendor.Id = tenant.VendorSeq++;
                vendor.CompanyId = companyId;
                tenant.Vendors.Add(CloneVendor(vendor));
            }
            else
            {
                var existing = tenant.Vendors.FirstOrDefault(v => v.Id == vendor.Id) ?? throw new InvalidOperationException("Vendedor no encontrado");
                existing.Name = vendor.Name;
                existing.CommissionRate = vendor.CommissionRate;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteVendorAsync(int companyId, int vendorId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            tenant.Vendors.RemoveAll(v => v.Id == vendorId);
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Customers

    public Task<List<Customer>> GetCustomersAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Customers.Select(CloneCustomer).ToList());
        }
    }

    public Task SaveCustomerAsync(int companyId, Customer customer, int? usuarioId = null)
    {
        ArgumentNullException.ThrowIfNull(customer);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (customer.Id == 0)
            {
                customer.Id = tenant.CustomerSeq++;
                customer.CompanyId = companyId;
                if (customer.FechaAlta == default(DateTime))
                    customer.FechaAlta = DateTime.Today;
                tenant.Customers.Add(CloneCustomer(customer));
            }
            else
            {
                var existing = tenant.Customers.FirstOrDefault(c => c.Id == customer.Id) ?? throw new InvalidOperationException("Cliente no encontrado");
                existing.CodigoCliente = customer.CodigoCliente;
                existing.CodigoSucursal = customer.CodigoSucursal;
                existing.CodigoParaMostrar = customer.CodigoParaMostrar;
                existing.Name = customer.Name;
                existing.NombreSucursal = customer.NombreSucursal;
                existing.NumeroProveedor = customer.NumeroProveedor;
                existing.DomicilioEntrega = customer.DomicilioEntrega;
                existing.DomicilioLegal = customer.DomicilioLegal;
                existing.LocalidadId = customer.LocalidadId;
                existing.LocalidadNombre = customer.LocalidadNombre;
                existing.ProvinciaId = customer.ProvinciaId;
                existing.ProvinciaNombre = customer.ProvinciaNombre;
                existing.CP = customer.CP;
                existing.Phone = customer.Phone;
                existing.Email = customer.Email;
                existing.Web = customer.Web;
                existing.Contacto = customer.Contacto;
                existing.Cuit = customer.Cuit;
                existing.ListaPrecio = customer.ListaPrecio;
                existing.CodigoDescuentoId = customer.CodigoDescuentoId;
                existing.CodigoDescuentoNombre = customer.CodigoDescuentoNombre;
                existing.TipoClienteId = customer.TipoClienteId;
                existing.TipoClienteNombre = customer.TipoClienteNombre;
                existing.CondicionPago = customer.CondicionPago;
                existing.ZonaId = customer.ZonaId;
                existing.ZonaNombre = customer.ZonaNombre;
                existing.VendedorId = customer.VendedorId;
                existing.VendedorNombre = customer.VendedorNombre;
                existing.CobradorId = customer.CobradorId;
                existing.CobradorNombre = customer.CobradorNombre;
                existing.ClaseClienteId = customer.ClaseClienteId;
                existing.ClaseClienteNombre = customer.ClaseClienteNombre;
                existing.FechaUltimaCompra = customer.FechaUltimaCompra;
                existing.FechaAlta = customer.FechaAlta;
                existing.Inhabilitado = customer.Inhabilitado;
                existing.MensajeSobreCliente = customer.MensajeSobreCliente;
                existing.TieneMensaje = customer.TieneMensaje;
            }
        }

        // Registrar en bitácora
        var accion = customer.Id == 0 ? "Alta de cliente" : "Modificación de cliente";
        var detalle = $"Cliente: {customer.Name} (Código: {customer.CodigoCliente})";
        _ = Task.Run(async () => await RegistrarBitacoraAsync(usuarioId, "ABM Clientes", accion, detalle));

        return Task.CompletedTask;
    }

    public Task DeleteCustomerAsync(int companyId, int customerId, int? usuarioId = null)
    {
        string nombreCliente = string.Empty;
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            var customer = tenant.Customers.FirstOrDefault(c => c.Id == customerId);
            nombreCliente = customer?.Name ?? string.Empty;
            tenant.Customers.RemoveAll(c => c.Id == customerId);
        }

        // Registrar en bitácora
        var detalle = !string.IsNullOrEmpty(nombreCliente) ? $"Cliente eliminado: {nombreCliente} (ID: {customerId})" : $"Cliente eliminado (ID: {customerId})";
        _ = Task.Run(async () => await RegistrarBitacoraAsync(usuarioId, "ABM Clientes", "Eliminación de cliente", detalle));

        return Task.CompletedTask;
    }

    #endregion

    #region Products

    public Task<List<Product>> GetProductsAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Products.Select(p => CloneProduct(tenant, p)).ToList());
        }
    }

    public async Task SaveProductAsync(int companyId, Product product, int? usuarioId = null)
    {
        ArgumentNullException.ThrowIfNull(product);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (product.Id == 0)
            {
                product.Id = tenant.ProductSeq++;
                product.CompanyId = companyId;
                // Si no tiene listas de precios definidas, inicializar con UnitPrice
                if (product.PrecioLista1 == 0) product.PrecioLista1 = product.UnitPrice;
                if (product.PrecioLista2 == 0) product.PrecioLista2 = product.UnitPrice;
                if (product.PrecioLista3 == 0) product.PrecioLista3 = product.UnitPrice;
                if (product.PrecioLista4 == 0) product.PrecioLista4 = product.UnitPrice;
                tenant.Products.Add(CopyProduct(product));
            }
            else
            {
                var existing = tenant.Products.FirstOrDefault(p => p.Id == product.Id) ?? throw new InvalidOperationException("Producto no encontrado");
                existing.CodigoArticulo = product.CodigoArticulo;
                existing.CodigoParaMostrar = product.CodigoParaMostrar;
                existing.Name = product.Name;
                existing.Sku = product.Sku;
                existing.Category = product.Category;
                existing.Description = product.Description;
                existing.UnitPrice = product.UnitPrice;
                existing.Stock = product.Stock;
                existing.MinStock = product.MinStock;
                existing.SupplierId = product.SupplierId;
                existing.MarcaId = product.MarcaId;
                existing.MarcaNombre = product.MarcaNombre;
                existing.OrigenId = product.OrigenId;
                existing.OrigenNombre = product.OrigenNombre;
                existing.PesoNeto = product.PesoNeto;
                existing.PesoEscurrido = product.PesoEscurrido;
                existing.TipoEnvaseId = product.TipoEnvaseId;
                existing.TipoEnvaseNombre = product.TipoEnvaseNombre;
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
                existing.TieneMensaje = product.TieneMensaje;
                existing.ImagePath = product.ImagePath;
            }
        }

        // Registrar en bitácora
        var accion = product.Id == 0 ? "Alta de artículo" : "Modificación de artículo";
        var detalle = $"Artículo: {product.Name} (Código: {product.CodigoArticulo})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Articulos", accion, detalle);
    }

    public async Task DeleteProductAsync(int companyId, int productId, int? usuarioId = null)
    {
        string? nombreProducto = null;
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            var product = tenant.Products.FirstOrDefault(p => p.Id == productId);
            nombreProducto = product?.Name;
            tenant.Products.RemoveAll(p => p.Id == productId);
        }

        // Registrar en bitácora
        var detalle = nombreProducto != null ? $"Artículo eliminado: {nombreProducto} (ID: {productId})" : $"Artículo eliminado (ID: {productId})";
        await RegistrarBitacoraAsync(usuarioId, "ABM Articulos", "Eliminación de artículo", detalle);
    }

    #endregion

    #region Sales

    public Task<List<Sale>> GetSalesAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Sales.Select(s => CloneSale(tenant, s)).ToList());
        }
    }

    public Task<Sale> CreateSaleAsync(int companyId, Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale);
        if (!sale.Lines.Any())
        {
            throw new InvalidOperationException("La venta requiere al menos un detalle.");
        }

        lock (_gate)
        {
            var tenant = GetTenant(companyId);

            sale.Id = tenant.SaleSeq++;
            sale.CompanyId = companyId;

            foreach (var line in sale.Lines)
            {
                if (line.UnitPrice == 0)
                {
                    var product = tenant.Products.FirstOrDefault(p => p.Id == line.ProductId) ?? throw new InvalidOperationException("Producto no encontrado");
                    line.UnitPrice = product.UnitPrice;
                }

                var stockProduct = tenant.Products.FirstOrDefault(p => p.Id == line.ProductId);
                if (stockProduct is not null)
                {
                    stockProduct.Stock = Math.Max(0, stockProduct.Stock - line.Quantity);
                }
            }

            tenant.Sales.Add(CopySale(sale));
            return Task.FromResult(CloneSale(tenant, sale));
        }
    }

    public Task<Sale?> GetSaleByComprobanteAsync(int companyId, string tipoComprobante, string numeroComprobante)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            var sale = tenant.Sales.FirstOrDefault(s => 
                s.TipoComprobante == tipoComprobante && 
                s.NumeroComprobante == numeroComprobante);
            
            return Task.FromResult(sale != null ? CloneSale(tenant, sale) : null);
        }
    }

    #endregion

    public Task<CommerceSnapshot> GetSnapshotAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var monthlySales = tenant.Sales.Where(s => s.Date >= monthStart).Sum(s => s.Total);
            var avgTicket = tenant.Sales.Any() ? Math.Round(tenant.Sales.Average(s => s.Total), 2) : 0m;

            return Task.FromResult(new CommerceSnapshot
            {
                InventoryValue = tenant.Products.Sum(p => p.UnitPrice * p.Stock),
                MonthlySales = monthlySales,
                ActiveVendors = tenant.Vendors.Count,
                CustomerCount = tenant.Customers.Count,
                AverageTicket = avgTicket
            });
        }
    }

    private TenantData GetTenant(int companyId)
    {
        if (!_tenants.TryGetValue(companyId, out var tenant))
        {
            throw new InvalidOperationException("Empresa no encontrada");
        }

        return tenant;
    }

    private static Company CloneCompany(Company company) => new()
    {
        Id = company.Id,
        Name = company.Name,
        Color = company.Color
    };

    private static Supplier CloneSupplier(Supplier supplier) => new()
    {
        Id = supplier.Id,
        CompanyId = supplier.CompanyId,
        Name = supplier.Name,
        ContactName = supplier.ContactName,
        Phone = supplier.Phone,
        Email = supplier.Email
    };

    private static Vendor CloneVendor(Vendor vendor) => new()
    {
        Id = vendor.Id,
        CompanyId = vendor.CompanyId,
        Name = vendor.Name,
        CommissionRate = vendor.CommissionRate
    };

    private static CustomerBranch CloneBranch(CustomerBranch branch) => new()
    {
        Id = branch.Id,
        CustomerId = branch.CustomerId,
        Name = branch.Name,
        DeliveryAddress = branch.DeliveryAddress
    };

    private static Customer CloneCustomer(Customer customer) => new()
    {
        Id = customer.Id,
        CompanyId = customer.CompanyId,
        CodigoCliente = customer.CodigoCliente,
        CodigoSucursal = customer.CodigoSucursal,
        CodigoParaMostrar = customer.CodigoParaMostrar,
        Name = customer.Name,
        NombreSucursal = customer.NombreSucursal,
        NumeroProveedor = customer.NumeroProveedor,
        DomicilioEntrega = customer.DomicilioEntrega,
        DomicilioLegal = customer.DomicilioLegal,
        LocalidadId = customer.LocalidadId,
        LocalidadNombre = customer.LocalidadNombre,
        ProvinciaId = customer.ProvinciaId,
        ProvinciaNombre = customer.ProvinciaNombre,
        CP = customer.CP,
        Phone = customer.Phone,
        Email = customer.Email,
        Web = customer.Web,
        Contacto = customer.Contacto,
        Cuit = customer.Cuit,
        ListaPrecio = customer.ListaPrecio,
        CodigoDescuentoId = customer.CodigoDescuentoId,
        CodigoDescuentoNombre = customer.CodigoDescuentoNombre,
        TipoClienteId = customer.TipoClienteId,
        TipoClienteNombre = customer.TipoClienteNombre,
        CondicionPago = customer.CondicionPago,
        ZonaId = customer.ZonaId,
        ZonaNombre = customer.ZonaNombre,
        VendedorId = customer.VendedorId,
        VendedorNombre = customer.VendedorNombre,
        CobradorId = customer.CobradorId,
        CobradorNombre = customer.CobradorNombre,
        ClaseClienteId = customer.ClaseClienteId,
        ClaseClienteNombre = customer.ClaseClienteNombre,
        FechaUltimaCompra = customer.FechaUltimaCompra,
        FechaAlta = customer.FechaAlta,
        Inhabilitado = customer.Inhabilitado,
        MensajeSobreCliente = customer.MensajeSobreCliente,
        TieneMensaje = customer.TieneMensaje,
        Address = customer.Address,
        Branches = customer.Branches?.Select(CloneBranch).ToList() ?? new List<CustomerBranch>()
    };

    private static Product CopyProduct(Product product) => new()
    {
        Id = product.Id,
        CompanyId = product.CompanyId,
        CodigoArticulo = product.CodigoArticulo,
        CodigoParaMostrar = product.CodigoParaMostrar,
        Name = product.Name,
        Sku = product.Sku,
        Category = product.Category,
        Description = product.Description,
        UnitPrice = product.UnitPrice,
        Stock = product.Stock,
        MinStock = product.MinStock,
        SupplierId = product.SupplierId,
        MarcaId = product.MarcaId,
        MarcaNombre = product.MarcaNombre,
        OrigenId = product.OrigenId,
        OrigenNombre = product.OrigenNombre,
        PesoNeto = product.PesoNeto,
        PesoEscurrido = product.PesoEscurrido,
        TipoEnvaseId = product.TipoEnvaseId,
        TipoEnvaseNombre = product.TipoEnvaseNombre,
        UnidadXBulto = product.UnidadXBulto,
        EAUN13 = product.EAUN13,
        DUN14 = product.DUN14,
        PrecioLista1 = product.PrecioLista1,
        PrecioLista2 = product.PrecioLista2,
        PrecioLista3 = product.PrecioLista3,
        PrecioLista4 = product.PrecioLista4,
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
        TieneMensaje = product.TieneMensaje,
        ImagePath = product.ImagePath
    };

    private Product CloneProduct(TenantData tenant, Product product)
    {
        var clone = CopyProduct(product);
        var supplier = tenant.Suppliers.FirstOrDefault(s => s.Id == product.SupplierId);
        clone.Supplier = supplier is not null ? CloneSupplier(supplier) : null;
        return clone;
    }

    private static Sale CopySale(Sale sale) => new()
    {
        Id = sale.Id,
        CompanyId = sale.CompanyId,
        Date = sale.Date,
        CustomerId = sale.CustomerId,
        VendorId = sale.VendorId,
        TipoComprobante = sale.TipoComprobante,
        NumeroComprobante = sale.NumeroComprobante,
        SucursalId = sale.SucursalId,
        Vencimiento = sale.Vencimiento,
        ComprobOriginado = sale.ComprobOriginado,
        RemitoAsociado = sale.RemitoAsociado,
        Estado = sale.Estado,
        Lines = sale.Lines.Select(line => new SaleLine
        {
            ProductId = line.ProductId,
            Quantity = line.Quantity,
            UnitPrice = line.UnitPrice
        }).ToList()
    };

    private Sale CloneSale(TenantData tenant, Sale sale)
    {
        return new Sale
        {
            Id = sale.Id,
            CompanyId = sale.CompanyId,
            Date = sale.Date,
            CustomerId = sale.CustomerId,
            Customer = tenant.Customers.FirstOrDefault(c => c.Id == sale.CustomerId) is { } customer ? CloneCustomer(customer) : null,
            VendorId = sale.VendorId,
            Vendor = tenant.Vendors.FirstOrDefault(v => v.Id == sale.VendorId) is { } vendor ? CloneVendor(vendor) : null,
            TipoComprobante = sale.TipoComprobante,
            NumeroComprobante = sale.NumeroComprobante,
            SucursalId = sale.SucursalId,
            Vencimiento = sale.Vencimiento,
            ComprobOriginado = sale.ComprobOriginado,
            RemitoAsociado = sale.RemitoAsociado,
            Estado = sale.Estado,
            Lines = sale.Lines.Select(line =>
            {
                var product = tenant.Products.FirstOrDefault(p => p.Id == line.ProductId);
                return new SaleLine
                {
                    ProductId = line.ProductId,
                    Product = product is not null ? CloneProduct(tenant, product) : null,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice
                };
            }).ToList()
        };
    }

    private void Seed()
    {
        var company1 = new Company { Id = 1, Name = "Cafeterías Regionales" };
        var company2 = new Company { Id = 2, Name = "Distribuciones Express" };

        _companies.Add(company1);
        _companies.Add(company2);
        _tenants[company1.Id] = new TenantData();
        _tenants[company2.Id] = new TenantData();

        SeedCompanyOne(company1.Id);
        SeedCompanyTwo(company2.Id);
    }

    private void SeedCompanyOne(int companyId)
    {
        var tenant = _tenants[companyId];

        var supplier1 = new Supplier { Name = "Tech Importaciones", ContactName = "Laura Díaz", Email = "contacto@techimport.com", Phone = "555-1000" };
        var supplier2 = new Supplier { Name = "Café del Sur", ContactName = "Ana Morales", Email = "hola@cafedelsur.com", Phone = "555-3000" };
        AddSupplier(tenant, companyId, supplier1);
        AddSupplier(tenant, companyId, supplier2);

        var vendor1 = new Vendor { Name = "María Hernández", CommissionRate = 3 };
        var vendor2 = new Vendor { Name = "Jorge Ramírez", CommissionRate = 4 };
        AddVendor(tenant, companyId, vendor1);
        AddVendor(tenant, companyId, vendor2);

        var customer1 = new Customer { Name = "Café Central", Email = "compras@cafecentral.com", Phone = "555-2211" };
        var customer2 = new Customer { Name = "Hotel Miramar", Email = "proveedores@miramar.com", Phone = "555-4422" };
        AddCustomer(tenant, companyId, customer1);
        AddCustomer(tenant, companyId, customer2);

        var product1 = new Product { Name = "Café en grano 1kg", Sku = "CF-001", UnitPrice = 18.5m, Stock = 50, SupplierId = 1, PrecioLista1 = 18.5m, PrecioLista2 = 17.6m, PrecioLista3 = 16.7m, PrecioLista4 = 15.7m };
        var product2 = new Product { Name = "Taza térmica", Sku = "AC-010", UnitPrice = 12.0m, Stock = 120, SupplierId = 1, PrecioLista1 = 12.0m, PrecioLista2 = 11.4m, PrecioLista3 = 10.8m, PrecioLista4 = 10.2m };
        var product3 = new Product { Name = "Molinillo de café", Sku = "AC-020", UnitPrice = 45.0m, Stock = 30, SupplierId = 1, PrecioLista1 = 45.0m, PrecioLista2 = 42.8m, PrecioLista3 = 40.5m, PrecioLista4 = 38.3m };
        var product4 = new Product { Name = "Café espresso 250g", Sku = "CF-002", UnitPrice = 15.0m, Stock = 75, SupplierId = 2, PrecioLista1 = 15.0m, PrecioLista2 = 14.3m, PrecioLista3 = 13.5m, PrecioLista4 = 12.8m };
        var product5 = new Product { Name = "Portafiltros acero", Sku = "AC-030", UnitPrice = 28.5m, Stock = 40, SupplierId = 1, PrecioLista1 = 28.5m, PrecioLista2 = 27.1m, PrecioLista3 = 25.7m, PrecioLista4 = 24.2m };
        AddProduct(tenant, companyId, product1);
        AddProduct(tenant, companyId, product2);
        AddProduct(tenant, companyId, product3);
        AddProduct(tenant, companyId, product4);
        AddProduct(tenant, companyId, product5);

        var sale = new Sale
        {
            Date = DateTime.Today.AddDays(-3),
            CustomerId = 1,
            VendorId = 1,
            Lines =
            {
                new SaleLine { ProductId = 1, Quantity = 5, UnitPrice = 18.5m },
                new SaleLine { ProductId = 2, Quantity = 10, UnitPrice = 12m }
            }
        };

        AddSale(tenant, companyId, sale);

        // Venta de ejemplo 1: NPV-001
        var sale1 = new Sale
        {
            Date = DateTime.Today.AddDays(-5),
            CustomerId = 1,
            VendorId = 1,
            TipoComprobante = "NPV",
            NumeroComprobante = "001",
            SucursalId = 1,
            Lines =
            {
                new SaleLine { ProductId = 1, Quantity = 3, UnitPrice = 18.5m }, // Café en grano 1kg
                new SaleLine { ProductId = 2, Quantity = 5, UnitPrice = 12.0m },  // Taza térmica
                new SaleLine { ProductId = 3, Quantity = 2, UnitPrice = 45.0m }   // Molinillo de café
            }
        };
        AddSale(tenant, companyId, sale1);

        // Venta de ejemplo 2: Factura-001
        var sale2 = new Sale
        {
            Date = DateTime.Today.AddDays(-2),
            CustomerId = 2,
            VendorId = 2,
            TipoComprobante = "Factura",
            NumeroComprobante = "001",
            SucursalId = 2,
            Vencimiento = DateTime.Today.AddDays(30),
            Lines =
            {
                new SaleLine { ProductId = 4, Quantity = 10, UnitPrice = 15.0m }, // Café espresso 250g
                new SaleLine { ProductId = 5, Quantity = 4, UnitPrice = 28.5m },  // Portafiltros acero
                new SaleLine { ProductId = 2, Quantity = 8, UnitPrice = 12.0m }   // Taza térmica
            }
        };
        AddSale(tenant, companyId, sale2);
    }

    private void SeedCompanyTwo(int companyId)
    {
        var tenant = _tenants[companyId];

        var supplier1 = new Supplier { Name = "Distribuciones Norte", ContactName = "Carlos López", Email = "ventas@dnorte.com", Phone = "555-2000" };
        var supplier2 = new Supplier { Name = "Logística Este", ContactName = "Paula Medina", Email = "soporte@logeste.com", Phone = "555-4500" };
        AddSupplier(tenant, companyId, supplier1);
        AddSupplier(tenant, companyId, supplier2);

        var vendor1 = new Vendor { Name = "Lucía Santos", CommissionRate = 5 };
        var vendor2 = new Vendor { Name = "Tomás Vega", CommissionRate = 4 };
        AddVendor(tenant, companyId, vendor1);
        AddVendor(tenant, companyId, vendor2);

        var customer1 = new Customer { Name = "Retail Norte", Email = "retail@norte.com", Phone = "555-7788" };
        var customer2 = new Customer { Name = "Tiendas Express", Email = "contacto@express.com", Phone = "555-6633" };
        AddCustomer(tenant, companyId, customer1);
        AddCustomer(tenant, companyId, customer2);

        var product1 = new Product { Name = "Kit barista", Sku = "KT-100", UnitPrice = 85m, Stock = 20, SupplierId = 1, PrecioLista1 = 85m, PrecioLista2 = 80.8m, PrecioLista3 = 76.5m, PrecioLista4 = 72.3m };
        var product2 = new Product { Name = "Café molido 500g", Sku = "CF-200", UnitPrice = 9.5m, Stock = 90, SupplierId = 2, PrecioLista1 = 9.5m, PrecioLista2 = 9.0m, PrecioLista3 = 8.6m, PrecioLista4 = 8.1m };
        var product3 = new Product { Name = "Cafetera italiana 6 tazas", Sku = "CA-300", UnitPrice = 32.0m, Stock = 35, SupplierId = 1, PrecioLista1 = 32.0m, PrecioLista2 = 30.4m, PrecioLista3 = 28.8m, PrecioLista4 = 27.2m };
        var product4 = new Product { Name = "Café descafeinado 250g", Sku = "CF-201", UnitPrice = 11.0m, Stock = 60, SupplierId = 2, PrecioLista1 = 11.0m, PrecioLista2 = 10.5m, PrecioLista3 = 9.9m, PrecioLista4 = 9.4m };
        var product5 = new Product { Name = "Vaso desechable 16oz", Sku = "AC-400", UnitPrice = 0.85m, Stock = 500, SupplierId = 1, PrecioLista1 = 0.85m, PrecioLista2 = 0.81m, PrecioLista3 = 0.77m, PrecioLista4 = 0.72m };
        AddProduct(tenant, companyId, product1);
        AddProduct(tenant, companyId, product2);
        AddProduct(tenant, companyId, product3);
        AddProduct(tenant, companyId, product4);
        AddProduct(tenant, companyId, product5);

        var sale = new Sale
        {
            Date = DateTime.Today.AddDays(-1),
            CustomerId = 2,
            VendorId = 1,
            Lines =
            {
                new SaleLine { ProductId = 2, Quantity = 15, UnitPrice = 9.5m }
            }
        };

        AddSale(tenant, companyId, sale);
    }

    private void AddSupplier(TenantData tenant, int companyId, Supplier supplier)
    {
        supplier.Id = tenant.SupplierSeq++;
        supplier.CompanyId = companyId;
        tenant.Suppliers.Add(CloneSupplier(supplier));
    }

    private void AddVendor(TenantData tenant, int companyId, Vendor vendor)
    {
        vendor.Id = tenant.VendorSeq++;
        vendor.CompanyId = companyId;
        tenant.Vendors.Add(CloneVendor(vendor));
    }

    private void AddCustomer(TenantData tenant, int companyId, Customer customer)
    {
        customer.Id = tenant.CustomerSeq++;
        customer.CompanyId = companyId;
        tenant.Customers.Add(CloneCustomer(customer));
    }

    private void AddProduct(TenantData tenant, int companyId, Product product)
    {
        product.Id = tenant.ProductSeq++;
        product.CompanyId = companyId;
        tenant.Products.Add(CopyProduct(product));
    }

    private void AddSale(TenantData tenant, int companyId, Sale sale)
    {
        sale.Id = tenant.SaleSeq++;
        sale.CompanyId = companyId;
        tenant.Sales.Add(CopySale(sale));
    }

    private class TenantData
    {
        public List<Supplier> Suppliers { get; } = new();
        public List<Vendor> Vendors { get; } = new();
        public List<Customer> Customers { get; } = new();
        public List<Product> Products { get; } = new();
        public List<Sale> Sales { get; } = new();

        public int SupplierSeq { get; set; } = 1;
        public int VendorSeq { get; set; } = 1;
        public int CustomerSeq { get; set; } = 1;
        public int ProductSeq { get; set; } = 1;
        public int SaleSeq { get; set; } = 1;
    }
}

