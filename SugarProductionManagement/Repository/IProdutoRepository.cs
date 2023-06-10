using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetAll();
        Produto GetById(int id);
        Produto Add(Produto produto);
        Produto Update(Produto produto);
        Produto Delete(int id);
    }

}
