using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescuentoClienteAndAddPorcentaje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AMRO_Descuentos_Clientes");

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeDescuento",
                table: "AMRO_Descuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeDescuento",
                table: "AMRO_Descuentos");

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

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Descuentos_Clientes_DescuentoABMId",
                table: "AMRO_Descuentos_Clientes",
                column: "DescuentoABMId");

            migrationBuilder.CreateIndex(
                name: "IX_AMRO_Descuentos_Clientes_IdCliente",
                table: "AMRO_Descuentos_Clientes",
                column: "IdCliente");
        }
    }
}
