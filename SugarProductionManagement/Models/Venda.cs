using SugarProductionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SugarProductionManagement.Models {
    public class Venda {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? ClienteId { get; set; }

        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? QtVendida { get; set; }

        public int? QtEntregue { get; set; }

        public DateTime? DataVenda { get; set; }

        public string? CodPedidoVenda { get; set; }

        public VendasStatus VendasStatus { get; set; }

        public Funcionario? Funcionario { get; set; }
        public Cliente? Cliente { get; set; }
        public Produto? Produto { get; set; }

        public virtual List<Saida>? ListSaidas { get; set; }

       //Listas que não serão salvas no banco de dados.
       [NotMapped]
        public virtual List<Produto>? ProdutosList { get; set; }
        [NotMapped]
        public virtual List<Cliente>? ClientesList { get; set; }

        public string ReturnQtEntregar() {
            return $"{QtVendida - QtEntregue}";
        }

        public string ReturnCodVendas() {
            Random rdn = new Random();
            string caixaCaracteres = "0123456789ABCEDFGHIJL";
            var codSafra = new StringBuilder();
            for (int c = 0; c < 10; c++) {
                int indice = rdn.Next(0, caixaCaracteres.Length - 1);
                codSafra.Append(caixaCaracteres[indice]);
            }
            return codSafra.ToString();
        }
    }
}
