using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository {
    public class InventarioRepository : IInventarioRepository {

        private readonly BancoContext _bancoContext;
        private readonly ISection _section;

        public InventarioRepository(BancoContext bancoContext, ISection section) {
            _bancoContext = bancoContext;
            _section = section;
        }

        public Inventario Create(Inventario inventario) {
            try {
                inventario.DataDeInventario = DateTime.Now;
                Funcionario funcionario = _section.buscarSectionUser();
                inventario.FuncionarioId = funcionario.Id;
                if (inventario.QtBaixa < 1) throw new Exception("Quantidade de baixas inválida!");
                BaixaEstoque(inventario);

                _bancoContext.Inventario.Add(inventario);
                _bancoContext.SaveChanges();
                return inventario;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void BaixaEstoque(Inventario inventario) {
            Producao producaoDB = _bancoContext.Producao.FirstOrDefault(x => x.Id == inventario.ProducaoId!)! ?? throw new Exception("Desculpe, objeto não encontrado!");
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producaoDB.ProdutoId)!;
            if (producaoDB.QtEstoque == 0) throw new Exception("Desculpe, não possui esse tipo de produto em estoque para permitir inventário!");
            if (producaoDB.QtEstoque >= inventario.QtBaixa) {
                producaoDB.QtEstoque = producaoDB.QtEstoque - inventario.QtBaixa;
                produtoDB.QtEstoque = produtoDB.QtEstoque - inventario.QtBaixa;
                _bancoContext.Produtos.Update(produtoDB);
                _bancoContext.Producao.Update(producaoDB);
            }
            else {
                throw new Exception("Quantidade de baixas inválida!");
            }
        }

        public List<Inventario> GetAtivosAll() {
            return _bancoContext.Inventario
                .AsNoTracking().Include(x => x.Funcionario)
                 .AsNoTracking().Include(x => x.Producao).ThenInclude(x => x!.Produto)
                .Where(x => x.Status == InventarioStatus.Ativo).ToList();
        }

        public Inventario GetById(int id) {
            return _bancoContext.Inventario.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Desculpe, objeto não encontrado!");
        }

        public List<Inventario> GetInativosAll() {
            return _bancoContext.Inventario
                .AsNoTracking().Include(x => x.Funcionario)
                .AsNoTracking().Include(x => x.Producao).ThenInclude(x => x!.Produto)
                .Where(x => x.Status == InventarioStatus.Inativo).ToList();
        }

        public Inventario Inativar(int id) {
            Inventario inventarioDB = GetById(id);
            inventarioDB.Status = InventarioStatus.Inativo;
            AltaEstoque(inventarioDB.ProducaoId!.Value, inventarioDB.QtBaixa!.Value);
            _bancoContext.Update(inventarioDB);
            _bancoContext.SaveChanges();
            return inventarioDB;
        }
        public void AltaEstoque(int id, int qtBaixa) {
            Producao producao = _bancoContext.Producao.FirstOrDefault(x => x.Id == id)!;
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producao.ProdutoId)!;
            producao.QtEstoque = producao.QtEstoque + qtBaixa;
            produtoDB.QtEstoque += qtBaixa;
            _bancoContext.Produtos.Update(produtoDB);
            _bancoContext.Producao.Update(producao);
        }


        public Inventario Update(Inventario inventario) {
            try {
                if (inventario.QtBaixa < 1) throw new Exception("Quantidade de baixas inválida!");
                Inventario inventarioDB = GetById(inventario.Id);
                //Se a quantidade de baixa no banco for igual a nova quantidade de baixa, não é necessário realizar a baixa ou alta de estoque.
                if (inventario.QtBaixa != inventarioDB.QtBaixa) {
                    BaixaOrAltaEstoque(inventario, inventarioDB);
                }

                inventarioDB.DescricaoMotivo = inventario.DescricaoMotivo;
                inventarioDB.QtBaixa = inventario.QtBaixa;
                _bancoContext.Inventario.Update(inventarioDB);
                _bancoContext.SaveChanges();
                return inventarioDB;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void BaixaOrAltaEstoque(Inventario inventario, Inventario inventarioDB) {
            Producao producaoDB = _bancoContext.Producao.FirstOrDefault(x => x.Id == inventarioDB.ProducaoId!)! ?? throw new Exception("Desculpe, objeto não encontrado!");
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == producaoDB.ProdutoId)!;
            if (producaoDB.QtEstoque == 0) throw new Exception("Desculpe, não possui esse tipo de produto em estoque para permitir inventário!");
            if (producaoDB.QtEstoque >= inventario.QtBaixa) {
                if (inventario.QtBaixa > inventarioDB.QtBaixa) {
                    producaoDB.QtEstoque = (producaoDB.QtEstoque + inventarioDB.QtBaixa) - inventario.QtBaixa;
                    produtoDB.QtEstoque = -inventario.QtBaixa;
                }
                else {
                    int alta = inventarioDB.QtBaixa!.Value - inventario.QtBaixa.Value;
                    producaoDB.QtEstoque += alta;
                    produtoDB.QtEstoque += inventario.QtBaixa;
                }
                _bancoContext.Produtos.Update(produtoDB);
                _bancoContext.Producao.Update(producaoDB);
            }
            else {
                throw new Exception("Quantidade de baixas inválida!");
            }
        }
    }
}
