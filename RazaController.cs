using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class RazaController : Controller
    {
        // GET: RazaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RazaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RazaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RazaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RazaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RazaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RazaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RazaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
