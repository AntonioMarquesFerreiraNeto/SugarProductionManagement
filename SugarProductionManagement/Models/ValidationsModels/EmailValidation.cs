using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SugarProductionManagement.Models.ValidationsModels {
    public class EmailValidation : ValidationAttribute {

        public override bool IsValid(object? value) {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {
                return true;
            }
            return validarEmail(value.ToString());
        }

        public bool validarEmail(string email) {
            bool emailValido = false;
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            try {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException) {
                emailValido = false;
            }
            return emailValido;
        }
    }
}
