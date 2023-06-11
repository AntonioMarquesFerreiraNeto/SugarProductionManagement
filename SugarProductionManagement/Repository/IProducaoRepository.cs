using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IProducaoRepository {
        public Producao Create(Producao producao);
        public Producao Update(Producao producao);
        public Producao Inativar(int id);
        public Producao Ativar(int id);
        public Producao GetById(int id);
        public List<Producao> GetProducaoAtivos();
        public List<Producao> GetProducaoInativos();
    }
}
