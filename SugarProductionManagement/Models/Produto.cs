using SugarProductionManagement.Models.Enums;
using SugarProductionManagement.Models.ValidationsModels.Produtos;
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

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [ValidarPreco(ErrorMessage = "Campo inválido!")]
        public decimal? Preco { get; set; }

        public ProdutoStatus ProdutoStatus { get; set; }

        public virtual List<Producao>? ListProducao { get; set; }  
        public virtual List<Venda>? ListVendas { get; set; }

        public string ReturnPrecoFormatado()
        {
            return Preco!.Value.ToString("C2");
        }
    }
}
