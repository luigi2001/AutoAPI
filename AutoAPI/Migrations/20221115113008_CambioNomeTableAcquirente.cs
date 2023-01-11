using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambioNomeTableAcquirente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_Acquirente_acquirenteID",
                table: "automobili");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acquirente",
                table: "Acquirente");

            migrationBuilder.RenameTable(
                name: "Acquirente",
                newName: "acquirente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_acquirente",
                table: "acquirente",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_acquirente_acquirenteID",
                table: "automobili",
                column: "acquirenteID",
                principalTable: "acquirente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_acquirente_acquirenteID",
                table: "automobili");

            migrationBuilder.DropPrimaryKey(
                name: "PK_acquirente",
                table: "acquirente");

            migrationBuilder.RenameTable(
                name: "acquirente",
                newName: "Acquirente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acquirente",
                table: "Acquirente",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_Acquirente_acquirenteID",
                table: "automobili",
                column: "acquirenteID",
                principalTable: "Acquirente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
