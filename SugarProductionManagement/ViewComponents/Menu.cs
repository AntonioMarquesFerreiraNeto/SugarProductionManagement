using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SugarProductionManagement.ViewComponents {
    public class Menu : ViewComponent {

        private readonly IFuncionarioRepository _funcionarioRepository;

        public Menu(IFuncionarioRepository funcionarioRepository) {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            string sectionUser = HttpContext.Session.GetString("sectionUserAutenticado");

            if (string.IsNullOrEmpty(sectionUser)) {
                return null;
            }
            Funcionario usuario = JsonConvert.DeserializeObject<Funcionario>(sectionUser);
            usuario = _funcionarioRepository.GetFuncionarioById(usuario!.Id);
            return View(usuario);
        }
    }
}
