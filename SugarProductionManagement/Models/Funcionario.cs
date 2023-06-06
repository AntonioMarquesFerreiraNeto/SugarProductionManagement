using SugarProductionManagement.Models.Enums;
using SugarProductionManagement.Models.ValidationsModels.Pessoas;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SugarProductionManagement.Models {
    public class Funcionario : Endereco {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "Campo inválido!")]
        public string? Rg { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [ValidationCPF(ErrorMessage = "Campo inválido!")]
        public string? Cpf { get; set; }

        [EmailValidation(ErrorMessage = "Campo inválido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(9, ErrorMessage = "Campo inválido!")]
        [MaxLength(9, ErrorMessage = "Campo inválido!")]
        public string? Tel { get; set; }

        [ValidationDateNascimento (ErrorMessage = "Campo inválido!")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime? DataNascimento { get; set; }

        public FuncionarioStatus Status { get; set; }

        [Required (ErrorMessage = "Campo obrigatório!")]
        public Departamento? Departamento { get; set; }

        public string? Senha { get; set; }


        public void SetSenhaUser() {
            Random rdn = new Random();
            string caixaCaracteres = "qwertyuiopasdfghjklzxcvbnm123456789" + "qwertyuiopasdfghjklzxcvbnm".ToUpper();    
            StringBuilder senha = new StringBuilder();
            for (int c = 0; c < 16; c ++) {
                int indiceSenha = rdn.Next(0, caixaCaracteres.Length - 1);
                senha.Append(caixaCaracteres[indiceSenha]);
            }
            Senha = senha.ToString();
        }
    }
}
