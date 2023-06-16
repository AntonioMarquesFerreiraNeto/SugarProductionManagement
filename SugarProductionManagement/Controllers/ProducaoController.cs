using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Migrations;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    [PagProducaoSafra]
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

        public IActionResult RelatoriosExcelAtivos() {
            try {
                List<Producao> producoes = _producaoRepository.GetProducaoAtivos();
                if (producoes == null || producoes.Count == 0) {
                    TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                    return RedirectToAction("Index");
                }

                using (var folhaBook = new XLWorkbook()) {
                    var folha = folhaBook.Worksheets.Add("Sample sheet");
                    folha.Cell(1, "A").Value = "ID";
                    folha.Cell(1, "B").Value = "Produto";
                    folha.Cell(1, "C").Value = "Safra";
                    folha.Cell(1, "D").Value = "Produção"; 
                    folha.Cell(1, "E").Value = "Estoque"; 
                    folha.Cell(1, "F").Value = "Lote";
                    folha.Cell(1, "G").Value = "Data de produção";
                    folha.Cell(1, "H").Value = "Data de validade";

                    var col1 = folha.Column("A");
                    var col2 = folha.Column("B");
                    var col3 = folha.Column("C");
                    var col4 = folha.Column("D");
                    var col5 = folha.Column("E");
                    var col6 = folha.Column("F");
                    var col7 = folha.Column("G");
                    var col8 = folha.Column("H");

                    col1.Width = 10;
                    col2.Width = 20;
                    col3.Width = 20;
                    col4.Width = 20;
                    col5.Width = 20;
                    col6.Width = 20;
                    col7.Width = 20;
                    col8.Width = 20;
                    col1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col6.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col7.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col8.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    foreach (var item in producoes) {
                        folha.Cell(producoes.IndexOf(item) + 2, "A").Value = item.Id;
                        folha.Cell(producoes.IndexOf(item) + 2, "B").Value = item.Produto!.Nome;
                        folha.Cell(producoes.IndexOf(item) + 2, "C").Value = $"Safra {item.Safra!.YearSafra}";
                        folha.Cell(producoes.IndexOf(item) + 2, "D").Value = item.QtProduzida;
                        folha.Cell(producoes.IndexOf(item) + 2, "E").Value = item.QtEstoque;
                        folha.Cell(producoes.IndexOf(item) + 2, "F").Value = item.Lote;
                        folha.Cell(producoes.IndexOf(item) + 2, "G").Value = item.DataProducao!.Value.ToString("dd/MM/yyyy");
                        folha.Cell(producoes.IndexOf(item) + 2, "H").Value = item.DataValidade!.Value.ToString("dd/MM/yyyy");
                    }

                    using (MemoryStream stream = new MemoryStream()) {
                        folhaBook.SaveAs(stream);
                        string nomeArquivo = "Sugar Production Management - Produções ativas.xlsx";
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
                    }
                }
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult RelatoriosExcelInativos() {
            try {
                List<Producao> producoes = _producaoRepository.GetProducaoInativos();
                if (producoes == null || producoes.Count == 0) {
                    TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                    return RedirectToAction("Index");
                }

                using (var folhaBook = new XLWorkbook()) {
                    var folha = folhaBook.Worksheets.Add("Sample sheet");
                    folha.Cell(1, "A").Value = "ID";
                    folha.Cell(1, "B").Value = "Produto";
                    folha.Cell(1, "C").Value = "Safra";
                    folha.Cell(1, "D").Value = "Produção";
                    folha.Cell(1, "E").Value = "Estoque";
                    folha.Cell(1, "F").Value = "Lote";
                    folha.Cell(1, "G").Value = "Data de produção";
                    folha.Cell(1, "H").Value = "Data de validade";

                    var col1 = folha.Column("A");
                    var col2 = folha.Column("B");
                    var col3 = folha.Column("C");
                    var col4 = folha.Column("D");
                    var col5 = folha.Column("E");
                    var col6 = folha.Column("F");
                    var col7 = folha.Column("G");
                    var col8 = folha.Column("H");

                    col1.Width = 10;
                    col2.Width = 20;
                    col3.Width = 20;
                    col4.Width = 20;
                    col5.Width = 20;
                    col6.Width = 20;
                    col7.Width = 20;
                    col8.Width = 20;
                    col1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col6.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col7.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    col8.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    foreach (var item in producoes) {
                        folha.Cell(producoes.IndexOf(item) + 2, "A").Value = item.Id;
                        folha.Cell(producoes.IndexOf(item) + 2, "B").Value = item.Produto!.Nome;
                        folha.Cell(producoes.IndexOf(item) + 2, "C").Value = $"Safra {item.Safra!.YearSafra}";
                        folha.Cell(producoes.IndexOf(item) + 2, "D").Value = item.QtProduzida;
                        folha.Cell(producoes.IndexOf(item) + 2, "E").Value = item.QtEstoque;
                        folha.Cell(producoes.IndexOf(item) + 2, "F").Value = item.Lote;
                        folha.Cell(producoes.IndexOf(item) + 2, "G").Value = item.DataProducao!.Value.ToString("dd/MM/yyyy");
                        folha.Cell(producoes.IndexOf(item) + 2, "H").Value = item.DataValidade!.Value.ToString("dd/MM/yyyy");
                    }

                    using (MemoryStream stream = new MemoryStream()) {
                        folhaBook.SaveAs(stream);
                        string nomeArquivo = "Sugar Production Management - Produções inativas.xlsx";
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
