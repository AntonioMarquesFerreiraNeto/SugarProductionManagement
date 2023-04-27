using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models {
    public class Endereco {

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? NumeroResidencial { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(3, ErrorMessage = "Campo inválido!")]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(2, ErrorMessage = "Campo inválido!")]
        public string? ComplementoResidencial { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(2, ErrorMessage = "Campo inválido!")]
        public string? Bairro { get; set; }

        [MinLength(3, ErrorMessage = "Campo inválido!")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]

        [MinLength(2, ErrorMessage = "Campo inválido!")]
        public string? Estado { get; set; }
    }
}
