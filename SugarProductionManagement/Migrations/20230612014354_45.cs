using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class _45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProducaoId",
                table: "Producao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producao_ProducaoId",
                table: "Producao",
                column: "ProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producao_Producao_ProducaoId",
                table: "Producao",
                column: "ProducaoId",
                principalTable: "Producao",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producao_Producao_ProducaoId",
                table: "Producao");

            migrationBuilder.DropIndex(
                name: "IX_Producao_ProducaoId",
                table: "Producao");

            migrationBuilder.DropColumn(
                name: "ProducaoId",
                table: "Producao");
        }
    }
}
