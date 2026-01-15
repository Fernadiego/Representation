using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class AddPercepcionesAndDescuentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Company_CompanyId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Company_CompanyId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Localidades_Provincias_ProvinciaId",
                table: "Localidades");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaLineas_Articulos_ArticuloId",
                table: "VentaLineas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Company_CompanyId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Vendedores_VendedorId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CompanyId_CodigoCliente_CodigoSucursal",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Comision",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MensajeSobreArticulo",
                table: "Articulos");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Zonas",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "TipoClientes",
                newName: "Descripcion");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Zonas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Zonas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Zonas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "Ventas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ComprobanteId",
                table: "Ventas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Vendedores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Vendedores",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ComisionPorcentaje",
                table: "Vendedores",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Vendedores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoEnvases",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "TipoClientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "TipoClientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CondicionIva",
                table: "TipoClientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "TipoClientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "TipoClientes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "RequiereCuit",
                table: "TipoClientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Provincias",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Provincias",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoAfip",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Provincias",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Provincias",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Paises",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Marcas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinciaId",
                table: "Localidades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Localidades",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Localidades",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Localidades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "Localidades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Localidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Localidades",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Localidades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinciaNombre",
                table: "Localidades",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Comprobantes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Comprobantes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Comprobantes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Numeracion",
                table: "Comprobantes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Companies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CUIT",
                table: "Companies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Companies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Companies",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "Companies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Companies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Cobradores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Cobradores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Cobradores",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Cobradores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AMRO_Descuentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Descuentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Tipo_Percepcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Tipo_Percepcion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bitacoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bitacoras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoComprobante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Letra = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CodigoAfip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RequiereCuit = table.Column<bool>(type: "bit", nullable: false),
                    RequiereStock = table.Column<bool>(type: "bit", nullable: false),
                    Afectacc = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoComprobante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Descuentos_Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescuentoABMId = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Descuentos_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Descuentos_Clientes_AMRO_Descuentos_DescuentoABMId",
                        column: x => x.DescuentoABMId,
                        principalTable: "AMRO_Descuentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AMRO_Descuentos_Clientes_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Percepcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoAfip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Iva = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CodPercepcion = table.Column<int>(type: "int", nullable: true),
                    PercepMinima = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PorcentPercepcion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NombrePercepcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TipoPercepcionId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Percepcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Percepcion_AMRO_Tipo_Percepcion_TipoPercepcionId",
                        column: x => x.TipoPercepcionId,
                        principalTable: "AMRO_Tipo_Percepcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Percepcion_Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PercepcionId = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Percepcion_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Percepcion_Cliente_AMRO_Percepcion_PercepcionId",
                        column: x => x.PercepcionId,
                        principalTable: "AMRO_Percepcion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AMRO_Percepcion_Cliente_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CompanyId",
                table: "Clientes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Descuentos_Clientes_DescuentoABMId",
                table: "AMRO_Descuentos_Clientes",
                column: "DescuentoABMId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Descuentos_Clientes_IdCliente",
                table: "AMRO_Descuentos_Clientes",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Percepcion_TipoPercepcionId",
                table: "AMRO_Percepcion",
                column: "TipoPercepcionId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Percepcion_Cliente_IdCliente",
                table: "AMRO_Percepcion_Cliente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Percepcion_Cliente_PercepcionId",
                table: "AMRO_Percepcion_Cliente",
                column: "PercepcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bitacoras_Fecha",
                table: "Bitacoras",
                column: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_Bitacoras_UsuarioId",
                table: "Bitacoras",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Companies_CompanyId",
                table: "Articulos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Companies_CompanyId",
                table: "Clientes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localidades_Provincias_ProvinciaId",
                table: "Localidades",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaLineas_Articulos_ArticuloId",
                table: "VentaLineas",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Companies_CompanyId",
                table: "Ventas",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas",
                column: "ComprobanteId",
                principalTable: "Comprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Vendedores_VendedorId",
                table: "Ventas",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Companies_CompanyId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Companies_CompanyId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Localidades_Provincias_ProvinciaId",
                table: "Localidades");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaLineas_Articulos_ArticuloId",
                table: "VentaLineas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Companies_CompanyId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Vendedores_VendedorId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "AMRO_Descuentos_Clientes");

            migrationBuilder.DropTable(
                name: "AMRO_Percepcion_Cliente");

            migrationBuilder.DropTable(
                name: "Bitacoras");

            migrationBuilder.DropTable(
                name: "TipoComprobante");

            migrationBuilder.DropTable(
                name: "AMRO_Descuentos");

            migrationBuilder.DropTable(
                name: "AMRO_Percepcion");

            migrationBuilder.DropTable(
                name: "AMRO_Tipo_Percepcion");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CompanyId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Zonas");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Zonas");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Zonas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "ComisionPorcentaje",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "CondicionIva",
                table: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "RequiereCuit",
                table: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "CodigoAfip",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "ProvinciaNombre",
                table: "Localidades");

            migrationBuilder.DropColumn(
                name: "Numeracion",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CUIT",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RazonSocial",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Cobradores");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Cobradores");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Cobradores");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Zonas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "TipoClientes",
                newName: "Nombre");

            migrationBuilder.AlterColumn<int>(
                name: "VendedorId",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComprobanteId",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Vendedores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<decimal>(
                name: "Comision",
                table: "Vendedores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoEnvases",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Provincias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Paises",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Marcas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinciaId",
                table: "Localidades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Localidades",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Comprobantes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Comprobantes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Comprobantes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Cobradores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "Clientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MensajeSobreArticulo",
                table: "Articulos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CompanyId_CodigoCliente_CodigoSucursal",
                table: "Clientes",
                columns: new[] { "CompanyId", "CodigoCliente", "CodigoSucursal" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Company_CompanyId",
                table: "Articulos",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Company_CompanyId",
                table: "Clientes",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Localidades_Provincias_ProvinciaId",
                table: "Localidades",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaLineas_Articulos_ArticuloId",
                table: "VentaLineas",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Company_CompanyId",
                table: "Ventas",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas",
                column: "ComprobanteId",
                principalTable: "Comprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Vendedores_VendedorId",
                table: "Ventas",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
