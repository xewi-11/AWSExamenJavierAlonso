using AWSExamenJavierAlonso.Models;
using AWSExamenJavierAlonso.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AWSExamenJavierAlonso.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }

        public IActionResult CreateZapatilla()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateZapatilla(Zapatilla zapatilla, IFormFile formFile)
        {
            if (zapatilla == null || formFile == null)
            {
                return View();
            }

            await this.repo.InsertZapatillaAsync(zapatilla, formFile);
            return RedirectToAction("Index");
        }
    }
}
