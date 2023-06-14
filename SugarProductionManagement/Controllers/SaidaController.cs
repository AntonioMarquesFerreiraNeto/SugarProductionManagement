using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    [PagUserAutenticado]
    public class SaidaController : Controller {

        public static List<VendaSaidas> VendaSaidasList = new List<VendaSaidas>();

        private readonly IVendaRepository _vendaRepository;
        private readonly ISaidaRepository _saidaRepository;
        private readonly IProducaoRepository _producaoRepository;

        public SaidaController(IVendaRepository vendaRepository, ISaidaRepository saidaRepository, IProducaoRepository producaoRepository) {
            _vendaRepository = vendaRepository;
            _saidaRepository = saidaRepository;
            _producaoRepository = producaoRepository;
        }

        public IActionResult Index() {
            List<Venda> vendas = _vendaRepository.VendasAllAtivas();
            return View(vendas);
        }

        public IActionResult Lancamentos(int id) {
            ViewBag.Id = id;
            List<Saida> saidas = _saidaRepository.ListSaidasVendaById(id);
            return View(saidas);
        }

        public IActionResult Inativos(int id) {
            List<Saida> saidas = _saidaRepository.ListSaidasVendaById(id);
            return View("Index", saidas);
        }

        public IActionResult NewSaida(int id) {
            ViewBag.Id = id;
            Saida saida = new Saida();
            Venda venda = new Venda();
            saida.Venda = _vendaRepository.GetById(id);
            saida.ListProducoesProduto = _saidaRepository.ListProducoesByProdutoId(saida.Venda.ProdutoId!.Value);
            return View(saida);
        }

        
        public IActionResult ReturnList() {
            return PartialView("_SaidasPartial", VendaSaidasList);
        }
        public void LimparLista() {
            VendaSaidasList.Clear();
        }

        public ActionResult AddSaidaTemp(int id, int qt) {
            Producao producaoSelect = _producaoRepository.GetById(id);
            if (producaoSelect.QtEstoque < qt) {
                return PartialView("_SaidasPartial", VendaSaidasList);
            }
            else {
                AddSaida(producaoSelect, qt);
                return PartialView("_SaidasPartial", VendaSaidasList);
            }
        }
        public void AddSaida(Producao producao, int qt) {
            VendaSaidas vendaSaidas = new VendaSaidas();
            vendaSaidas.QtSaidaLote = qt;
            vendaSaidas.Producao = producao;
            if (!VendaSaidasList.Any(x => x.Producao!.Id == producao.Id)) {
                VendaSaidasList.Add(vendaSaidas);
            }
        }

        public ActionResult RemoveSaidaTemp(int id) {
            
            VendaSaidasList.RemoveAll(x => x.Producao!.Id == id);
            return PartialView("_SaidasPartial", VendaSaidasList);
        }
    }
}
