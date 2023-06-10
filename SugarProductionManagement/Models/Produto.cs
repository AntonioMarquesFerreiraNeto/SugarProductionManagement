using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string? Nome { get; set; }
       
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(20, ErrorMessage = "Campo inválido!")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public decimal? Preco { get; set; }

        public string ReturnPrecoFormatado()
        {
            return Preco!.Value.ToString("C2");
        }
    }
}
