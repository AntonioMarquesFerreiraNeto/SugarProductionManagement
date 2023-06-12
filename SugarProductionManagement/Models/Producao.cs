using SugarProductionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SugarProductionManagement.Models {
    public class Producao {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? SafraId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? QtProduzida { get; set; }
        public int? QtEstoque { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Lote { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime? DataProducao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime? DataValidade { get; set; }

        public StatusProducao Status { get; set; }

        public virtual List<Producao>? ListInventarios { get; set; }

        //Objetos relacionado.
        public Produto? Produto { get; set; }
        public Safra? Safra { get; set; }  

        //Esta lista não é salva no banco de dados, apenas para listar produtos para seleção.
        [NotMapped]
        public virtual List<Produto>? ListProdutos { get; set; }

        [NotMapped]
        public virtual List<Safra>? ListSafras { get; set; }
    }
}
