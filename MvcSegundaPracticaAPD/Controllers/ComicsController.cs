using Microsoft.AspNetCore.Mvc;
using MvcSegundaPracticaAPD.Models;
using MvcSegundaPracticaAPD.Repositories;

namespace MvcSegundaPracticaAPD.Controllers
{
    public class ComicsController : Controller
    {
        RepositoryComics repo;
        public ComicsController()
        {
            this.repo = new RepositoryComics();
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }
        public IActionResult Details(int id)
        {
            Comic comic = this.repo.GetComic_id(id);
            return View(comic);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comic comic)
        {
            this.repo.CreateComic(comic);
            return RedirectToAction("Index");
        }
    }
}
