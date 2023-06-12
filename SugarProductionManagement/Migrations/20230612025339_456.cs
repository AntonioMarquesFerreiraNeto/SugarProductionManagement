using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class _456 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Produtos_ProdutoId",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_ProdutoId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Inventario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Inventario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ProdutoId",
                table: "Inventario",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Produtos_ProdutoId",
                table: "Inventario",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
