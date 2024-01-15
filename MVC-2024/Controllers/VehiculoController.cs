using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_2024.Models;

namespace MVC_2024.Controllers
{
    public class VehiculoController : Controller
    {
        public Contexto Contexto { get; }

        public VehiculoController(Contexto contexto)
        {
            this.Contexto = contexto;
        }
        // GET: VehiculoController
        public ActionResult Index()
        {
            List<VehiculoModelo> lista = this.Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca).ToList();
            return View(lista);
        }

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            //VehiculoModelo vehiculoModelo = this.Contexto.Vehiculos.Find(id);
            VehiculoModelo vehiculoModelo = this.Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca).FirstOrDefault(v => v.Id == id);
            if (id != -1)
            {
                return View(vehiculoModelo);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            ViewBag.SerieId = new SelectList(Contexto.Series, "Id", "NomSerie");
            return View();
        }

        // POST: VehiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehiculoModelo vehiculoModelo)
        {
            Contexto.Vehiculos.Add(vehiculoModelo);
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

        // GET: VehiculoController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SerieId = new SelectList(Contexto.Series, "Id", "NomSerie");
            VehiculoModelo elemento = this.Contexto.Vehiculos.Find(id);
            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: VehiculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehiculoModelo vehiculoModelo)
        {
            try
            {
                VehiculoModelo elemento = this.Contexto.Vehiculos.Find(id);
                if (elemento != null)
                {
                    cambioVehiculo(vehiculoModelo, elemento);
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

        private static void cambioVehiculo(VehiculoModelo vehiculoModelo, VehiculoModelo elemento)
        {
            elemento.Id = vehiculoModelo.Id;
            elemento.Matricula = vehiculoModelo.Matricula;
            elemento.Color = vehiculoModelo.Color;
            elemento.SerieId = vehiculoModelo.SerieId;
            if (vehiculoModelo.Serie != null)
                elemento.Serie = vehiculoModelo.Serie;
        }

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.marcas = this.Contexto.Marcas.ToList();
            ViewBag.series = this.Contexto.Series.ToList();
            VehiculoModelo elemento = this.Contexto.Vehiculos.Find(id);
            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VehiculoModelo vehiculoModelo)
        {
            try
            {
                VehiculoModelo elemento = Contexto.Vehiculos.Find(id);
                if (elemento != null)
                {
                    Contexto.Vehiculos.Remove(elemento);
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
