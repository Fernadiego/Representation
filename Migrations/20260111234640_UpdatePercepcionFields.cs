using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePercepcionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iva",
                table: "AMRO_Percepcion");

            migrationBuilder.DropColumn(
                name: "NombrePercepcion",
                table: "AMRO_Percepcion");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "AMRO_Percepcion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IvaPercepcion",
                table: "AMRO_Percepcion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IvaPercepcion",
                table: "AMRO_Percepcion");

            migrationBuilder.AlterColumn<int>(
                name: "Codigo",
                table: "AMRO_Percepcion",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<decimal>(
                name: "Iva",
                table: "AMRO_Percepcion",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NombrePercepcion",
                table: "AMRO_Percepcion",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
