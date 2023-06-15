using ClosedXML.Excel;
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

        public IActionResult RelatorioExecelInativos() {
            try {
                List<Venda> vendas = _vendaRepository.VendasAllInativas();
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
                        string nomeArquivo = "Sugar Production Management - Vendas inativas.xlsx";
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
