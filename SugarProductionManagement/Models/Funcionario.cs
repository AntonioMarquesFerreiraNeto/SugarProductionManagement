using SugarProductionManagement.Models.Enums;
using SugarProductionManagement.Models.ValidationsModels;
using System.ComponentModel.DataAnnotations;

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
    }
}
