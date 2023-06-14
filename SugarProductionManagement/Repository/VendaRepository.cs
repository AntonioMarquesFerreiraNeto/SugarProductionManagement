using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;
using System.Configuration;

namespace SugarProductionManagement.Repository {
    public class VendaRepository : IVendaRepository {

        private readonly BancoContext _bancoContext;
        private readonly ISection _section;

        public VendaRepository(BancoContext bancoContext, ISection section) {
            _bancoContext = bancoContext;
            _section = section;
        }

        public Venda GetById(int id) {
            return _bancoContext.Venda.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Desculpe, objeto não encontrado!");
        }

        public Venda InativarVenda(int id) {
            Venda vendaDB = GetById(id);
            if (vendaDB.QtEntregue != 0) throw new Exception("Esta venda possui saídas!");
            RetirarReservasProduto(vendaDB);
            vendaDB.VendasStatus = VendasStatus.Inativo;
            _bancoContext.Venda.Update(vendaDB);
            _bancoContext.SaveChanges();
            return vendaDB;
        }
        public void RetirarReservasProduto(Venda venda) {
            Produto produto = _bancoContext.Produtos.FirstOrDefault(x => x.Id == venda.ProdutoId)!;
            produto.QtReservada = produto.QtReservada - venda.QtVendida;
            _bancoContext.Produtos.Update(produto);
        }

        public Venda NewVenda(Venda venda) {
            try {
                Funcionario funcionario = _section.buscarSectionUser();
                venda.CodPedidoVenda = venda.ReturnCodVendas();
                venda.QtEntregue = 0;
                venda.FuncionarioId = funcionario.Id;
                venda.DataVenda = DateTime.Now.Date;
                SetQtReservada(venda);
                _bancoContext.Venda.Add(venda);
                _bancoContext.SaveChanges();
                return venda;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void SetQtReservada(Venda venda) {
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == venda.ProdutoId)!;
            produtoDB.QtReservada += venda.QtVendida;
            _bancoContext.Produtos.Update(produtoDB);
        }

        public Venda UpdateVenda(Venda venda) {
            try {
                Venda vendaDB = GetById(venda.Id);
                if(venda.QtVendida != vendaDB.QtVendida) setQtReservadaUpdate(venda, vendaDB);
                vendaDB.QtVendida = venda.QtVendida;
                vendaDB.ClienteId = venda.ClienteId;
                vendaDB.ProdutoId = venda.ProdutoId;

                _bancoContext.Venda.Update(vendaDB);
                _bancoContext.SaveChanges();
                return venda;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
        public void setQtReservadaUpdate(Venda venda, Venda vendaDB) {
            Produto produtoDB = _bancoContext.Produtos.FirstOrDefault(x => x.Id == vendaDB.ProdutoId)!;
            int? qtAlterada = 0;
            if (venda.QtVendida > vendaDB.QtVendida) {
                qtAlterada = venda.QtVendida - vendaDB.QtVendida;
                produtoDB.QtReservada += qtAlterada;
            }
            else {
                qtAlterada = vendaDB.QtVendida - venda.QtVendida;
                produtoDB.QtReservada -= qtAlterada;
            }
            _bancoContext.Produtos.Update(produtoDB);
        }


        public List<Venda> VendasAllAtivas() {
            return _bancoContext.Venda
                .AsNoTracking().Include(x => x.Produto)
                .AsNoTracking().Include(x => x.Funcionario)
                .AsNoTracking().Include(x => x.Cliente)
                .Where(x => x.VendasStatus == VendasStatus.Ativa).ToList();
        }

        public List<Venda> VendasAllInativas() {
            return _bancoContext.Venda
                .AsNoTracking().Include(x => x.Produto)
                .AsNoTracking().Include(x => x.Funcionario)
                .AsNoTracking().Include(x => x.Cliente)
                .Where(x => x.VendasStatus == VendasStatus.Inativo).ToList();
        }
    }
}
