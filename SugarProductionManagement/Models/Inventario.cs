using SugarProductionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace SugarProductionManagement.Models {
    public class Inventario {

        public int Id { get; set; }
        
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? ProducaoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int? QtBaixa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? DescricaoMotivo { get; set; }

        public DateTime? DataDeInventario { get; set; }

        public InventarioStatus Status { get; set; }

        public Funcionario? Funcionario { get; set; }
        public Producao? Producao { get; set; }

        [NotMapped]
        public virtual List<Producao>? ListProducao { get; set; }
    }
}
