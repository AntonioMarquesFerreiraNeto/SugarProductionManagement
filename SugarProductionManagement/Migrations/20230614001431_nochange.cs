using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class nochange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodPedidoVenda",
                table: "Saida");

            migrationBuilder.AddColumn<string>(
                name: "CodPedidoVenda",
                table: "Venda",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CodCarregamento",
                table: "Saida",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SaidaStatus",
                table: "Saida",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodPedidoVenda",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "SaidaStatus",
                table: "Saida");

            migrationBuilder.AlterColumn<int>(
                name: "CodCarregamento",
                table: "Saida",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CodPedidoVenda",
                table: "Saida",
                type: "int",
                nullable: true);
        }
    }
}
