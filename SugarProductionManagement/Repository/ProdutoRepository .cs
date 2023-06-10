using SugarProductionManagement.Data;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BancoContext _bancoContext;

        public ProdutoRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _bancoContext.Produtos.ToList();
        }

        public Produto GetById(int id)
        {
            return _bancoContext.Produtos.FirstOrDefault(p => p.Id == id) ?? throw new Exception("Desculpe, nenhum registro encontrado!");
        }

        public Produto Add(Produto produto)
        {
            try
            {
                _bancoContext.Produtos.Add(produto);
                _bancoContext.SaveChanges();
                return produto;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public Produto Update(Produto produto)
        {
            try
            {
                var produtoDB = GetById(produto.Id);
                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Preco = produto.Preco;
                _bancoContext.Update(produtoDB);
                _bancoContext.SaveChanges();
                return produto;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public Produto Delete(int id)
        {
            var produto = GetById(id);
            return null;
        }
    }

}
