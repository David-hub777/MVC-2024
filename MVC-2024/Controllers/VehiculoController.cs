using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_2024.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVC_2024.Controllers
{
    public class VehiculoController : Controller
    {
        public class VehiculoTotal
        {
            public string NomMarca { get; set; }
            public string NomSerie { get; set; }
            public string Matricula { get; set; }
            public string Color { get; set; }
            //public VehiculoTotal() {// BORRAR ESTE CONSTRUCTOR O NO FUNCIONA LA VISTA DE LISTADO2 DE VEHICULOS TOTALES PERO SI FUNCIONA LA VISTA DE LISTADO DE VEHICULOS Y NO SE PORQUE . @MENSAJE_ESCRITO_POR_COPILOT
            //    NomMarca = "";
            //    NomSerie = "";
            //    Matricula = "";
            //    Color = "";
            //}
        }
        public Contexto Contexto { get; }

        public VehiculoController(Contexto contexto)
        {
            this.Contexto = contexto;
        }
        // GET: VehiculoController => IndexVehiculoTotal
        public ActionResult Listado2()
        {
            List<VehiculoTotal> lista = this.Contexto.ViewTotal.ToList();
            return View(lista);
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
            VehiculoModelo vehiculoModelo = this.Contexto.Vehiculos.Include("Serie.Marca").FirstOrDefault(v => v.Id == id);
            // .Include(s => s.Serie).Include(m => m.Serie.Marca).
            if (id != -1)
            {
                return View(vehiculoModelo);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: VehiculoController/Details2/matriculaElegida
        public ActionResult Details2(String id)
        {
            if (id != null)
            {
                VehiculoTotal vehiculo = Contexto.ViewTotal.FirstOrDefault(v => v.Matricula == id);
                return View(vehiculo);
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
        // GET: VehiculoController/Busqueda
        public ActionResult Busqueda(string matriculaVehiculo = "")
        {
            ViewBag.buscar = matriculaVehiculo;
            var lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
                        where (v.Matricula.Contains(matriculaVehiculo))
                        select v;
            if (matriculaVehiculo == null)
            {
                lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
                            select v;
            }
            //else
            //{
            //    lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
            //            where (v.Matricula.Contains(matriculaVehiculo))
            //            select v;
            //}
            return View(lista);
        }

        // GET: VehiculoController/Busqueda2
        public ActionResult Busqueda2(String matriculaVehiculo = "")
        {
            ViewBag.listaMatriculas = new SelectList(Contexto.Vehiculos,"Matricula", "Matricula" , matriculaVehiculo);
            var lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
                        where (v.Matricula.Equals(matriculaVehiculo))
                        select v;
            if (matriculaVehiculo == "")
            {
                lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
                        select v;
            }
            return View(lista);
        }


        // GET: VehiculoController/DesplegableComplejo
        public ActionResult DesplegableComplejo(String marcaVehiculo = "", String serieVehiculo = "")
        {
            ViewBag.listaMarcas = new SelectList(this.Contexto.Marcas, "NomMarca", "NomMarca", marcaVehiculo);
            var listaSeries = from v in Contexto.Series.Include(s => s.Marca)
                        where (v.Marca.NomMarca.Equals(marcaVehiculo))
                        select v;
            //if (marcaVehiculo == "" && serieVehiculo != "")
            //{
            //    listaSeries = from v in Contexto.Series.Include(s => s.Marca)
            //            select v;
            //}
            ViewBag.listaSeries = new SelectList(listaSeries, "NomSerie", "NomSerie" , serieVehiculo);
            var lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
                        where (v.Serie.NomSerie.Equals(serieVehiculo))
                        select v;
            if (serieVehiculo == "")
            {
                lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca)
                            select v;
            }
            return View(lista);
        }

        // GET: VehiculoController/Desplegable
        public ActionResult Seleccion(int marcaId = 1, int serieId = 0)
        {
            ViewBag.lasMarcas = new SelectList(this.Contexto.Marcas, "Id", "NomMarca", marcaId);
            ViewBag.lasSeries = new SelectList(this.Contexto.Series.Where(s => s.MarcaId == marcaId), "Id", "NomSerie", serieId);
            List<VehiculoModelo> listaVehiculos = this.Contexto.Vehiculos.Include(s => s.Serie).Where(v => v.SerieId == serieId).Include(m => m.Serie.Marca).Where(v => v.Serie.MarcaId == marcaId).ToList();
            return View(listaVehiculos);
        }
    }
}
