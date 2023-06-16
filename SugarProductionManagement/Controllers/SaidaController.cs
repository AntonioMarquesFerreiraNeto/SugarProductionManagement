using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;
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
            try {
                if (string.IsNullOrEmpty(id.ToString())) {
                    return RedirectToAction("Index");
                }
                ViewBag.Id = id;
                List<Saida> saidas = _saidaRepository.ListSaidasVendaById(id);
                return View(saidas);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Inativos(int id) {
            List<Saida> saidas = _saidaRepository.ListSaidasVendaById(id);
            return View("Index", saidas);
        }

        public IActionResult NewSaida(int id) {
            try {
                ViewBag.Id = id;
                Saida saida = new Saida();
                Venda venda = new Venda();
                saida.Venda = _vendaRepository.GetById(id);
                saida.ListProducoesProduto = _saidaRepository.ListProducoesByProdutoId(saida.Venda.ProdutoId!.Value);
                saida.ProdutoId = saida.Venda.ProdutoId;
                return View(saida);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult NewSaida(Saida saida) {
            ViewBag.Id = saida.VendaId;
            saida.Venda = _vendaRepository.GetById(saida.VendaId!.Value);
            saida.ListProducoesProduto = _saidaRepository.ListProducoesByProdutoId(saida.Venda.ProdutoId!.Value);
            try {
                if (!VendaSaidasList.Any()) {
                    TempData["Error"] = "Nenhuma produção selecionada!";
                    return View(saida);
                }
                TempData["Sucesso"] = "Registrada com sucesso!";
                _saidaRepository.CreateSaida(saida, VendaSaidasList);
                List<Saida> saidas = _saidaRepository.ListSaidasVendaById(saida.VendaId.Value);
                return View("Lancamentos", saidas);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(saida);
            }
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

        public IActionResult InativarSaida(int id) {
            try {
                Saida saida = _saidaRepository.GetById(id);
                return View(saida);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult InativarSaida(Saida saida) {
            ViewBag.Id = saida.VendaId;
            try {
                int vendaId = saida.VendaId!.Value;
                _saidaRepository.InativarSaida(saida.Id);
                TempData["Sucesso"] = "Inativado com sucesso!";

                List<Saida> saidas = _saidaRepository.ListSaidasVendaById(vendaId);
                return View("Lancamentos", saidas);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult RelatorioPicking(int id) {
            try {
                List<Saida> saidas = _saidaRepository.ListSaidasVendaById(id);
                if (saidas == null || saidas.Count == 0) {
                    TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                    return RedirectToAction("Lancamentos", saidas);
                }

                using (var folhaBook = new XLWorkbook()) {
                    var folha = folhaBook.Worksheets.Add("Sample sheet");

                    folha.Cell(1, "A").Value = "ID";
                    folha.Cell(1, "B").Value = "Cod. carregamento";
                    folha.Cell(1, "C").Value = "Qt. unitárias";
                    folha.Cell(1, "D").Value = "Funcionário";
                    folha.Cell(1, "E").Value = "Situação";

                    var col1 = folha.Column("A");
                    var col2 = folha.Column("B");
                    var col3 = folha.Column("C");
                    var col4 = folha.Column("D");
                    var col5 = folha.Column("E");

                    col1.Width = 10;
                    col2.Width = 20;
                    col3.Width = 35;
                    col4.Width = 20;
                    col5.Width = 20;
                    col1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    foreach (var item in saidas) {
                        folha.Cell(saidas.IndexOf(item) + 2, "A").Value = item.Id;
                        folha.Cell(saidas.IndexOf(item) + 2, "B").Value = item.CodCarregamento;
                        folha.Cell(saidas.IndexOf(item) + 2, "C").Value = item.QtSaidaTotal;
                        folha.Cell(saidas.IndexOf(item) + 2, "D").Value = item.Funcionario!.Name;
                        folha.Cell(saidas.IndexOf(item) + 2, "E").Value = (item.SaidaStatus == SaidaStatus.Ativo) ? "Ativa" : "Inativa";
                    }

                    //Finalizando o excel e realizando o upload para o cliente. 
                    using (MemoryStream stream = new MemoryStream()) {
                        folhaBook.SaveAs(stream);
                        string nomeArquivo = "Sugar Production Management - Saidas.xlsx";
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
                    }
                }
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult RelatorioExecelAtivos() {
            try {
                List<Venda> vendas = _vendaRepository.VendasAllAtivas();
                if (vendas.Count == 0 || vendas == null) {
                    TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                    return RedirectToAction("Index");
                }

                using (var folhaBook = new XLWorkbook()) {
                    var folha = folhaBook.Worksheets.Add("Sample sheet");

                    folha.Cell(1, "A").Value = "ID";
                    folha.Cell(1, "B").Value = "Cod. pedido de vendas";
                    folha.Cell(1, "C").Value = "Cliente";
                    folha.Cell(1, "D").Value = "Produto";
                    folha.Cell(1, "E").Value = "Func. realizador da venda";
                    folha.Cell(1, "F").Value = "Qt. vendida";
                    folha.Cell(1, "G").Value = "Qt. Entregue";
                    folha.Cell(1, "H").Value = "Qt. Entregar";
                    folha.Cell(1, "I").Value = "Data de venda";

                    var col1 = folha.Column("A");
                    var col2 = folha.Column("B");
                    var col3 = folha.Column("C");
                    var col4 = folha.Column("D");
                    var col5 = folha.Column("E");
                    var col6 = folha.Column("F");
                    var col7 = folha.Column("G");
                    var col8 = folha.Column("H");
                    var col9 = folha.Column("I");

                    col1.Width = 10;
                    col2.Width = 30;
                    col3.Width = 35;
                    col4.Width = 20;
                    col5.Width = 35;
                    col6.Width = 20;
                    col7.Width = 20;
                    col8.Width = 20;
                    col9.Width = 20;

                    col1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col6.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col7.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col8.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col9.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    foreach (var item in vendas) {
                        folha.Cell(vendas.IndexOf(item) + 2, "A").Value = item.Id;
                        folha.Cell(vendas.IndexOf(item) + 2, "B").Value = item.CodPedidoVenda;
                        folha.Cell(vendas.IndexOf(item) + 2, "C").Value = item.Cliente!.NomeFantasia;
                        folha.Cell(vendas.IndexOf(item) + 2, "D").Value = item.Produto!.Nome;
                        folha.Cell(vendas.IndexOf(item) + 2, "E").Value = item.Funcionario!.Name;
                        folha.Cell(vendas.IndexOf(item) + 2, "F").Value = item.QtVendida;
                        folha.Cell(vendas.IndexOf(item) + 2, "G").Value = item.QtEntregue;
                        folha.Cell(vendas.IndexOf(item) + 2, "H").Value = (item.QtVendida - item.QtEntregue);
                        folha.Cell(vendas.IndexOf(item) + 2, "I").Value = item.DataVenda!.Value.ToString("dd/MM/yyyy");
                    }

                    using (MemoryStream stream = new MemoryStream()) {
                        folhaBook.SaveAs(stream);
                        string nomeArquivo = "Sugar Production Management - Vendas ativas.xlsx";
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
                    }
                }
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
