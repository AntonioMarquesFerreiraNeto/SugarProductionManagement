using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Helpers;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    public class LogarController : Controller {

        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly ISection _section;

        public LogarController(IFuncionarioRepository funcionarioRepository, ISection section) {
            _funcionarioRepository = funcionarioRepository;
            _section = section;
        }
        
        public IActionResult Index() {
            if (_section.buscarSectionUser() != null) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(Autenticar autenticar) {
            try {
                if (ModelState.IsValid) {
                    Funcionario usuario = _funcionarioRepository.ValidarCredenciais(autenticar);
                    _section.CriarSection(usuario);
                    return RedirectToAction("Index", "Home");
                }
                return View(autenticar);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }

        }

        public IActionResult Sair() {
            try {
                _section.EncerrarSection();
                return RedirectToAction("Index", "Logar");
            }
            catch (Exception error) {
                TempData["Erro"] = error.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
