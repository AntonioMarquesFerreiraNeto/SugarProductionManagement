using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    [PagRfInventario]
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

        public IActionResult RelatorioExcelAtivos() {
            try {
                List<Inventario> inventarios = _inventarioRepository.GetAtivosAll();
                if (inventarios == null || inventarios.Count == 0) {
                    TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                    return RedirectToAction("Index");
                }

                using (var folhaBook = new XLWorkbook()) {
                    var folha = folhaBook.Worksheets.Add("Sample sheet");

                    folha.Cell(1, "A").Value = "ID";
                    folha.Cell(1, "B").Value = "Produto";
                    folha.Cell(1, "C").Value = "Funcinário que inventáriou";
                    folha.Cell(1, "D").Value = "Quantidade de baixa";
                    folha.Cell(1, "E").Value = "Data de entrada";

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

                    foreach (var item in inventarios) {
                        folha.Cell(inventarios.IndexOf(item) + 2, "A").Value = item.Id;
                        folha.Cell(inventarios.IndexOf(item) + 2, "B").Value = item.Producao!.Produto!.Nome;
                        folha.Cell(inventarios.IndexOf(item) + 2, "C").Value = item.Funcionario!.Name;
                        folha.Cell(inventarios.IndexOf(item) + 2, "D").Value = item.QtBaixa;
                        folha.Cell(inventarios.IndexOf(item) + 2, "E").Value = item.DataDeInventario!.Value.ToString("dd/MM/yyyy");
                    }

                    //Finalizando o excel e realizando o upload para o cliente. 
                    using (MemoryStream stream = new MemoryStream()) {
                        folhaBook.SaveAs(stream);
                        string nomeArquivo = "Sugar Production Management - Inventários.xlsx";
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
