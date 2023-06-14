using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class create2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProducaoId",
                table: "Saida",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Saida_ProducaoId",
                table: "Saida",
                column: "ProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saida_Producao_ProducaoId",
                table: "Saida",
                column: "ProducaoId",
                principalTable: "Producao",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saida_Producao_ProducaoId",
                table: "Saida");

            migrationBuilder.DropIndex(
                name: "IX_Saida_ProducaoId",
                table: "Saida");

            migrationBuilder.DropColumn(
                name: "ProducaoId",
                table: "Saida");
        }
    }
}
