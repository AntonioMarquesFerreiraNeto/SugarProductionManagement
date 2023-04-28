using System.ComponentModel.DataAnnotations;

namespace SugarProductionManagement.Models.ValidationsModels {
    public class ValidationDateNascimento : ValidationAttribute {
        
        public override bool IsValid(object? value) {
            if (value == null) {
                return false;
            }
            return ValidarDataNascimento(value.ToString());
        }

        public bool ValidarDataNascimento(string value) {
            DateTime dataNascimento = DateTime.Parse(value).Date;
            DateTime dataAtual = DateTime.Now.Date;

            long dias = (int)dataAtual.Subtract(dataNascimento).TotalDays;
            int idade = (int)dias / 365;

            if (idade < 18 || idade > 132) {
                return false;
            }
            return true;
        }
    }
}
