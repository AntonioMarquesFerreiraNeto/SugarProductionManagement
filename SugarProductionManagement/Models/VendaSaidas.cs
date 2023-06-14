using System.ComponentModel.DataAnnotations.Schema;

namespace SugarProductionManagement.Models {
    public class VendaSaidas {
        public int Id { get; set; }
        public int? ProducaoId { get; set; }
        public int? VendaId { get; set; }
        public int QtEntregue { get; set; }
        public int? QtSaidaLote { get; set; }

        public Producao? Producao { get; set; } 
        public Venda? Venda { get; set; }
    }
}
