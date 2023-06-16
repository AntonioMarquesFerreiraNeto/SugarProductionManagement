using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;
using System.CodeDom.Compiler;

namespace SugarProductionManagement.Controllers {

    [PagUserCadastro]
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
        public IActionResult Inativos() {
            ViewData["Title"] = "Funcionários inativos";
            List<Funcionario> funcionarios = _funcionarioRepository.GetAllFuncionarioInativos();
            return View("Index", funcionarios);
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
                TempData["Error"] = error.Message;   
                return View(funcionario);
            }
        }

        public IActionResult EditFuncionario(int? id) {
            ViewData["Title"] = "Editar funcionário";
            Funcionario funcionario = _funcionarioRepository.GetFuncionarioById(id);
            if (funcionario == null) {
                TempData["Error"] = "Nenhum registro encontrado!";
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
        [HttpPost]
        public IActionResult EditFuncionario(Funcionario funcionario) {
            ViewData["Title"] = "Editar funcionário";
            try {
                if (ModelState.IsValid) {
                    _funcionarioRepository.Update(funcionario);
                    TempData["Sucesso"] = "Editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(funcionario);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(funcionario);
            }
        }

        public IActionResult InativarFuncionario(int? id) {
            ViewData["Title"] = "Inativar funcionário";
            Funcionario funcionario = _funcionarioRepository.GetFuncionarioById(id);
            if (funcionario == null) {
                TempData["Error"] = "Nenhum registro encontrado!";
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
        [HttpPost]
        public IActionResult InativarFuncionario(Funcionario funcionario) {
            try {
                ViewData["Title"] = "Inativar funcionário";
                _funcionarioRepository.Inativar(funcionario);
                TempData["Sucesso"] = "Inativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult AtivarFuncionario(int? id) {
            ViewData["Title"] = "Ativar funcionário";
            Funcionario funcionario = _funcionarioRepository.GetFuncionarioById(id);
            if (funcionario == null) {
                TempData["Error"] = "Nenhum registro encontrado!";
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
        [HttpPost]
        public IActionResult AtivarFuncionario(Funcionario funcionario) {
            try {
                ViewData["Title"] = "Ativar funcionário";
                _funcionarioRepository.Ativar(funcionario);
                TempData["Sucesso"] = "Ativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
