using SugarProductionManagement.Data;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly BancoContext _bancoContext;

        public ProdutoRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public Produto GetById(int id)
        {
            return _bancoContext.Produtos.FirstOrDefault(p => p.Id == id) ?? throw new Exception("Desculpe, nenhum registro encontrado!");
        }

        public Produto Add(Produto produto)
        {
            try
            {
                produto.QtEstoque = 0;
                produto.QtReservada = 0;
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

        public Produto Inativar(int id)
        {
            var produto = GetById(id);
            produto.ProdutoStatus = ProdutoStatus.Inativo;
            _bancoContext.Update(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public Produto Ativar(int id) {
            var produto = GetById(id);
            produto.ProdutoStatus = ProdutoStatus.Ativo;
            _bancoContext.Update(produto);
            _bancoContext.SaveChanges();
            return produto;
        }

        public List<Produto> GetAllAtivos() {
            return _bancoContext.Produtos.Where(x => x.ProdutoStatus == ProdutoStatus.Ativo).ToList();
        }
        public List<Produto> GetAllInativos() {
            return _bancoContext.Produtos.Where(x => x.ProdutoStatus == ProdutoStatus.Inativo).ToList();
        }
    }

}
