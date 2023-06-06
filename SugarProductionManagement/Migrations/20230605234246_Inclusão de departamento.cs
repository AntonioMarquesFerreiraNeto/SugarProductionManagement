using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class Inclusãodedepartamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Departamento",
                table: "Funcionario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Funcionario");
        }
    }
}
