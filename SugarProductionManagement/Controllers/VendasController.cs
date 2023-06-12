using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    [PagUserAutenticado]
    public class VendasController : Controller {

        private readonly IVendaRepository _vendaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public VendasController(IVendaRepository vendaRepository, IProdutoRepository produtoRepository, IClienteRepository clienteRepository) {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index() {
            List<Venda> vendas = _vendaRepository.VendasAllAtivas();
            return View(vendas);
        }
        public IActionResult Inativos() {
            List<Venda> vendas = _vendaRepository.VendasAllInativas();
            return View("Index", vendas);
        }


        public IActionResult NewVenda() {
            Venda venda = new Venda();
            venda.ClientesList = _clienteRepository.GetFornecedorAtivosAll();
            venda.ProdutosList = _produtoRepository.GetAllAtivos();
            return View(venda);
        }

        [HttpPost]
        public IActionResult NewVenda(Venda venda) {
            venda.ClientesList = _clienteRepository.GetFornecedorAtivosAll();
            venda.ProdutosList = _produtoRepository.GetAllAtivos();
            try {
                if (ModelState.IsValid) {
                    _vendaRepository.NewVenda(venda);
                    TempData["Sucesso"] = "Registrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(venda);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(venda);
            }
        }

        public IActionResult Edit(int id) {
            try {
                Venda venda = _vendaRepository.GetById(id);
                venda.ClientesList = _clienteRepository.GetFornecedorAtivosAll();
                venda.ProdutosList = _produtoRepository.GetAllAtivos();
                return View(venda);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(Venda venda) {
            venda.ClientesList = _clienteRepository.GetFornecedorAtivosAll();
            venda.ProdutosList = _produtoRepository.GetAllAtivos();
            try {
                if (ModelState.IsValid) {
                    _vendaRepository.UpdateVenda(venda);
                    TempData["Sucesso"] = "Registrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(venda);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(venda);
            }
        }

        public IActionResult Inativar(int id) {
            try {
                Venda venda = _vendaRepository.GetById(id);
                venda.ClientesList = _clienteRepository.GetFornecedorAtivosAll();
                venda.ProdutosList = _produtoRepository.GetAllAtivos();
                return View(venda);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Inativar(Venda venda) {
            try {
                venda.ClientesList = _clienteRepository.GetFornecedorAtivosAll();
                venda.ProdutosList = _produtoRepository.GetAllAtivos();
                _vendaRepository.InativarVenda(venda.Id);
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
