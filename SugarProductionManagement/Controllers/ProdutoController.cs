namespace SugarProductionManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Filter;
using SugarProductionManagement.Models;
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
            return View(produto);
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
            return View(produto);
        }
    }
}

