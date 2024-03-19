using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
            // List<VehiculoTotal> lista = this.Contexto.ViewTotal.ToList();
            var lista = this.Contexto.ViewTotal.FromSql($"SELECT dbo.Marcas.NomMarca, dbo.Series.NomSerie, dbo.Vehiculos.Matricula, dbo.Vehiculos.Color\r\n\tFROM     dbo.Marcas INNER JOIN\r\n                  dbo.Series ON dbo.Marcas.Id = dbo.Series.MarcaId INNER JOIN\r\n                  dbo.Vehiculos ON dbo.Series.Id = dbo.Vehiculos.SerieId");
            //var lista = this.Contexto.ViewTotal.FromSql($"EXECUTE getSeriesVehiculos");
            return View(lista);
        }

        // GET: VehiculoController => IndexVehiculoTotal
        public ActionResult Listado3(string color = "%")
        {
            //primera parte del ejercicio
            var lista = this.Contexto.ViewTotal.FromSql($"EXECUTE getSeriesVehiculos");

            //segunda parte del ejercicio
            ViewBag.elcolor = new SelectList(this.Contexto.Vehiculos.Select(v => v.Color ).Distinct().ToList(), color);
            ViewBag.loscolores = new SelectList(this.Contexto.Vehiculos.Select(v => new { Color = v.Color }).Distinct().ToList(), "Color", "Color");// Agustin Mode
            var color2 = "%";// "%" Significa que EMPLEA TODOS LOS COLORES
            if (color == null){color = "%";}//Este es EXTRA: Si no se selecciona ningun color, se seleccionan todos
            var lista2 = this.Contexto.ViewTotal.FromSql($"EXECUTE getVehiculosPorColor {color}");// {color}, {color2}
            return View(lista2);
        }
        // COMO LISTADO 3 
        public ActionResult Listado4(string color = "%")
        {
            var elColor = new SqlParameter("@ColorSel", color);
            ViewBag.elcolor = new SelectList(this.Contexto.Vehiculos.Select(v => v.Color).Distinct().ToList(), color);
            //ViewBag.loscolores = new SelectList(this.Contexto.Vehiculos.Select(v => new { Color = v.Color }).Distinct().ToList(), "Color", "Color");// Agustin Mode
            if (color == null) { color = "%"; }//Este es EXTRA: Si no se selecciona ningun color, se seleccionan todos
            var lista = this.Contexto.ViewTotal.FromSql($"EXECUTE getVehiculosPorColor {elColor}");
            return View(lista);
        }


        // GET: VehiculoController
        public ActionResult Index()
        {
            List<VehiculoModelo> lista = this.Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca).Include(x => x.Combustible).Include(v => v.VehiculoExtras).ThenInclude(ve => ve.extra).ToList();
            return View(lista);
        }

        // GET: VehiculoController/BuscadorCombustible ( EXAMEN )
        public ActionResult BuscadorCombustible(int combustibleIdentificador  = 0)
        {
            ViewBag.Combustibles = new SelectList(Contexto.Combustibles, "Id", "NomCombustible", combustibleIdentificador);
            var lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca).Include(x => x.Combustible).Include(v => v.VehiculoExtras).ThenInclude(ve => ve.extra)
                        where (v.CombustibleId.Equals(combustibleIdentificador))
                        select v;
            if (combustibleIdentificador == 0)
            {
                lista = from v in Contexto.Vehiculos.Include(s => s.Serie).Include(m => m.Serie.Marca).Include(x => x.Combustible).Include(v => v.VehiculoExtras).ThenInclude(ve => ve.extra)
                        select v;
            }
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
        public ActionResult Details2(String Matricula)
        {
            if (Matricula != null)
            {
                //var Matricula = id;
                var lista = this.Contexto.ViewTotal.FromSql($"SELECT dbo.Marcas.NomMarca, dbo.Series.NomSerie, dbo.Vehiculos.Matricula, dbo.Vehiculos.Color\r\n\tFROM     dbo.Marcas INNER JOIN\r\n                  dbo.Series ON dbo.Marcas.Id = dbo.Series.MarcaId INNER JOIN\r\n                  dbo.Vehiculos ON dbo.Series.Id = dbo.Vehiculos.SerieId");
                //var lista = this.Contexto.ViewTotalius.FromSql($"SELECT dbo.Marcas.NomMarca, dbo.Series.NomSerie, dbo.Vehiculos.Matricula, dbo.Vehiculos.Color\r\n\tFROM     dbo.Marcas INNER JOIN\r\n                  dbo.Series ON dbo.Marcas.Id = dbo.Series.MarcaId INNER JOIN\r\n                  dbo.Vehiculos ON dbo.Series.Id = dbo.Vehiculos.SerieId");
                //var lista = Contexto.ViewTotalius.Include(s => s.Matricula).Include(s => s.NomMarca).Include(s => s.NomSerie).Include(s => s.Color).ToList();
                foreach (var item in lista)
                {
                    if (item.Matricula == Matricula)
                    {
                        return View(item);
                    }
                }

                return View(lista);
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
            ViewBag.Combustibles = new SelectList(Contexto.Combustibles, "Id", "NomCombustible");
            ViewBag.VehiculoExtras = new MultiSelectList(this.Contexto.Extras, "Id", "NomModelo");
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
            foreach (var extraId in vehiculoModelo.ExtrasSeleccionados)
            {
                //var obj = new VehiculoExtraModelo()
                //{
                //    extraId = extraId,
                //    vehiculoId = vehiculoModelo.Id
                //}
                VehiculoExtraModelo vehiculoExtra = new VehiculoExtraModelo();
                vehiculoExtra.vehiculoId = vehiculoModelo.Id;
                vehiculoExtra.extraId = extraId;
                Contexto.VehiculoExtras.Add(vehiculoExtra);
                Contexto.SaveChanges();
            }
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
            ViewBag.VehiculoExtras = new MultiSelectList(this.Contexto.Extras, "Id", "NomModelo");
            VehiculoModelo elemento = this.Contexto.Vehiculos.Find(id);
            elemento.ExtrasSeleccionados = this.Contexto.VehiculoExtras.Where(ve => ve.vehiculoId == id).Select(ve => ve.extraId).ToList();
            ViewBag.VehiculoExtras = new MultiSelectList(this.Contexto.Extras, "Id", "NomModelo", elemento.ExtrasSeleccionados);
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
                    var extrasActuales = this.Contexto.VehiculoExtras.Where(ve => ve.vehiculoId == id);//.Select(ve => ve.extraId).ToList();
                    //var extrasSeleccionados = vehiculoModelo.ExtrasSeleccionados;
                    //var extrasParaBorrar = extrasActuales.Except(extrasSeleccionados);
                    //var extrasParaAnadir = extrasSeleccionados.Except(extrasActuales);
                    foreach (var extrasParaBorrar in extrasActuales)
                    {
                        //VehiculoExtraModelo vehiculoExtra = this.Contexto.VehiculoExtras.FirstOrDefault(ve => ve.extraId == extraId && ve.vehiculoId == id);
                        Contexto.VehiculoExtras.Remove(extrasParaBorrar);
                        
                    }
                    foreach (var extraId in vehiculoModelo.ExtrasSeleccionados)
                    {
                        VehiculoExtraModelo vehiculoExtra = new VehiculoExtraModelo();
                        vehiculoExtra.vehiculoId = vehiculoModelo.Id;
                        vehiculoExtra.extraId = extraId;
                        Contexto.VehiculoExtras.Add(vehiculoExtra);
                    }
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
