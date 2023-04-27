using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    public class FuncionarioController : Controller {

        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository) {
            _funcionarioRepository = funcionarioRepository;
        }

        public IActionResult Index() {
            ViewData["Title"] = "Funcionários";
            List<Funcionario> funcionarios = _funcionarioRepository.GetAllFuncionarioAtivos();
            return View(funcionarios);
        }

        public IActionResult CreateFuncionario() {
            ViewData["Title"] = "Novo funcionário";
            return View();
        }
        [HttpPost]
        public IActionResult CreateFuncionario(Funcionario funcionario) {
            ViewData["Title"] = "Novo funcionário";
            try {
                if (ModelState.IsValid) {
                    _funcionarioRepository.Create(funcionario);
                    TempData["Sucesso"] = "Registrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(funcionario);
            }
            catch (Exception error) {
                TempData["Erro"] = error.Message;   
                return View(funcionario);
            }
        }

        public IActionResult EditFuncionario(int? id) {
            Funcionario funcionario = _funcionarioRepository.GetFuncionarioById(id);
            if (funcionario == null) {
                TempData["Error"] = "Nenhum registro encontrado!";
                return View(funcionario);
            }
            return View(funcionario);
        }
        [HttpPost]
        public IActionResult EditFuncionario(Funcionario funcionario) {
            return View();
        }
    }
}
