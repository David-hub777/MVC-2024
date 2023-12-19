using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_2024.Models;

namespace MVC_2024.Controllers
{
    public class MarcaController : Controller
    {
        public Contexto Contexto { get; }

        public MarcaController(Contexto contexto)
        {
            this.Contexto = contexto;
        }
        // GET: MarcaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MarcaController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: MarcaController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: MarcaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(marcaModel marca)
        {
            Contexto.Marcas.Add(marca);
            Contexto.Database.EnsureCreated();
            //Contexto.Database.AutoSavepointsEnabled = true;//Que hace esto?
            Contexto.SaveChanges();
            
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarcaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarcaController/Edit/5
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

        // GET: MarcaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarcaController/Delete/5
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
