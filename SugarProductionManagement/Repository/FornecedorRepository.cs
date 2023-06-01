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

                fornecedorDB.NomeFantasia = fornecedor.NomeFantasia;
                fornecedorDB.RazaoSocial = fornecedor.RazaoSocial;
                fornecedorDB.InscricaoMunicipal = fornecedor.InscricaoMunicipal;
                fornecedorDB.InscricaoEstadual = fornecedor.InscricaoEstadual;
                fornecedorDB.Cnpj = fornecedor.Cnpj;
                fornecedorDB.Tel = fornecedor.Tel;
                fornecedorDB.Email = fornecedor.Email;
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
