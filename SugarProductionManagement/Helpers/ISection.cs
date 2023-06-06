using SugarProductionManagement.Models;

namespace SugarProductionManagement.Helpers {
    public interface ISection {
        void CriarSection(Funcionario usuario);
        void EncerrarSection();
        Funcionario buscarSectionUser();
    }
}
