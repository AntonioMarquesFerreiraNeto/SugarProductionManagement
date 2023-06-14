using SugarProductionManagement.Models.Enums;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SugarProductionManagement.Models {
    public class Saida {
        public int Id { get; set; }
        public string? CodCarregamento { get; set; }
        public int? QtSaidaTotal { get; set; }
        public int? FuncionarioId { get; set; }
        public int? VendaId { get; set; }
        public int? ProdutoId { get; set; }
        public int? ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
        public Produto? Produto { get; set; }
        public Venda? Venda { get; set; }
        public Funcionario? Funcionario { get; set; }

        public SaidaStatus SaidaStatus { get; set; }

        public string ReturnCodCarregamento() {
            Random rdn = new Random();
            string caixaCaracteres = "0123456789ABCEDFGHIJL";
            var codSafra = new StringBuilder();
            for (int c = 0; c < 10; c++) {
                int indice = rdn.Next(0, caixaCaracteres.Length - 1);
                codSafra.Append(caixaCaracteres[indice]);
            }
            return codSafra.ToString();
        }

        public virtual List<VendaSaidas>? ListVendaSaidas { get; set; }

        [NotMapped]
        public List<Producao>? ListProducoesProduto { get; set; }
    }
}
