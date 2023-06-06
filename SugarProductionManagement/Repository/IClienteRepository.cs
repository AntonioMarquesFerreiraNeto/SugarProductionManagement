using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IClienteRepository {
        public Cliente Create(Cliente fornecedor);
        public Cliente GetFornecedorById(int id);
        public List<Cliente> GetFornecedorAtivosAll();
        public List<Cliente> GetFornecedorInativosAll();
        public Cliente Update(Cliente fornecedor);
        public Cliente Inativar(Cliente fornecedor);
        public Cliente Ativar(Cliente fornecedor);
    }
}
