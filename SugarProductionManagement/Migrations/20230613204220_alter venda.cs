using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class altervenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtEntregue",
                table: "Venda",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QtReservada",
                table: "Produtos",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtEntregue",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "QtReservada",
                table: "Produtos");
        }
    }
}
