using SugarProductionManagement.Data;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository {
    public class FornecedorRepository : IFornecedorRepository {

        private BancoContext _bancoContext;

        public FornecedorRepository(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }

        public Fornecedor Ativar(Fornecedor fornecedor) {
            try {
                Fornecedor fornecedorDB = GetFornecedorById(fornecedor.Id);
                if (fornecedorDB == null) throw new Exception("Desculpe, houve algum conflito interno!");
                fornecedorDB.Status = FornecedorStatus.Ativo;
                _bancoContext.Fornecedor.Update(fornecedorDB);
                _bancoContext.SaveChanges();
                return fornecedor;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Fornecedor Create(Fornecedor fornecedor) {
            try {
                _bancoContext.Fornecedor.Add(fornecedor);
                _bancoContext.SaveChanges();
                return fornecedor;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public List<Fornecedor> GetFornecedorAtivosAll() {
            return _bancoContext.Fornecedor.Where(x => x.Status == FornecedorStatus.Ativo).ToList();
        }

        public Fornecedor GetFornecedorById(int id) {
            return _bancoContext.Fornecedor.FirstOrDefault(x => x.Id == id);
        }

        public List<Fornecedor> GetFornecedorInativosAll() {
            return _bancoContext.Fornecedor.Where(x => x.Status == FornecedorStatus.Inativo).ToList();
        }

        public Fornecedor Inativar(Fornecedor fornecedor) {
            try {
                Fornecedor fornecedorDB = GetFornecedorById(fornecedor.Id);
                if(fornecedorDB == null) throw new Exception("Desculpe, houve algum conflito interno!");
                fornecedorDB.Status = FornecedorStatus.Inativo;
                _bancoContext.Fornecedor.Update(fornecedorDB);
                _bancoContext.SaveChanges();
                return fornecedor;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }

        public Fornecedor Update(Fornecedor fornecedor) {
            try {
                Fornecedor fornecedorDB = GetFornecedorById(fornecedor.Id);
                if (fornecedorDB == null) throw new Exception("Desculpe, houve algum conflito interno!");

                fornecedorDB.Name = fornecedor.Name;
                fornecedorDB.Rg = fornecedor.Rg;
                fornecedorDB.Cpf = fornecedor.Cpf;
                fornecedorDB.Tel = fornecedor.Tel;
                fornecedorDB.Email = fornecedor.Email;
                fornecedorDB.DataNascimento = fornecedor.DataNascimento;
                fornecedorDB.Logradouro = fornecedor.Logradouro;
                fornecedorDB.NumeroResidencial = fornecedor.NumeroResidencial;
                fornecedorDB.ComplementoResidencial = fornecedor.ComplementoResidencial;
                fornecedorDB.Bairro = fornecedor.Bairro;
                fornecedorDB.Cidade = fornecedor.Cidade;
                fornecedorDB.Estado = fornecedor.Estado;

                _bancoContext.Fornecedor.Update(fornecedorDB);
                _bancoContext.SaveChanges();
                return fornecedorDB;
            }
            catch (Exception error) {
                throw new Exception(error.Message);
            }
        }
    }
}
