using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IProdutoRepository {
        List<Produto> GetAllAtivos();
        List<Produto> GetAllInativos();
        Produto GetById(int id);
        Produto Add(Produto produto);
        Produto Update(Produto produto);
        Produto Inativar(int id);
        Produto Ativar(int id);
    }
}
