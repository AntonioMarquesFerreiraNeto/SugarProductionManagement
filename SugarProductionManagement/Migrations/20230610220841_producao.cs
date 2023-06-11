using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class producao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtProdutos",
                table: "Producao",
                newName: "QtProduzida");

            migrationBuilder.AddColumn<int>(
                name: "QtEstoque",
                table: "Producao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Producao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtEstoque",
                table: "Producao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Producao");

            migrationBuilder.RenameColumn(
                name: "QtProduzida",
                table: "Producao",
                newName: "QtProdutos");
        }
    }
}
