using SugarProductionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime? DataVenda { get; set; }

        public VendasStatus VendasStatus { get; set; }

        public Funcionario? Funcionario { get; set; }
        public Cliente? Cliente { get; set; }
        public Produto? Produto { get; set; }

        //Listas que não serão salvas no banco de dados.
        [NotMapped]
        public virtual List<Produto>? ProdutosList { get; set; }
        [NotMapped]
        public virtual List<Cliente>? ClientesList { get; set; }
    }
}
