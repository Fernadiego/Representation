# Base de Datos - Sistema de Ventas Multiempresa

## Descripción

Base de datos diseñada para un representante legal que maneja múltiples empresas. Cada empresa tiene sus propios clientes, artículos y ventas independientes.

## Estructura de la Base de Datos

### Entidades Principales

#### Company (Empresas)
- Representa cada empresa que maneja el representante legal
- Multi-tenant: cada empresa tiene datos independientes

#### Cliente
- Todos los campos especificados según requerimientos
- Relación con Company (multiempresa)
- Soporte para precios especiales por cliente
- Campos de ABM: Localidad, Provincia, Descuento, TipoCliente, Zona, ClaseCliente, Cobrador

#### Articulo
- Todos los campos especificados según requerimientos
- Relación con Company (multiempresa)
- Relación con Marca (ABM)
- Relación con Pais (Origen - ABM)
- Relación con TipoEnvase (ABM)
- 4 listas de precios (PrecioLista1, PrecioLista2, PrecioLista3, PrecioLista4)

#### Venta
- Relación con Company, Cliente, Vendedor, Comprobante
- Impuestos provinciales y nacionales
- Subtotal y Total calculados

#### VentaLinea
- Detalle de cada línea de venta
- Relación con Articulo
- Impuestos por línea
- Descuentos aplicables

### Tablas de ABM (Administración)

- **Provincia**: Provincias
- **Localidad**: Localidades (relacionadas con Provincia)
- **Descuento**: Descuentos disponibles
- **TipoCliente**: Tipos de cliente
- **Zona**: Zonas de venta
- **ClaseCliente**: Clases de cliente
- **Marca**: Marcas de artículos
- **Pais**: Países (para origen de artículos)
- **TipoEnvase**: Tipos de envase (Frasco, Lata, Caja, Bolsa)
- **Cobrador**: Cobradores
- **Comprobante**: Tipos de comprobantes (Factura, Nota de Crédito, etc.)
- **Vendedor**: Vendedores (compartidos entre todas las empresas)

### Relaciones Especiales

- **ClientePrecio**: Precios especiales o promociones por cliente y artículo
- Los **Vendedores** son compartidos entre todas las empresas (no tienen CompanyId)
- Cada **Venta** está asociada a un **Comprobante** (ABM)

## Configuración

### Cadena de Conexión

La cadena de conexión se configura en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlazorVentas;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### Migraciones

Para crear la base de datos:

```bash
dotnet ef database update
```

Para crear una nueva migración:

```bash
dotnet ef migrations add NombreMigracion
```

Para revertir la última migración:

```bash
dotnet ef migrations remove
```

## Campos de Cliente

Según especificación:
- CodigoCliente, CodigoSucursal, CodigoParaMostrar
- NombreCliente, NombreSucursal
- NumeroProveedor
- DomicilioEntrega, DomicilioLegal
- Localidad, Provincia, CP
- Telefono, Mail, Web, Contacto, CUIT
- ListaPrecio (1,2,3,4)
- CodigoDescuento (ABM)
- TipoCliente (ABM)
- CondicionPago (días)
- Zona (ABM)
- Vendedor (ABM - compartido)
- Cobrador (ABM)
- ClaseCliente (ABM)
- FechaUltimaCompra, FechaAlta
- Inhabilitado (S/N)
- MensajeSobreCliente

## Campos de Articulo

Según especificación:
- CodigoArticulo, CodigoParaMostrar
- Descripcion
- Marca (ABM)
- Origen (Pais - ABM)
- PesoNeto, PesoEscurrido (Gramos)
- TipoEnvase (ABM)
- UnidadXBulto
- EAUN13, DUN14
- PrecioLista1, PrecioLista2, PrecioLista3, PrecioLista4
- TamañoUnidad (Alto, Ancho, Profundo en cm)
- TamañoBulto (Alto, Ancho, Profundo en cm)
- TamañoPalet (Alto, Ancho, Profundo en cm)
- PesoBulto, PesoPalet
- BultosXCamada, BultosXPalet
- Inhabilitado (S/N)
- MensajeSobreArticulo

## Impuestos

Cada venta y línea de venta puede tener:
- ImpuestoProvincial
- ImpuestoNacional

Estos se calculan y almacenan tanto a nivel de línea como de venta completa.

## Precios Especiales

La tabla `ClientePrecio` permite:
- Definir precios especiales por cliente y artículo
- Promociones con fechas de inicio y fin
- Precios fijos independientes de las listas de precios

## Notas

- Todos los campos opcionales están marcados como nullable
- Los índices únicos están configurados para evitar duplicados (ej: CodigoCliente + CodigoSucursal por empresa)
- Las relaciones están configuradas con eliminación en cascada donde corresponde
- Los vendedores son compartidos entre empresas (no tienen CompanyId)

