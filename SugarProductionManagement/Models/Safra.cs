using SugarProductionManagement.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SugarProductionManagement.Models {
    public class Safra {
        
        public int? Id { get; set; }
        
        public string? CodSafra { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime? DataAberturaSafra { get; set; }

        public DateTime? DataFechamentoSafra { get; set; }

        public string? YearSafra { get; set; }

        public StatusSafra StatusSafra { get; set; }

        public string ReturnCodSafra() {
            Random rdn = new Random();
            string caixaCaracteres = "0123456789ABCEDFGHIJL";
            var codSafra = new StringBuilder();
            for (int c = 0; c < 10; c++) {
                int indice = rdn.Next(0, caixaCaracteres.Length -1);
                codSafra.Append(caixaCaracteres[indice]);
            }
            return codSafra.ToString(); 
        }

        public string? ReturnStatusSafra() {
            if (StatusSafra == StatusSafra.Aberta) {
                return "Aberta";
            }
            else {
                return "Fechada";
            }
        }
        public string? ReturnDataAberturaSafra() {
            return $"{DataAberturaSafra!.Value:dd/MM/yyyy}";
        }

        public string? ReturnDataFechamentoSafra() {
            return $"{DataFechamentoSafra!.Value:dd/MM/yyyy}";
        }
    }
}
