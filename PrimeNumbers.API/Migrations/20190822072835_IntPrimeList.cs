using Microsoft.EntityFrameworkCore.Migrations;

namespace PrimeNumbers.API.Migrations
{
    public partial class IntPrimeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultValues",
                table: "Results");

            migrationBuilder.CreateTable(
                name: "PrimeNumber",
                columns: table => new
                {
                    Number = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimeNumber", x => x.Number);
                    table.ForeignKey(
                        name: "FK_PrimeNumber_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimeNumber_ResultId",
                table: "PrimeNumber",
                column: "ResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrimeNumber");

            migrationBuilder.AddColumn<string>(
                name: "ResultValues",
                table: "Results",
                nullable: true);
        }
    }
}
