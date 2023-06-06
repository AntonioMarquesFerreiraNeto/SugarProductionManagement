using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class safra2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataSafra",
                table: "Safra",
                newName: "DataFechamentoSafra");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAberturaSafra",
                table: "Safra",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "YearSafra",
                table: "Safra",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAberturaSafra",
                table: "Safra");

            migrationBuilder.DropColumn(
                name: "YearSafra",
                table: "Safra");

            migrationBuilder.RenameColumn(
                name: "DataFechamentoSafra",
                table: "Safra",
                newName: "DataSafra");
        }
    }
}
