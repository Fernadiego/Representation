using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class AddDescuentoABMToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescuentoABMId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "AMRO_Descuentos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_DescuentoABMId",
                table: "Clientes",
                column: "DescuentoABMId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_AMRO_Descuentos_DescuentoABMId",
                table: "Clientes",
                column: "DescuentoABMId",
                principalTable: "AMRO_Descuentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_AMRO_Descuentos_DescuentoABMId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_DescuentoABMId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DescuentoABMId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "AMRO_Descuentos");
        }
    }
}
