using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_App.Migrations
{
    /// <inheritdoc />
    public partial class actualizarmedidatiempo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HorasCobradas",
                table: "Ticket",
                newName: "MinutosCobrados");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinutosCobrados",
                table: "Ticket",
                newName: "HorasCobradas");
        }
    }
}
