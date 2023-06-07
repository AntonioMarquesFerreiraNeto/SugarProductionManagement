using SugarProductionManagement.Models;

namespace SugarProductionManagement.Repository {
    public interface IFuncionarioRepository {
        public Funcionario GetFuncionarioById(int? id);
        public List<Funcionario> GetAllFuncionarioAtivos();
        public List<Funcionario> GetAllFuncionarioInativos();
        public Funcionario Create(Funcionario funcionario);
        public Funcionario Update(Funcionario funcionario);
        public Funcionario Inativar(Funcionario funcionario);
        public Funcionario Ativar(Funcionario funcionario);
        public Funcionario ValidarCredenciais(Autenticar autenticar);
        public Funcionario RecuperationAuth(RecuperarSenha recuperarSenha);
    }
}
