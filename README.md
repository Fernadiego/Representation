# BlazorVentas - Sistema de Ventas Multiempresa

Sistema de gestión de ventas desarrollado en Blazor Server para representantes legales que manejan múltiples empresas. Cada empresa tiene sus propios clientes, artículos y ventas independientes.

## 🚀 Características

- **Multi-tenant**: Soporte completo para múltiples empresas con datos independientes
- **Gestión de Clientes**: ABM completo con todos los campos requeridos
- **Gestión de Artículos**: Catálogo de productos con 4 listas de precios
- **Gestión de Ventas**: Sistema maestro-detalle con impuestos provinciales y nacionales
- **Precios Especiales**: Precios y promociones personalizadas por cliente
- **ABM Completo**: Administración de localidades, provincias, marcas, zonas, etc.
- **Base de Datos**: Entity Framework Core con SQL Server
- **Interfaz Moderna**: UI responsive con Bootstrap

## 🛠️ Tecnologías

- **.NET 8.0**: Framework principal
- **Blazor Server**: Tecnología web
- **Entity Framework Core 8.0**: ORM para acceso a datos
- **SQL Server**: Base de datos
- **Bootstrap 5**: Framework CSS

## 📋 Requisitos Previos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express o Full)
- Visual Studio 2022 o Visual Studio Code

## 🔧 Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/Fernadiego/Representation.git
cd Representation/BlazorVentas
```

### 2. Configurar la base de datos

Edita el archivo `appsettings.json` con tu cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlazorVentas;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### 3. Aplicar migraciones

```bash
dotnet ef database update
```

### 4. Ejecutar la aplicación

```bash
dotnet run
```

La aplicación estará disponible en `https://localhost:5001` o `http://localhost:5000`

## 📁 Estructura del Proyecto

```
BlazorVentas/
├── Data/
│   ├── Models/              # Modelos de entidades
│   │   ├── ABM/            # Tablas de administración
│   │   ├── Cliente.cs
│   │   ├── Articulo.cs
│   │   ├── Venta.cs
│   │   └── ...
│   ├── CommerceDbContext.cs
│   ├── CommerceService.cs
│   └── TenantState.cs
├── Pages/                   # Páginas Blazor
│   ├── Index.razor         # Dashboard
│   ├── Clientes.razor
│   ├── Productos.razor
│   ├── Ventas.razor
│   └── ...
├── Shared/                  # Componentes compartidos
│   ├── MainLayout.razor
│   └── NavMenu.razor
├── Migrations/              # Migraciones de EF Core
└── wwwroot/                # Archivos estáticos
```

## 🗄️ Base de Datos

### Entidades Principales

- **Company**: Empresas (multi-tenant)
- **Cliente**: Clientes con todos los campos requeridos
- **Articulo**: Productos con 4 listas de precios
- **Venta**: Ventas con impuestos
- **VentaLinea**: Detalle de ventas

### Tablas de ABM

- Provincia, Localidad
- Descuento, TipoCliente, Zona, ClaseCliente
- Marca, Pais, TipoEnvase
- Cobrador, Comprobante
- Vendedor (compartido entre empresas)

Para más detalles sobre la estructura de la base de datos, consulta [README_DATABASE.md](README_DATABASE.md)

## 🎯 Funcionalidades

### Panel Principal
- Dashboard con métricas clave
- Valor de inventario
- Ventas del mes
- Promedio de ticket
- Últimas ventas registradas

### Gestión de Clientes
- ABM completo de clientes
- Todos los campos según especificación
- Precios especiales por cliente
- Filtrado por empresa

### Gestión de Artículos
- Catálogo de productos
- 4 listas de precios
- Gestión de stock
- Relación con marcas y proveedores

### Gestión de Ventas
- Registro de ventas maestro-detalle
- Asignación de vendedores
- Aplicación de impuestos (provincial y nacional)
- Cálculo automático de totales
- Historial de ventas

### Selector Multiempresa
- Búsqueda y autocompletado de empresas
- Cambio dinámico de tenant
- Filtrado automático de datos por empresa

## 🔐 Configuración Multiempresa

El sistema utiliza un selector de empresa en la barra superior que permite cambiar entre diferentes empresas. Cada empresa tiene:

- Clientes independientes
- Artículos independientes
- Ventas independientes
- Configuraciones propias

Los vendedores son compartidos entre todas las empresas.

## 📝 Migraciones

### Crear una nueva migración

```bash
dotnet ef migrations add NombreMigracion
```

### Aplicar migraciones

```bash
dotnet ef database update
```

### Revertir última migración

```bash
dotnet ef migrations remove
```

## 🧪 Desarrollo

### Compilar el proyecto

```bash
dotnet build
```

### Ejecutar en modo desarrollo

```bash
dotnet run
```

### Restaurar paquetes NuGet

```bash
dotnet restore
```

## 📦 Paquetes NuGet Utilizados

- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.0)
- `Microsoft.EntityFrameworkCore.Design` (8.0.0)

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 👤 Autor

**Fernando Diego**

- GitHub: [@Fernadiego](https://github.com/Fernadiego)

## 🙏 Agradecimientos

- .NET Foundation
- Blazor Community
- Entity Framework Core Team

---

⭐ Si este proyecto te resulta útil, considera darle una estrella en GitHub!

