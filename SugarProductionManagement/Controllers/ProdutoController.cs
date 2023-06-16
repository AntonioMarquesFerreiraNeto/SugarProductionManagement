namespace SugarProductionManagement.Controllers;

using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
using SugarProductionManagement.Models.Enums;
using SugarProductionManagement.Repository;

[PagUserAutenticado]
public class ProdutoController : Controller {
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository) {
        _produtoRepository = produtoRepository;
    }

    public IActionResult Index() {
        var produtos = _produtoRepository.GetAllAtivos();
        return View(produtos);
    }
    public IActionResult Inativos() {
        var produtos = _produtoRepository.GetAllInativos();
        return View("Index", produtos);
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Produto produto) {
        try {
            if (ModelState.IsValid) {
                _produtoRepository.Add(produto);
                TempData["Sucesso"] = "Registrado com sucesso!";
                return RedirectToAction("Index");
            }
            return View(produto);
        }
        catch (Exception error) {
            TempData["Error"] = error.Message;
            return View(produto);
        }
    }

    public IActionResult Edit(int id) {
        try {
            var produto = _produtoRepository.GetById(id);
            return View(produto);
        }
        catch (Exception error) {
            TempData["Error"] = error.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Edit(Produto produto) {
        if (ModelState.IsValid) {
            _produtoRepository.Update(produto);
            TempData["Sucesso"] = "Atualizado com sucesso!";
            return RedirectToAction("Index");
        }
        return View(produto);
    }

    public IActionResult Inativar(int id) {
        try {
            var produto = _produtoRepository.GetById(id);
            return View(produto);
        }
        catch (Exception error) {
            TempData["Error"] = error.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Inativar(Produto produto) {
        try {
            _produtoRepository.Inativar(produto.Id);
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
            var produto = _produtoRepository.GetById(id);
            return View(produto);
        }
        catch (Exception error) {
            TempData["Error"] = error.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Ativar(Produto produto) {
        try {
            _produtoRepository.Ativar(produto.Id);
            TempData["Sucesso"] = "Ativado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception error) {
            TempData["error"] = error.Message;
            return RedirectToAction("Index");
        }
    }

    public IActionResult RelatorioExcelAtivos() {
        try {
            List<Produto> produtos = _produtoRepository.GetAllAtivos();
            if (produtos == null || produtos.Count == 0) {
                TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                return RedirectToAction("Index");
            }
            using (var folhaBook = new XLWorkbook()) {
                var folha = folhaBook.Worksheets.Add("Sample sheet");
                folha.Cell(1, "A").Value = "ID";
                folha.Cell(1, "B").Value = "Nome";
                folha.Cell(1, "C").Value = "Preço";
                folha.Cell(1, "D").Value = "Qt. em estoque";
                folha.Cell(1, "E").Value = "Qt. reservada";
                folha.Cell(1, "F").Value = "Qt. produções";
                folha.Cell(1, "G").Value = "Tot. de vendas";

                //Definindo o tamanho e posição de cada coluna da planilha.
                var col1 = folha.Column("A");
                var col2 = folha.Column("B");
                var col3 = folha.Column("C");
                var col4 = folha.Column("D");
                var col5 = folha.Column("E");
                var col6 = folha.Column("F");
                var col7 = folha.Column("G");

                col1.Width = 10;
                col2.Width = 35;
                col3.Width = 20;
                col4.Width = 20;
                col5.Width = 20;
                col6.Width = 20;

                col1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col6.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col7.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                //Montar a tabela com os dados na lista. 
                foreach (var item in produtos) {
                    folha.Cell(produtos.IndexOf(item) + 2, "A").Value = item.Id;
                    folha.Cell(produtos.IndexOf(item) + 2, "B").Value = item.Nome;
                    folha.Cell(produtos.IndexOf(item) + 2, "C").Value = item.Preco!.Value.ToString("C2");
                    folha.Cell(produtos.IndexOf(item) + 2, "D").Value = item.QtEstoque;
                    folha.Cell(produtos.IndexOf(item) + 2, "E").Value = item.QtReservada;
                    folha.Cell(produtos.IndexOf(item) + 2, "F").Value = item.ListProducao!.Count(x => x.Status == StatusProducao.Ativo);
                    folha.Cell(produtos.IndexOf(item) + 2, "G").Value = item.ListVendas!.Count(x => x.VendasStatus == VendasStatus.Ativa);
                }

                //Finalizando o excel e realizando o upload para o cliente. 
                using (MemoryStream stream = new MemoryStream()) {
                    folhaBook.SaveAs(stream);
                    string nomeArquivo = "Sugar Production Management - Produtos ativos.xlsx";
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
                }
            }
        }
        catch (Exception error) {
            TempData["Error"] = error.Message;
            return RedirectToAction("Index");
        }
    }

    public IActionResult RelatorioExcelInativos() {
        try {
            List<Produto> produtos = _produtoRepository.GetAllInativos();
            if (produtos == null || produtos.Count == 0) {
                TempData["Error"] = "Desculpe, não conseguimos encontrar nenhum registro!";
                return RedirectToAction("Index");
            }
            using (var folhaBook = new XLWorkbook()) {
                var folha = folhaBook.Worksheets.Add("Sample sheet");
                folha.Cell(1, "A").Value = "ID";
                folha.Cell(1, "B").Value = "Nome";
                folha.Cell(1, "C").Value = "Preço unitário";
                folha.Cell(1, "D").Value = "Qt. em estoque";
                folha.Cell(1, "E").Value = "Qt. reservada";
                folha.Cell(1, "F").Value = "Qt. produções";
                folha.Cell(1, "G").Value = "Tot. de vendas";

                //Definindo o tamanho e posição de cada coluna da planilha.
                var col1 = folha.Column("A");
                var col2 = folha.Column("B");
                var col3 = folha.Column("C");
                var col4 = folha.Column("D");
                var col5 = folha.Column("E");
                var col6 = folha.Column("F");
                var col7 = folha.Column("G");

                col1.Width = 10;
                col2.Width = 35;
                col3.Width = 20;
                col4.Width = 20;
                col5.Width = 20;
                col6.Width = 20;

                col1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col2.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col6.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                col7.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                //Montar a tabela com os dados na lista. 
                foreach (var item in produtos) {
                    folha.Cell(produtos.IndexOf(item) + 2, "A").Value = item.Id;
                    folha.Cell(produtos.IndexOf(item) + 2, "B").Value = item.Nome;
                    folha.Cell(produtos.IndexOf(item) + 2, "C").Value = item.Preco!.Value.ToString("C2");
                    folha.Cell(produtos.IndexOf(item) + 2, "D").Value = item.QtEstoque;
                    folha.Cell(produtos.IndexOf(item) + 2, "E").Value = item.QtReservada;
                    folha.Cell(produtos.IndexOf(item) + 2, "F").Value = item.ListProducao!.Count;
                    folha.Cell(produtos.IndexOf(item) + 2, "G").Value = item.ListVendas!.Count;
                }

                //Finalizando o excel e realizando o upload para o cliente. 
                using (MemoryStream stream = new MemoryStream()) {
                    folhaBook.SaveAs(stream);
                    string nomeArquivo = "Sugar Production Management - Produtos inativos.xlsx";
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

