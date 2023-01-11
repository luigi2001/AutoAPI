using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambioChiavePrimariaTabellaAuto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "automobili",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "automobili",
                newName: "id");
        }
    }
}
