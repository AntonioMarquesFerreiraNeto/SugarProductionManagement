using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IVendaRepository {
        public Venda NewVenda(Venda venda);
        public Venda UpdateVenda(Venda venda);
        public Venda InativarVenda(int id);
        public Venda GetById(int id);
        public List<Venda> VendasAllAtivas();
        public List<Venda> VendasAllInativas();
    }
}
