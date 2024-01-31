using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_App.Migrations
{
    /// <inheritdoc />
    public partial class actualizarestancia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "Estancias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "Estancias");
        }
    }
}
