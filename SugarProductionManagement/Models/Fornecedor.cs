using SugarProductionManagement.Models.Enums;
using SugarProductionManagement.Models.ValidationsModels.Pessoas;
using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models {
    public class Fornecedor : Endereco {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(2, ErrorMessage = "Campo inválido!")]
        public string? NomeFantasia { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [ValidationCNPJ(ErrorMessage = "Campo inválido!")]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(3, ErrorMessage = "Campo inválido!")]
        public string? RazaoSocial { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(9, ErrorMessage = "Campo inválido!")]
        public string? InscricaoEstadual { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(11, ErrorMessage = "Campo inválido!")]
        public string? InscricaoMunicipal { get; set; }


        [EmailValidation(ErrorMessage = "Campo inválido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(9, ErrorMessage = "Campo inválido!")]
        [MaxLength(9, ErrorMessage = "Campo inválido!")]
        public string? Tel { get; set; }

        public string ReturnCnpjCliente() {
            return $"{Convert.ToUInt64(Cnpj): 00\\.000\\.000\\/0000-00}";
        }

        public FornecedorStatus Status { get; set; }
    }
}
