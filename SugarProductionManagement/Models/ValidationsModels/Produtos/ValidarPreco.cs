using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models.ValidationsModels.Produtos {
    public class ValidarPreco : ValidationAttribute {
        public override bool IsValid(object value) {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {
                return false;
            }
            return ValidarValor(value.ToString());
        }
        public bool ValidarValor(string value) {
            decimal valor = decimal.Parse(value);
            if (valor < 1) {
                return false;
            }
            return true;
        }
    }
}
