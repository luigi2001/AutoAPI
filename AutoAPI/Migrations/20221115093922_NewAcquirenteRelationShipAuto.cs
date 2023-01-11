using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewAcquirenteRelationShipAuto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "acquirenteID",
                table: "automobili",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Acquirente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acquirente", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_automobili_acquirenteID",
                table: "automobili",
                column: "acquirenteID");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_Acquirente_acquirenteID",
                table: "automobili",
                column: "acquirenteID",
                principalTable: "Acquirente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_Acquirente_acquirenteID",
                table: "automobili");

            migrationBuilder.DropTable(
                name: "Acquirente");

            migrationBuilder.DropIndex(
                name: "IX_automobili_acquirenteID",
                table: "automobili");

            migrationBuilder.DropColumn(
                name: "acquirenteID",
                table: "automobili");
        }
    }
}
