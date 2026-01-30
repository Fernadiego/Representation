using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class AddCobranzasModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comprobantes",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "Numeracion",
                table: "Comprobantes");

            migrationBuilder.RenameTable(
                name: "Comprobantes",
                newName: "AMRO_Comprobantes");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "AMRO_Comprobantes",
                newName: "SignoCC");

            migrationBuilder.AddColumn<string>(
                name: "Mostrar",
                table: "AMRO_Percepcion",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "AMRO_Comprobantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Afectacc",
                table: "AMRO_Comprobantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CodigoAfip",
                table: "AMRO_Comprobantes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "AMRO_Comprobantes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "AMRO_Comprobantes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Letra",
                table: "AMRO_Comprobantes",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereCuit",
                table: "AMRO_Comprobantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereStock",
                table: "AMRO_Comprobantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AMRO_Comprobantes",
                table: "AMRO_Comprobantes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AMRO_Cobranzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: true),
                    CobradorId = table.Column<int>(type: "int", nullable: true),
                    CodigoComprobante = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumeroComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NombreSucursal = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NombreCobrador = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalEfectivo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalCheques = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTransferencia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalRetencion = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalOtros = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Cobranzas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Cobranzas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AMRO_Cobranzas_Cobradores_CobradorId",
                        column: x => x.CobradorId,
                        principalTable: "Cobradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Movimientos_CC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CodigoComprobante = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NumeroComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VentaId = table.Column<int>(type: "int", nullable: true),
                    CobranzaId = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Debe = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Haber = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Movimientos_CC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Movimientos_CC_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Num_Comprobantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComprobanteId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Num_Comprobantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Num_Comprobantes_AMRO_Comprobantes_ComprobanteId",
                        column: x => x.ComprobanteId,
                        principalTable: "AMRO_Comprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ComprobanteId = table.Column<int>(type: "int", nullable: true),
                    CodigoComprobante = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumeroComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    CodigoCliente = table.Column<int>(type: "int", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: true),
                    NombreSucursal = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendedorId = table.Column<int>(type: "int", nullable: true),
                    NombreVendedor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalDescuentos = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DescuentoAdicionalPorcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoAdicionalMonto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIVA = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPercepciones = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Ventas_AMRO_Comprobantes_ComprobanteId",
                        column: x => x.ComprobanteId,
                        principalTable: "AMRO_Comprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PorcentajeDescuento = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MontoFijo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ofertas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Cobranzas_Detalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CobranzaId = table.Column<int>(type: "int", nullable: false),
                    TipoPago = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BancoCheque = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumeroCheque = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaCheque = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumeroTransferencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BancoOrigen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TipoRetencion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumeroRetencion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Cobranzas_Detalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Cobranzas_Detalle_AMRO_Cobranzas_CobranzaId",
                        column: x => x.CobranzaId,
                        principalTable: "AMRO_Cobranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Cobranzas_Comprobantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CobranzaId = table.Column<int>(type: "int", nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    CodigoComprobante = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumeroComprobante = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaComprobante = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalComprobante = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MontoAplicado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Cobranzas_Comprobantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Cobranzas_Comprobantes_AMRO_Cobranzas_CobranzaId",
                        column: x => x.CobranzaId,
                        principalTable: "AMRO_Cobranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AMRO_Cobranzas_Comprobantes_AMRO_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "AMRO_Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Ventas_Detalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    CodigoArticulo = table.Column<int>(type: "int", nullable: false),
                    DescripcionArticulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DescripcionAdicional = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ListaPrecio = table.Column<int>(type: "int", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DescuentoMonto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OfertaNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EsOfertaLibre = table.Column<bool>(type: "bit", nullable: false),
                    IVAPorcentaje = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IVAMonto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SubtotalConIVA = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NumeroLinea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Ventas_Detalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Ventas_Detalle_AMRO_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "AMRO_Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AMRO_Ventas_Percepciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    PercepcionId = table.Column<int>(type: "int", nullable: false),
                    NombrePercepcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoAfip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Porcentaje = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BaseImponible = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AMRO_Ventas_Percepciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AMRO_Ventas_Percepciones_AMRO_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "AMRO_Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Comprobantes_Codigo",
                table: "AMRO_Comprobantes",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_ClienteId",
                table: "AMRO_Cobranzas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_CobradorId",
                table: "AMRO_Cobranzas",
                column: "CobradorId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_CompanyId_ClienteId_Fecha",
                table: "AMRO_Cobranzas",
                columns: new[] { "CompanyId", "ClienteId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_CompanyId_NumeroComprobante",
                table: "AMRO_Cobranzas",
                columns: new[] { "CompanyId", "NumeroComprobante" });

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_Comprobantes_CobranzaId",
                table: "AMRO_Cobranzas_Comprobantes",
                column: "CobranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_Comprobantes_VentaId",
                table: "AMRO_Cobranzas_Comprobantes",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Cobranzas_Detalle_CobranzaId",
                table: "AMRO_Cobranzas_Detalle",
                column: "CobranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Movimientos_CC_ClienteId",
                table: "AMRO_Movimientos_CC",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Movimientos_CC_CompanyId_ClienteId_Fecha",
                table: "AMRO_Movimientos_CC",
                columns: new[] { "CompanyId", "ClienteId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Num_Comprobantes_CompanyId_ComprobanteId",
                table: "AMRO_Num_Comprobantes",
                columns: new[] { "CompanyId", "ComprobanteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Num_Comprobantes_ComprobanteId",
                table: "AMRO_Num_Comprobantes",
                column: "ComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Ventas_CompanyId_CodigoComprobante_NumeroComprobante",
                table: "AMRO_Ventas",
                columns: new[] { "CompanyId", "CodigoComprobante", "NumeroComprobante" });

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Ventas_ComprobanteId",
                table: "AMRO_Ventas",
                column: "ComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Ventas_Detalle_VentaId",
                table: "AMRO_Ventas_Detalle",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Ventas_Percepciones_VentaId",
                table: "AMRO_Ventas_Percepciones",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_ArticuloId",
                table: "Ofertas",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_ClienteId",
                table: "Ofertas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_CompanyId",
                table: "Ofertas",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_AMRO_Comprobantes_ComprobanteId",
                table: "Ventas",
                column: "ComprobanteId",
                principalTable: "AMRO_Comprobantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_AMRO_Comprobantes_ComprobanteId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "AMRO_Cobranzas_Comprobantes");

            migrationBuilder.DropTable(
                name: "AMRO_Cobranzas_Detalle");

            migrationBuilder.DropTable(
                name: "AMRO_Movimientos_CC");

            migrationBuilder.DropTable(
                name: "AMRO_Num_Comprobantes");

            migrationBuilder.DropTable(
                name: "AMRO_Ventas_Detalle");

            migrationBuilder.DropTable(
                name: "AMRO_Ventas_Percepciones");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "AMRO_Cobranzas");

            migrationBuilder.DropTable(
                name: "AMRO_Ventas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AMRO_Comprobantes",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_AMRO_Comprobantes_Codigo",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "Mostrar",
                table: "AMRO_Percepcion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "Afectacc",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "CodigoAfip",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "Letra",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "RequiereCuit",
                table: "AMRO_Comprobantes");

            migrationBuilder.DropColumn(
                name: "RequiereStock",
                table: "AMRO_Comprobantes");

            migrationBuilder.RenameTable(
                name: "AMRO_Comprobantes",
                newName: "Comprobantes");

            migrationBuilder.RenameColumn(
                name: "SignoCC",
                table: "Comprobantes",
                newName: "Tipo");

            migrationBuilder.AddColumn<string>(
                name: "Numeracion",
                table: "Comprobantes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comprobantes",
                table: "Comprobantes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas",
                column: "ComprobanteId",
                principalTable: "Comprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
