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
            funcionarioDB.Status = FuncionarioStatus.Ativo;
            _bancoContext.Funcionario.Update(funcionarioDB);
            _bancoContext.SaveChanges();
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
            funcionarioDB.Status = FuncionarioStatus.Inativo;
            _bancoContext.Funcionario.Update(funcionarioDB);
            _bancoContext.SaveChanges();
            return funcionarioDB;
        }

        public Funcionario Update(Funcionario funcionario) {
            try {
                Funcionario funcionarioDB = GetFuncionarioById(funcionario.Id);
                if (funcionarioDB == null) throw new Exception("Nenhum registro encontrado!");
                funcionarioDB.Name = funcionario.Name;
                funcionarioDB.Rg = funcionario.Rg;
                funcionarioDB.Cpf = funcionario.Cpf;
                funcionarioDB.Tel = funcionario.Tel;
                funcionarioDB.Email = funcionario.Email;
                funcionarioDB.DataNascimento = funcionario.DataNascimento;
                funcionarioDB.Logradouro = funcionario.Logradouro;
                funcionarioDB.NumeroResidencial = funcionario.NumeroResidencial;
                funcionarioDB.ComplementoResidencial = funcionario.ComplementoResidencial;
                funcionarioDB.Bairro = funcionario.Bairro;
                funcionarioDB.Cidade = funcionario.Cidade;
                funcionarioDB.Estado = funcionario.Estado;
                funcionarioDB.Departamento = funcionario.Departamento;

                _bancoContext.Funcionario.Update(funcionarioDB);
                _bancoContext.SaveChanges();
                return funcionario;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
    }
}
