using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModififyTableCredenziali : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IV",
                table: "credenziali",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "credenziali",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IV",
                table: "credenziali");

            migrationBuilder.DropColumn(
                name: "key",
                table: "credenziali");
        }
    }
}
