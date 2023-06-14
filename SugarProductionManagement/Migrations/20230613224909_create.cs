using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SugarProductionManagement.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodCarregamento = table.Column<int>(type: "int", nullable: true),
                    CodPedidoVenda = table.Column<int>(type: "int", nullable: true),
                    QtSaidaTotal = table.Column<int>(type: "int", nullable: true),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true),
                    VendaId = table.Column<int>(type: "int", nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Saida_Fornecedor_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saida_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saida_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saida_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VendaSaidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProducaoId = table.Column<int>(type: "int", nullable: true),
                    VendaId = table.Column<int>(type: "int", nullable: true),
                    QtEntregue = table.Column<int>(type: "int", nullable: false),
                    QtSaidaLote = table.Column<int>(type: "int", nullable: true),
                    SaidaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaSaidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendaSaidas_Producao_ProducaoId",
                        column: x => x.ProducaoId,
                        principalTable: "Producao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VendaSaidas_Saida_SaidaId",
                        column: x => x.SaidaId,
                        principalTable: "Saida",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VendaSaidas_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Saida_ClienteId",
                table: "Saida",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Saida_FuncionarioId",
                table: "Saida",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Saida_ProdutoId",
                table: "Saida",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Saida_VendaId",
                table: "Saida",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaSaidas_ProducaoId",
                table: "VendaSaidas",
                column: "ProducaoId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaSaidas_SaidaId",
                table: "VendaSaidas",
                column: "SaidaId");

            migrationBuilder.CreateIndex(
                name: "IX_VendaSaidas_VendaId",
                table: "VendaSaidas",
                column: "VendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaSaidas");

            migrationBuilder.DropTable(
                name: "Saida");
        }
    }
}
