using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_2024.Models;

namespace MVC_2024.Controllers
{
    public class SerieController : Controller
    {
        public Contexto Contexto { get; }

        public SerieController(Contexto contexto)
        {
            this.Contexto = contexto;
        }
        // GET: SerieController
        public ActionResult Index()
        {
            List<SerieModelo> lista = this.Contexto.Series.Include(s => s.Marca).ToList();
            return View(lista);
        }

        // GET: SerieController/Details/5
        public ActionResult Details(int id)
        {
            List<SerieModelo> lista = this.Contexto.Series.ToList();
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

        // GET: SerieController/Create
        public ActionResult Create()
        {
            ViewBag.MarcaId = new SelectList(Contexto.Marcas, "Id", "NomMarca");
            return View();
        }

        // POST: SerieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SerieModelo serie)
        {
            //List<MarcaModel> lista = this.Contexto.Marcas.ToList();
            ////MarcaModel marcaSeleccionada = lista.FirstOrDefault(m => m.Id == serie.MarcaId);
            //serie.Marca = lista.FirstOrDefault(m => m.Id == serie.MarcaId);
            ////serie.Marca = new MarcaModel();
            ////serie.Marca.NomMarca=marcaSeleccionada.NomMarca;
            ////serie.Marca.Id=marcaSeleccionada.Id;
            Contexto.Series.Add(serie);
            Contexto.Database.EnsureCreated();
            //Contexto.Database.AutoSavepointsEnabled = true;//Crea un punto de guardado por si peta
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

        // GET: SerieController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.MarcaId = new SelectList(Contexto.Marcas, "Id", "NomMarca");
            SerieModelo elemento = this.Contexto.Series.Find(id);
            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: SerieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SerieModelo serieModelo)
        {
            try
            {
                SerieModelo elemento = this.Contexto.Series.Find(id);
                if (elemento != null)
                {
                    elemento.Id = serieModelo.Id; 
                    elemento.MarcaId = serieModelo.MarcaId;
                    elemento.NomSerie = serieModelo.NomSerie;
                    if (serieModelo.Marca != null)
                        elemento.Marca = serieModelo.Marca;
                    Contexto.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: SerieController/Delete/5
        public ActionResult Delete(int id)
        {
            MarcaModel elemento = this.Contexto.Marcas.Find(id);
            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: SerieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SerieModelo serieModelo)
        {
            try
            {
                SerieModelo elemento = Contexto.Series.Find(id);
                if (elemento != null)
                {
                    Contexto.Series.Remove(elemento);
                    Contexto.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
