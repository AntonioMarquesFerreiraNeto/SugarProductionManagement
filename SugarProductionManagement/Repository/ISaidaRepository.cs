using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface ISaidaRepository {
        public Saida CreateSaida(Saida saida, List<VendaSaidas> vendaSaidas);
        public List<Saida> ListSaidasVendaById(int id);
        public List<Saida> ListSaidaVendaInativasById(int id);
        public List<Producao> ListProducoesByProdutoId(int id);
        public Saida GetById(int id);
        public Saida InativarSaida(int id);
    }
}
