using Microsoft.EntityFrameworkCore;
using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository {
    public class SaidaRepository : ISaidaRepository {

        private readonly BancoContext _bancoContext;
        private readonly ISection _section;

        public SaidaRepository(BancoContext bancoContext, ISection section) {
            _bancoContext = bancoContext;
            _section = section;
        }

        public Saida CreateSaida(Saida saida) {
            saida.CodCarregamento = saida.ReturnCodCarregamento();
            saida.Funcionario = _section.buscarSectionUser();
            return saida;
        }

        public List<Producao> ListProducoesByProdutoId(int id) {
            return _bancoContext.Producao
                .AsNoTracking().Include(x => x.Produto)
                .Where(x => x.ProdutoId == id && x.Status == StatusProducao.Ativo).ToList();
        }

        public List<Saida> ListSaidasVendaById(int id) {
            return _bancoContext.Saida.Where(x => x.VendaId == id && x.SaidaStatus == SaidaStatus.Ativo).ToList();
        }

        public List<Saida> ListSaidaVendaInativasById(int id) {
            return _bancoContext.Saida.Where(x => x.VendaId == id && x.SaidaStatus == SaidaStatus.Inativo).ToList();
        }
    }
}
