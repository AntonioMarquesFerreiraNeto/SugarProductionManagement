using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models {
    public class RecuperarSenha {

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Cpf { get; set; }   
    }
}
