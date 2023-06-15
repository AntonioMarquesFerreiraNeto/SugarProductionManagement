using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtEntregue",
                table: "VendaSaidas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtEntregue",
                table: "VendaSaidas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
