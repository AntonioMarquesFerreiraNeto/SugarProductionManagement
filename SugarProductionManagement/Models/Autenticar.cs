using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models {
    public class Autenticar {

        [Required (ErrorMessage = "Campo obrigatório!")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Senha { get; set; }
    }
}
