using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class teste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invetario_Funcionario_FuncionarioId",
                table: "Invetario");

            migrationBuilder.DropForeignKey(
                name: "FK_Invetario_Produtos_ProdutoId",
                table: "Invetario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invetario",
                table: "Invetario");

            migrationBuilder.RenameTable(
                name: "Invetario",
                newName: "Inventario");

            migrationBuilder.RenameIndex(
                name: "IX_Invetario_ProdutoId",
                table: "Inventario",
                newName: "IX_Inventario_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Invetario_FuncionarioId",
                table: "Inventario",
                newName: "IX_Inventario_FuncionarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventario",
                table: "Inventario",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Funcionario_FuncionarioId",
                table: "Inventario",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Produtos_ProdutoId",
                table: "Inventario",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Funcionario_FuncionarioId",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Produtos_ProdutoId",
                table: "Inventario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventario",
                table: "Inventario");

            migrationBuilder.RenameTable(
                name: "Inventario",
                newName: "Invetario");

            migrationBuilder.RenameIndex(
                name: "IX_Inventario_ProdutoId",
                table: "Invetario",
                newName: "IX_Invetario_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventario_FuncionarioId",
                table: "Invetario",
                newName: "IX_Invetario_FuncionarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invetario",
                table: "Invetario",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invetario_Funcionario_FuncionarioId",
                table: "Invetario",
                column: "FuncionarioId",
                principalTable: "Funcionario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invetario_Produtos_ProdutoId",
                table: "Invetario",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
