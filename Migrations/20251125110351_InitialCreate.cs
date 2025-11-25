using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaseClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseClientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cobradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comprobantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comprobantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Descuentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoClientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEnvases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEnvases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Comision = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidades_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CodigoArticulo = table.Column<int>(type: "int", nullable: false),
                    CodigoParaMostrar = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: true),
                    OrigenId = table.Column<int>(type: "int", nullable: true),
                    PesoNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PesoEscurrido = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TipoEnvaseId = table.Column<int>(type: "int", nullable: true),
                    UnidadXBulto = table.Column<int>(type: "int", nullable: true),
                    EAUN13 = table.Column<long>(type: "bigint", nullable: true),
                    DUN14 = table.Column<long>(type: "bigint", nullable: true),
                    PrecioLista1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioLista2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioLista3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioLista4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TamañoUnidadAlto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoUnidadAncho = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoUnidadProfundo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoBultoAlto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoBultoAncho = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoBultoProfundo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoPaletAlto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoPaletAncho = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TamañoPaletProfundo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PesoBulto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PesoPalet = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BultosXCamada = table.Column<int>(type: "int", nullable: true),
                    BultosXPalet = table.Column<int>(type: "int", nullable: true),
                    Inhabilitado = table.Column<bool>(type: "bit", nullable: false),
                    MensajeSobreArticulo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articulos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Articulos_Paises_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Articulos_TipoEnvases_TipoEnvaseId",
                        column: x => x.TipoEnvaseId,
                        principalTable: "TipoEnvases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CodigoCliente = table.Column<int>(type: "int", nullable: false),
                    CodigoSucursal = table.Column<int>(type: "int", nullable: false),
                    CodigoParaMostrar = table.Column<int>(type: "int", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NombreSucursal = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumeroProveedor = table.Column<int>(type: "int", nullable: true),
                    DomicilioEntrega = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DomicilioLegal = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LocalidadId = table.Column<int>(type: "int", nullable: true),
                    ProvinciaId = table.Column<int>(type: "int", nullable: true),
                    CP = table.Column<int>(type: "int", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Web = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CUIT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ListaPrecio = table.Column<int>(type: "int", nullable: false),
                    CodigoDescuentoId = table.Column<int>(type: "int", nullable: true),
                    TipoClienteId = table.Column<int>(type: "int", nullable: true),
                    CondicionPago = table.Column<int>(type: "int", nullable: false),
                    ZonaId = table.Column<int>(type: "int", nullable: true),
                    VendedorId = table.Column<int>(type: "int", nullable: true),
                    CobradorId = table.Column<int>(type: "int", nullable: true),
                    ClaseClienteId = table.Column<int>(type: "int", nullable: true),
                    FechaUltimaCompra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Inhabilitado = table.Column<bool>(type: "bit", nullable: false),
                    MensajeSobreCliente = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_ClaseClientes_ClaseClienteId",
                        column: x => x.ClaseClienteId,
                        principalTable: "ClaseClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_Cobradores_CobradorId",
                        column: x => x.CobradorId,
                        principalTable: "Cobradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Descuentos_CodigoDescuentoId",
                        column: x => x.CodigoDescuentoId,
                        principalTable: "Descuentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_Localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_TipoClientes_TipoClienteId",
                        column: x => x.TipoClienteId,
                        principalTable: "TipoClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clientes_Zonas_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClientePrecios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaHasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EsPromocion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientePrecios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientePrecios_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientePrecios_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    ComprobanteId = table.Column<int>(type: "int", nullable: false),
                    NumeroComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImpuestoProvincial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpuestoNacional = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Comprobantes_ComprobanteId",
                        column: x => x.ComprobanteId,
                        principalTable: "Comprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaLineas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpuestoProvincial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpuestoNacional = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaLineas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaLineas_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentaLineas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_CompanyId_CodigoArticulo",
                table: "Articulos",
                columns: new[] { "CompanyId", "CodigoArticulo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_MarcaId",
                table: "Articulos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_OrigenId",
                table: "Articulos",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_TipoEnvaseId",
                table: "Articulos",
                column: "TipoEnvaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientePrecios_ArticuloId",
                table: "ClientePrecios",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientePrecios_ClienteId",
                table: "ClientePrecios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ClaseClienteId",
                table: "Clientes",
                column: "ClaseClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CobradorId",
                table: "Clientes",
                column: "CobradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CodigoDescuentoId",
                table: "Clientes",
                column: "CodigoDescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CompanyId_CodigoCliente_CodigoSucursal",
                table: "Clientes",
                columns: new[] { "CompanyId", "CodigoCliente", "CodigoSucursal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_LocalidadId",
                table: "Clientes",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ProvinciaId",
                table: "Clientes",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoClienteId",
                table: "Clientes",
                column: "TipoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_VendedorId",
                table: "Clientes",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ZonaId",
                table: "Clientes",
                column: "ZonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_ProvinciaId",
                table: "Localidades",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaLineas_ArticuloId",
                table: "VentaLineas",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaLineas_VentaId",
                table: "VentaLineas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_CompanyId",
                table: "Ventas",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ComprobanteId",
                table: "Ventas",
                column: "ComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_VendedorId",
                table: "Ventas",
                column: "VendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientePrecios");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "VentaLineas");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "TipoEnvases");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Comprobantes");

            migrationBuilder.DropTable(
                name: "ClaseClientes");

            migrationBuilder.DropTable(
                name: "Cobradores");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Descuentos");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "TipoClientes");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
