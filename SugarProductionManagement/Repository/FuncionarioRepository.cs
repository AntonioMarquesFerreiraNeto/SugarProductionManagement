using SugarProductionManagement.Data;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;

namespace SugarProductionManagement.Repository {
    public class FuncionarioRepository : IFuncionarioRepository {

        private readonly BancoContext _bancoContext;
        private readonly IEmail _email;

        public FuncionarioRepository(BancoContext bancoContext, IEmail email) {
            _bancoContext = bancoContext;
            _email = email;
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
                funcionario.SetSenhaUser();
                if (!EnviarSenha(funcionario)) throw new Exception("Desculpe, não conseguimos enviar o e-mail!");
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


        public Funcionario ValidarCredenciais(Autenticar autenticar) {
            Funcionario usuario = _bancoContext.Funcionario.FirstOrDefault(x => x.Cpf == autenticar.Cpf && x.Senha == autenticar.Senha && x.Status == FuncionarioStatus.Ativo) ?? throw new Exception("CPF ou senha inválido!");
            return usuario;
        }

        public Funcionario RecuperationAuth(RecuperarSenha recuperarSenha) {
            Funcionario usuario = _bancoContext.Funcionario.FirstOrDefault(x => x.Email == recuperarSenha.Email && x.Cpf == recuperarSenha.Cpf && x.Status == FuncionarioStatus.Ativo) ?? throw new Exception("CPF ou e-mail inválido!");
            usuario.SetSenhaUser();
            if (!EnviarSenha(usuario)) throw new Exception("Desculpe, não conseguimos enviar a senha!");
            _bancoContext.Funcionario.Update(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public bool EnviarSenha(Funcionario funcionario) {
            string tema = "Sugar Production Management — Credencial para autenticação";
            string mensagem = $"Prezado {funcionario.Name}, <br><br>Gostaríamos de informar que uma senha foi gerada exclusivamente para você. " +
                "Por motivos de segurança, recomendamos que você mantenha essa informação confidencial. " +
                $"Segue abaixo a senha gerada:<br>Senha: <strong>{funcionario.Senha}</strong>" +
                "<br><br>Caso necessário, lembre-se de alterar essa senha periodicamente para garantir a proteção dos seus dados pessoais. " +
                "Caso tenha alguma dúvida ou precise de suporte adicional, não hesite em entrar em contato conosco.";
            return _email.EnviarEmail(funcionario.Email!, tema, mensagem);
        }


        public void CreateFuncionarioPadrao() {
            //Se não tiver nenhum funcionário registrado, ele realiza a inclusão de um funcionário padrão.
            if (!_bancoContext.Funcionario.Any()) {
                Funcionario funcionario = new Funcionario();
                funcionario.Name = "Carlos Caixa D'Água";
                funcionario.Cpf = "74766868021";
                funcionario.Rg = "76686802";
                funcionario.Email = "useradminpadrao@gmail.com";
                funcionario.DataNascimento = DateTime.Now.Date.AddYears(-20);
                funcionario.Tel = "985740125";
                funcionario.Senha = "0000000000000000";
                funcionario.Logradouro = "Rua bill";
                funcionario.NumeroResidencial = "24";
                funcionario.ComplementoResidencial = "Ao lado do bar do tonhão bola oito";
                funcionario.Bairro = "Aeroporto 4";
                funcionario.Cidade = "Goianésia";
                funcionario.Estado = "Goiás";
                funcionario.Departamento = Departamento.Administracao;

                _bancoContext.Funcionario.Add(funcionario);
                _bancoContext.SaveChanges();
            }

        }
    }
}
