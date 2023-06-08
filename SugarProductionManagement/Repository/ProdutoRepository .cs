using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private List<Produto> _produtos;

        public ProdutoRepository()
        {
            _produtos = new List<Produto>();
        }

        public IEnumerable<Produto> GetAll()
        {
            return _produtos;
        }

        public Produto GetById(int id)
        {
            return _produtos.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Produto produto)
        {
            produto.Id = _produtos.Count + 1;
            _produtos.Add(produto);
        }

        public void Update(Produto produto)
        {
            var existingProduto = _produtos.FirstOrDefault(p => p.Id == produto.Id);
            if (existingProduto != null)
            {
                existingProduto.Nome = produto.Nome;
                existingProduto.Descricao = produto.Descricao;
                existingProduto.Preco = produto.Preco;
            }
        }

        public void Delete(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                _produtos.Remove(produto);
            }
        }
    }

}
