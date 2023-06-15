using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations.Schema;

namespace SugarProductionManagement.Models {
    public class VendaSaidas {
        public int Id { get; set; }
        public int? ProducaoId { get; set; }
        public int? SaidaId { get; set; }
        public int? VendaId { get; set; }
        public int? QtSaidaLote { get; set; }

        public Producao? Producao { get; set; } 
        public Venda? Venda { get; set; }
        public Saida? Saida { get; set; }
    }
}
