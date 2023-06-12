using SugarProductionManagement.Data;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository {
    public class SafraRepository : ISafraRepository {

        private readonly BancoContext _bancoContext;

        public SafraRepository(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }

        public Safra CreateSafra(Safra safra) {
            try {
                safra.YearSafra = safra.DataAberturaSafra!.Value.Year.ToString();
                if (_bancoContext.Safra.Any(x => x.DataAberturaSafra!.Value.Year == safra.DataAberturaSafra.Value.Year)) throw new Exception("Não é possível ter duas safras abertas no mesmo ano!");
                _bancoContext.Safra.Add(safra);
                _bancoContext.SaveChanges();
                return safra;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Safra DeleteSafra(int? id) {
            try {
                Safra safraDB = GetSafraById(id);
                if (safraDB.StatusSafra == StatusSafra.Fechado) throw new Exception("Esta safra não pode ser deletada!");
                bool temProducao = _bancoContext.Producao.Any(x => x.SafraId == id);
                if (temProducao) throw new Exception("Safra possui entrada de produção!");
                _bancoContext.Safra.Remove(safraDB);
                _bancoContext.SaveChanges();
                return safraDB;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Safra GetSafraById(int? id) {
            return _bancoContext.Safra.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Registro não encontrado!");
        }

        public Safra FecharSafra(int? id) {
            try {
                Safra safraDB = GetSafraById(id);
                safraDB.DataFechamentoSafra = DateTime.Now;
                if (safraDB.StatusSafra == StatusSafra.Fechado) throw new Exception("Desculpe, safra já se encontra fechada!");
                safraDB.StatusSafra = StatusSafra.Fechado;
                _bancoContext.SaveChanges();
                return safraDB;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public List<Safra> GetAllSafras() {
            return _bancoContext.Safra.ToList();
        }

        public List<Safra> GetSafrasAbertas() {
            return _bancoContext.Safra.Where(x => x.StatusSafra == StatusSafra.Aberta).ToList();
        }
    }
}