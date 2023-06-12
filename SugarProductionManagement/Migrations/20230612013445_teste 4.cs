using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class teste4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ProducaoId",
                table: "Inventario",
                column: "ProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Producao_ProducaoId",
                table: "Inventario",
                column: "ProducaoId",
                principalTable: "Producao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Producao_ProducaoId",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_ProducaoId",
                table: "Inventario");
        }
    }
}
