using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    [PagUserAutenticado]
    public class InventarioController : Controller {

        private readonly IInventarioRepository _inventarioRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProducaoRepository _producaoRepository;

        public InventarioController(IInventarioRepository inventarioController, IProdutoRepository produtoRepository, IProducaoRepository producaoRepository) {
            _inventarioRepository = inventarioController;
            _produtoRepository = produtoRepository;
            _producaoRepository = producaoRepository;
        }

        public IActionResult Index() {
            List<Inventario> ListInventario = _inventarioRepository.GetAtivosAll();
            return View(ListInventario);
        }
        public IActionResult Inativos() {
            List<Inventario> ListInventario = _inventarioRepository.GetInativosAll();
            return View("Index", ListInventario);
        }

        public IActionResult NewInventario() {
            Inventario inventario = new Inventario();
            inventario.ListProducao = _producaoRepository.GetProducaoAtivos();
            return View(inventario);
        }

        [HttpPost]
        public IActionResult NewInventario(Inventario inventario) {
            inventario.ListProducao = _producaoRepository.GetProducaoAtivos();
            try {
                if (ModelState.IsValid) {
                    _inventarioRepository.Create(inventario);
                    TempData["Sucesso"] = "Registrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(inventario);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(inventario);
            }
        }

        public IActionResult Edit(int id) {
            Inventario inventario = _inventarioRepository.GetById(id);
            inventario.ListProducao = _producaoRepository.GetProducaoAtivos();
            return View(inventario);
        }

        [HttpPost]
        public IActionResult Edit(Inventario inventario) {
            inventario.ListProducao = _producaoRepository.GetProducaoAtivos();
            try {
                if (ModelState.IsValid) {
                    _inventarioRepository.Update(inventario);
                    TempData["Sucesso"] = "Atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(inventario);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(inventario);
            }
        }

        public IActionResult Inativar(int id) {
            Inventario inventario = _inventarioRepository.GetById(id);
            inventario.ListProducao = _producaoRepository.GetProducaoAtivos();
            return View(inventario);
        }

        [HttpPost]
        public IActionResult Inativar(Inventario inventario) {
            try {
                _inventarioRepository.Inativar(inventario.Id);
                TempData["Sucesso"] = "Inativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
