using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

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
            vendaDB.VendasStatus = VendasStatus.Inativo;
            _bancoContext.Venda.Update(vendaDB);
            _bancoContext.SaveChanges();
            return vendaDB;
        }

        public Venda NewVenda(Venda venda) {
            try {
                Funcionario funcionario = _section.buscarSectionUser();
                venda.FuncionarioId = funcionario.Id;
                venda.DataVenda = DateTime.Now.Date;
                _bancoContext.Venda.Add(venda);
                _bancoContext.SaveChanges();
                return venda;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Venda UpdateVenda(Venda venda) {
            try {
                Venda vendaDB = GetById(venda.Id);
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
