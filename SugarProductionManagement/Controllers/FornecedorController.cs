using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    public class FornecedorController : Controller {

        private readonly IFornecedorRepository _repository;

        public FornecedorController(IFornecedorRepository repository) {
            _repository = repository;
        }

        public IActionResult Index() {
            ViewData["Title"] = "Fornecedores";
            List<Fornecedor> fornecedores = _repository.GetFornecedorAtivosAll();
            return View(fornecedores);
        }
        public IActionResult Inativos() {
            ViewData["Title"] = "Fornecedores";
            List<Fornecedor> fornecedores = _repository.GetFornecedorInativosAll();
            return View("Index", fornecedores);
        }


        public IActionResult CreateFornecedor() {
            ViewData["Title"] = "Novo fornecedor";
            return View();
        }
        [HttpPost]
        public IActionResult CreateFornecedor(Fornecedor fornecedor) {
            ViewData["Title"] = "Novo fornecedor";
            try {
                if (ModelState.IsValid) {
                    _repository.Create(fornecedor);
                    TempData["Sucesso"] = "Registrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(fornecedor);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(fornecedor);
            }
        }

        public IActionResult EditFornecedor(int id) {
            ViewData["Title"] = "Editar fornecedor";
            Fornecedor fornecedor = _repository.GetFornecedorById(id);
            if (fornecedor == null) {
                TempData["Error"] = "Desculpe, registro não encontrado!";
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        [HttpPost]
        public IActionResult EditFornecedor(Fornecedor fornecedor) {
            ViewData["Title"] = "Editar fornecedor";
            try {
                if (ModelState.IsValid) {
                    _repository.Update(fornecedor);
                    TempData["Sucesso"] = "Editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(fornecedor);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(fornecedor);
            }
        }

        public IActionResult InativarFornecedor(int id) {
            ViewData["Title"] = "Inativar fornecedor";
            Fornecedor fornecedor = _repository.GetFornecedorById(id);
            if (fornecedor == null) {
                TempData["Error"] = "Desculpe, registro não encontrado!";
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        [HttpPost]
        public IActionResult InativarFornecedor(Fornecedor fornecedor) {
            ViewData["Title"] = "Inativar fornecedor";
            try {
                _repository.Inativar(fornecedor);
                TempData["Sucesso"] = "Inativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(fornecedor);
            }
        }

        public IActionResult AtivarFornecedor(int id) {
            ViewData["Title"] = "Ativar fornecedor";
            Fornecedor fornecedor = _repository.GetFornecedorById(id);
            if (fornecedor == null) {
                TempData["Error"] = "Desculpe, registro não encontrado!";
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        [HttpPost]
        public IActionResult AtivarFornecedor(Fornecedor fornecedor) {
            ViewData["Title"] = "Ativar fornecedor";
            try {
                _repository.Ativar(fornecedor);
                TempData["Sucesso"] = "Ativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(fornecedor);
            }
        }
    }
}
