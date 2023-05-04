using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IFornecedorRepository {
        public Fornecedor Create(Fornecedor fornecedor);
        public Fornecedor GetFornecedorById(int id);
        public List<Fornecedor> GetFornecedorAtivosAll();
        public List<Fornecedor> GetFornecedorInativosAll();
        public Fornecedor Update(Fornecedor fornecedor);
        public Fornecedor Inativar(Fornecedor fornecedor);
        public Fornecedor Ativar(Fornecedor fornecedor);
    }
}
