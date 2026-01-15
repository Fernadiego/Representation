using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVentas.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCodPercepcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodPercepcion",
                table: "AMRO_Percepcion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodPercepcion",
                table: "AMRO_Percepcion",
                type: "int",
                nullable: true);
        }
    }
}
