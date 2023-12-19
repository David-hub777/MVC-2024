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
            List<MarcaModel> lista = this.Contexto.Marcas.ToList();
            return View(lista);
        }

        // GET: MarcaController/Details/5
        public ActionResult Details(int id)
        {
            List<MarcaModel> lista = this.Contexto.Marcas.ToList();
            // Encuentra el índice del elemento con el ID especificado
            int indice = lista.FindIndex(item => item.Id == id);

            // Verifica si se encontró el elemento
            if (indice != -1)
            {
                // Si se encontró, pasa el elemento correspondiente a la vista
                return View(lista[indice]);
            }
            else
            {
                // Si no se encontró, puedes manejarlo de la manera que consideres apropiada
                // Por ejemplo, puedes redirigir a una página de error.
                return RedirectToAction("Error");
            }
        }

        // GET: MarcaController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: MarcaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MarcaModel marca)
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
