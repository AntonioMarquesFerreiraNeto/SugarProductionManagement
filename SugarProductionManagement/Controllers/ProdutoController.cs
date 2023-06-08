namespace SugarProductionManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

public class ProdutoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public IActionResult Index()
    {
        var produtos = _produtoRepository.GetAll();
        return View(produtos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Produto produto)
    {
        if (ModelState.IsValid)
        {
            _produtoRepository.Add(produto);
            return RedirectToAction("Index");
        }
        return View(produto);
    }

    public IActionResult Edit(int id)
    {
        var produto = _produtoRepository.GetById(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    [HttpPost]
    public IActionResult Edit(Produto produto)
    {
        if (ModelState.IsValid)
        {
            _produtoRepository.Update(produto);
            return RedirectToAction("Index");
        }
        return View(produto);
    }

    public IActionResult Delete(int id)
    {
        var produto = _produtoRepository.GetById(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _produtoRepository.Delete(id);
        return RedirectToAction("Index");
    }
}

