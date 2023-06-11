using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class producao4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SafraId",
                table: "Producao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Producao_SafraId",
                table: "Producao",
                column: "SafraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producao_Safra_SafraId",
                table: "Producao",
                column: "SafraId",
                principalTable: "Safra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producao_Safra_SafraId",
                table: "Producao");

            migrationBuilder.DropIndex(
                name: "IX_Producao_SafraId",
                table: "Producao");

            migrationBuilder.DropColumn(
                name: "SafraId",
                table: "Producao");
        }
    }
}
