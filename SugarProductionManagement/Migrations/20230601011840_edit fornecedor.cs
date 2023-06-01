using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class editfornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "Rg",
                table: "Fornecedor",
                newName: "RazaoSocial");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Fornecedor",
                newName: "NomeFantasia");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Fornecedor",
                newName: "InscricaoMunicipal");

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Fornecedor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Fornecedor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "RazaoSocial",
                table: "Fornecedor",
                newName: "Rg");

            migrationBuilder.RenameColumn(
                name: "NomeFantasia",
                table: "Fornecedor",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "InscricaoMunicipal",
                table: "Fornecedor",
                newName: "Cpf");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Fornecedor",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
