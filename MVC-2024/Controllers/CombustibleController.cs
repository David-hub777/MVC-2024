using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_2024.Models;

namespace MVC_2024.Controllers
{
    public class CombustibleController : Controller
    {

        public Contexto Contexto { get; }

        public CombustibleController(Contexto contexto)
        {
            this.Contexto = contexto;
        }



        // GET: CombustibleController
        public ActionResult Index()
        {
            List<CombustibleModelo> lista = this.Contexto.Combustibles.ToList();
            return View(lista);
        }


        // GET: CombustibleController/Details/5
        public ActionResult Details(int id)
        {
            List<CombustibleModelo> lista = this.Contexto.Combustibles.ToList();
            // Encuentra el índice del elemento con el ID especificado
            int indice = lista.FindIndex(item => item.Id == id);
            if (indice != -1)
            {
                // Si se encontró, pasa el elemento correspondiente a la vista
                return View(lista[indice]);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: CombustibleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CombustibleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CombustibleModelo combustible)
        {
            try
            {
                Contexto.Combustibles.Add(combustible);
                Contexto.Database.EnsureCreated(); // Crea la base de datos si no existe
                //Contexto.Database.AutoSavepointsEnabled = true;//Crea un punto de guardado por si peta
                Contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CombustibleController/Edit/5
        public ActionResult Edit(int id)
        {
            CombustibleModelo elemento = this.Contexto.Combustibles.Find(id);
            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
            return View();
        }

        // POST: CombustibleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CombustibleModelo combustible)
        {
            try
            {
                CombustibleModelo elemento = this.Contexto.Combustibles.Find(id);
                if (elemento != null)
                {
                    elemento.Id = combustible.Id;
                    elemento.NomCombustible = combustible.NomCombustible;
                    
                    if (combustible.LosVehiculos != null)
                        elemento.LosVehiculos = combustible.LosVehiculos;
                    Contexto.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CombustibleController/Delete/5
        public ActionResult Delete(int id)
        {
            CombustibleModelo elemento = this.Contexto.Combustibles.Find(id);
            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: CombustibleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CombustibleModelo combustibleModelo)
        {
            try
            {
                CombustibleModelo elemento = Contexto.Combustibles.Find(id);
                if (elemento != null)
                {
                    Contexto.Combustibles.Remove(elemento);
                    Contexto.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
