using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_App.Migrations
{
    /// <inheritdoc />
    public partial class modifyrangeimportecobrado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ImporteCobrado",
                table: "Ticket",
                type: "decimal(8,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)",
                oldPrecision: 4,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ImporteCobrado",
                table: "Ticket",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 4,
                oldScale: 2);
        }
    }
}
