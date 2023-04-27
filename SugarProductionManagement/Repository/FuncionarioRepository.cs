using SugarProductionManagement.Data;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository {
    public class FuncionarioRepository : IFuncionarioRepository {

        private readonly BancoContext _bancoContext;

        public FuncionarioRepository(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }

        public Funcionario Ativar(Funcionario funcionario) {
            Funcionario funcionarioDB = GetFuncionarioById(funcionario.Id);
            if (funcionarioDB == null) throw new Exception("Nenhum registro encontrado!");
            funcionarioDB.Status = Models.Enums.FuncionarioStatus.Ativo;
            _bancoContext.Funcionario.Update(funcionarioDB);
            return funcionarioDB;
        }

        public Funcionario Create(Funcionario funcionario) {
            try {
                _bancoContext.Funcionario.Add(funcionario);
                _bancoContext.SaveChanges();
                return funcionario;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public List<Funcionario> GetAllFuncionarioAtivos() {
            return _bancoContext.Funcionario.Where(x => x.Status == FuncionarioStatus.Ativo).ToList();
        }

        public List<Funcionario> GetAllFuncionarioInativos() {
            return _bancoContext.Funcionario.Where(x => x.Status == FuncionarioStatus.Inativo).ToList();
        }

        public Funcionario GetFuncionarioById(int? id) {
            return _bancoContext.Funcionario.FirstOrDefault(x => x.Id == id);
        }

        public Funcionario Inativar(Funcionario funcionario) {
            Funcionario funcionarioDB = GetFuncionarioById(funcionario.Id);
            if (funcionarioDB == null) throw new Exception("Nenhum registro encontrado!");
            funcionarioDB.Status = Models.Enums.FuncionarioStatus.Inativo;
            _bancoContext.Funcionario.Update(funcionarioDB);
            return funcionarioDB;
        }

        public Funcionario Update(Funcionario funcionario) {
            throw new NotImplementedException();
        }
    }
}
