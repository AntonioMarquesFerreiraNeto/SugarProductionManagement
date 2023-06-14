using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;
using System.Security.Cryptography.X509Certificates;

namespace SugarProductionManagement.Repository {
    public class ProducaoRepository : IProducaoRepository {

        private readonly BancoContext _bancoContext;

        public ProducaoRepository(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }

        public Producao Ativar(int id) {
            Producao producao = GetById(id);
            producao.Status = StatusProducao.Ativo;
            _bancoContext.Update(producao);
            _bancoContext.SaveChanges();
            return producao;
        }

        public Producao Create(Producao producao) {
            try {
                if (producao.QtProduzida < 1) throw new Exception("Quantidade produzida inválida!");
                AltaEstoqueProduto(producao);
                producao.QtEstoque = producao.QtProduzida;
                _bancoContext.Producao.Add(producao);
                _bancoContext.SaveChanges();
                return producao;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void AltaEstoqueProduto(Producao producao) {
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producao.ProdutoId)!;
            produtoDB.QtEstoque += producao.QtProduzida;
            _bancoContext.Produtos.Update(produtoDB);
        }

        public Producao GetById(int id) {
            return _bancoContext.Producao.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Desculpe, registro não encontrado!");
        }

        public Producao GetByIdJoin(int id) {
            return _bancoContext.Producao
                .AsNoTracking().Include(x => x.Produto)
                .AsNoTracking().Include(x => x.Safra)
                .FirstOrDefault(x => x.Id == id) ?? throw new Exception("Desculpe, registro não encontrado!");
        }

        public List<Producao> GetProducaoAtivos() {
            return _bancoContext.Producao
                .AsNoTracking().Include(x => x.Produto)
                .AsNoTracking().Include(x => x.Safra)
                .Where(x => x.Status == StatusProducao.Ativo).ToList();
        }

        public List<Producao> GetProducaoInativos() {
            return _bancoContext.Producao
                .AsNoTracking().Include(x => x.Produto)
                .AsNoTracking().Include(x => x.Safra)
                .Where(x => x.Status == StatusProducao.Inativo).ToList();
        }

        public Producao Inativar(int id) {
            Producao producao = GetById(id);
            if (_bancoContext.Inventario.Any(x => x.ProducaoId == producao.Id)) throw new Exception("Produção possui inventários!");
            producao.Status = StatusProducao.Inativo;
            _bancoContext.Update(producao);
            _bancoContext.SaveChanges();
            return producao;
        }

        public Producao Update(Producao producao) {
            try {
                if (producao.QtProduzida < 1) throw new Exception("Quantidade produzida inválida!");
                Producao producaoDB = GetById(producao.Id);
                if (_bancoContext.Inventario.Any(x => x.ProducaoId == producaoDB.Id)) throw new Exception("Produção possui inventários!");
                if (producao.QtProduzida != producaoDB.QtProduzida) AltaOrBaixaEstoque(producao, producaoDB);
                //Adicionar validações para não deixar que seja realizado update de produção com vendas, saídas ou invetários.
                producaoDB.ProdutoId = producao.ProdutoId;
                producaoDB.SafraId = producao.SafraId;
                producaoDB.QtProduzida = producao.QtProduzida;
                producaoDB.QtEstoque = producao.QtProduzida; 
                producaoDB.DataProducao = producao.DataProducao;
                producaoDB.DataValidade = producao.DataValidade;
                producaoDB.Lote = producao.Lote;

                _bancoContext.Producao.Update(producaoDB);
                _bancoContext.SaveChanges();
                return producao;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void AltaOrBaixaEstoque(Producao producao, Producao producaoDB) {
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producao.ProdutoId)!;
            int? QtEstoqueProduto = 0;
            int? qtProdutos = 0;
            List<Producao> lista = _bancoContext.Producao.Where(x => x.ProdutoId == produtoDB.Id).ToList();
            foreach (var item in lista) {
                if (produtoDB.Id != item.Id) {
                    QtEstoqueProduto += item.QtProduzida;
                }
            }
            if (producao.QtProduzida > producaoDB.QtProduzida) {
                qtProdutos = producao.QtProduzida - producaoDB.QtProduzida;
                produtoDB.QtEstoque = QtEstoqueProduto + qtProdutos;
            }
            else {
                qtProdutos = producaoDB.QtProduzida - producao.QtProduzida;
                produtoDB.QtEstoque = QtEstoqueProduto - qtProdutos;
            }
            _bancoContext.Produtos.Update(produtoDB);
        }
    }
}
