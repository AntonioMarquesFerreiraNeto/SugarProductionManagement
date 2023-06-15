using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;
using System.Text;

namespace SugarProductionManagement.Repository {
    public class SaidaRepository : ISaidaRepository {

        private readonly BancoContext _bancoContext;
        private readonly ISection _section;

        public SaidaRepository(BancoContext bancoContext, ISection section) {
            _bancoContext = bancoContext;
            _section = section;
        }

        public Saida CreateSaida(Saida saida, List<VendaSaidas> vendaSaidas) {
            try {
                saida.CodCarregamento = saida.ReturnCodCarregamento();
                Funcionario funcionario = _section.buscarSectionUser();
                saida.Id = default;
                saida.ClienteId = saida.Venda!.ClienteId;
                saida.FuncionarioId = funcionario.Id;
                saida.QtSaidaTotal = vendaSaidas.Sum(x => x.QtSaidaLote);
                AddListVendaSaidas(saida, vendaSaidas);
                BaixaEstoque(saida, vendaSaidas);
                AlterQtEntregueAndEntregarVendas(saida);
                _bancoContext.Saida.Add(saida);
                _bancoContext.SaveChanges();
                return saida;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void AlterQtEntregueAndEntregarVendas(Saida saida) {
            Venda vendaDB = _bancoContext.Venda.FirstOrDefault(x => x.Id == saida.VendaId)!;
            if (vendaDB.QtEntregue == vendaDB.QtVendida) throw new Exception("Venda possui todos os carregamentos entregues!"); 
            vendaDB.QtEntregue = vendaDB.QtEntregue + saida.QtSaidaTotal;
            if (vendaDB.QtEntregue > vendaDB.QtVendida) throw new Exception("Ação inválida, quantidade total informada passa a quantia de vendas!");
            _bancoContext.Venda.Update(vendaDB);
        }
        public void AddListVendaSaidas(Saida saida, List<VendaSaidas> list) {
            foreach (var item in list) {
                item.VendaId = saida.VendaId;
                item.Saida = saida;
                _bancoContext.VendaSaidas.Add(item);
            }
        }
        public void BaixaEstoque(Saida saida, List<VendaSaidas> list) {
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == saida.ProdutoId)!;
            produtoDB.QtEstoque = produtoDB.QtEstoque - saida.QtSaidaTotal;
            produtoDB.QtReservada = produtoDB.QtReservada - saida.QtSaidaTotal;
            _bancoContext.Produtos.Update(produtoDB);
            foreach (var item in list) {
                Producao producao = _bancoContext.Producao.FirstOrDefault(x => x.Id == item.Producao!.Id)!;
                producao.QtEstoque = producao.QtEstoque - item.QtSaidaLote;
                _bancoContext.Producao.Update(producao);
            }
        }

        public List<Producao> ListProducoesByProdutoId(int id) {
            return _bancoContext.Producao
                .AsNoTracking().Include(x => x.Produto)
                .Where(x => x.ProdutoId == id && x.Status == StatusProducao.Ativo).ToList();
        }

        public List<Saida> ListSaidasVendaById(int id) {
            return _bancoContext.Saida
                .AsNoTracking().Include(x => x.Funcionario)
                .AsNoTracking().Include(x => x.Cliente)
                .AsNoTracking().Include(x => x.Venda)
                .Where(x => x.VendaId == id && x.SaidaStatus == SaidaStatus.Ativo).ToList();
        }

        public List<Saida> ListSaidaVendaInativasById(int id) {
            return _bancoContext.Saida.Where(x => x.VendaId == id && x.SaidaStatus == SaidaStatus.Inativo).ToList();
        }
    }
}
