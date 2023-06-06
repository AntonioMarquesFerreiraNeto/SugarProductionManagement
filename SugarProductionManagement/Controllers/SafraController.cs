using Microsoft.AspNetCore.Mvc;
using SugarProductionManagement.Models;
using SugarProductionManagement.Repository;

namespace SugarProductionManagement.Controllers {
    public class SafraController : Controller {

        private readonly ISafraRepository _safraRepository;

        public SafraController(ISafraRepository safraRepository) {
            _safraRepository = safraRepository;
        }

        public IActionResult Index() {
            ViewData["Title"] = "Safra";
            List<Safra> safra = _safraRepository.GetAllSafras();
            return View(safra);
        }

        public IActionResult NewSafra() {
            ViewData["Title"] = "Safra";
            Safra safra = new Safra();
            safra.CodSafra = safra.ReturnCodSafra();
            return View(safra);
        }

        [HttpPost]
        public IActionResult NewSafra(Safra safra) {
            ViewData["Title"] = "Safra";
            try {
                if (ModelState.IsValid) {
                    if (safra.DataAberturaSafra!.Value.Year > DateTime.Now.Year) {
                        TempData["Error"] = "A data de abertura não pode ser maior que o ano atual!";
                        return View(safra);
                    }
                    _safraRepository.CreateSafra(safra);
                    TempData["Sucesso"] = "Adicionado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(safra);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return View(safra);
            }
        }

        public IActionResult DeletarSafra(int? id) {
            ViewData["Title"] = "Safra";
            try {
                Safra safra = _safraRepository.GetSafraById(id);
                return View(safra);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DeletarSafra(Safra safra) {
            ViewData["Title"] = "Safra";
            try {
                _safraRepository.DeleteSafra(safra.Id);
                TempData["Sucesso"] = "Deletado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult FecharSafra(int? id) {
            ViewData["Title"] = "Safra";
            try {
                Safra safra = _safraRepository.GetSafraById(id);
                return View(safra);
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult FecharSafra(Safra safra) {
            ViewData["Title"] = "Safra";
            try {
                _safraRepository.FecharSafra(safra.Id);
                TempData["Sucesso"] = "Safra fechada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error) {
                TempData["Error"] = error.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
