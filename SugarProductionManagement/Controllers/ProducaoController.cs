using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Migrations;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    [PagUserAutenticado]
    public class ProducaoController : Controller {

        private readonly IProducaoRepository _producaoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ISafraRepository _safraRepository;

        public ProducaoController(IProducaoRepository producaoRepository, IProdutoRepository produtoRepository, ISafraRepository safraRepository) {
            _producaoRepository = producaoRepository;
            _produtoRepository = produtoRepository;
            _safraRepository = safraRepository;
        }

        public IActionResult Index() {
            List<Producao> producaoList = _producaoRepository.GetProducaoAtivos();
            return View(producaoList);
        }
        public IActionResult Inativos() {
            List<Producao> producaoList = _producaoRepository.GetProducaoInativos();
            return View("Index", producaoList);
        }

        public IActionResult CreateProducao() {
            Producao producao = new Producao();
            producao.ListProdutos = _produtoRepository.GetAllAtivos();
            producao.ListSafras = _safraRepository.GetSafrasAbertas();
            ViewData["Title"] = "Nova entrada de produção";
            return View(producao);
        }

        [HttpPost]
        public IActionResult CreateProducao(Producao producao) {
            producao.ListProdutos = _produtoRepository.GetAllAtivos();
            producao.ListSafras = _safraRepository.GetSafrasAbertas();
            try {
                if (ModelState.IsValid) {
                    Safra safra = _safraRepository.GetSafraById(producao.SafraId);
                    if (producao.DataProducao!.Value < safra.DataAberturaSafra!.Value) {
                        TempData["Error"] = "Data de produção não pode ser inferior a data da Safra!";
                        return View(producao);
                    }
                    if (producao.DataProducao > producao.DataValidade) {
                        TempData["Error"] = "Data de validade não pode ser inferior a data de produção!";
                        return View(producao);
                    }
                    _producaoRepository.Create(producao);
                    TempData["Sucesso"] = "Registrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(producao);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(producao);
            }
        }

        public IActionResult EditProducao(int id) {
            try {
                Producao producao = _producaoRepository.GetById(id);
                producao.ListProdutos = _produtoRepository.GetAllAtivos();
                producao.ListSafras = _safraRepository.GetSafrasAbertas();
                ViewData["Title"] = "Edit entrada de produção";
                return View(producao);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditProducao(Producao producao) {
            producao.ListProdutos = _produtoRepository.GetAllAtivos();
            producao.ListSafras = _safraRepository.GetSafrasAbertas();
            try {
                if (ModelState.IsValid) {
                    Safra safra = _safraRepository.GetSafraById(producao.SafraId);
                    if (producao.DataProducao!.Value < safra.DataAberturaSafra!.Value) {
                        TempData["Error"] = "Data de produção não pode ser inferior a data da Safra!";
                        return View(producao);
                    }
                    if (producao.DataProducao > producao.DataValidade) {
                        TempData["Error"] = "Data de validade não pode ser inferior a data de produção!";
                        return View(producao);
                    }
                    _producaoRepository.Update(producao);
                    TempData["Sucesso"] = "Editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(producao);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(producao);
            }
        }

        public IActionResult Inativar(int id) {
            try {
                var producao = _producaoRepository.GetById(id);
                producao.ListProdutos = _produtoRepository.GetAllAtivos();
                producao.ListSafras = _safraRepository.GetSafrasAbertas();
                return View(producao);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Inativar(Producao producao) {
            try {
                _producaoRepository.Inativar(producao.Id);
                TempData["Sucesso"] = "Inativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Ativar(int id) {
            try {
                var producao = _producaoRepository.GetById(id);
                producao.ListProdutos = _produtoRepository.GetAllAtivos();
                producao.ListSafras = _safraRepository.GetSafrasAbertas();
                return View(producao);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Ativar(Producao producao) {
            try {
                _producaoRepository.Ativar(producao.Id);
                TempData["Sucesso"] = "Ativado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
