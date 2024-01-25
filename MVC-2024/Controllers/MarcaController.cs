using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ActionResult Listado()
        {
            List<MarcaModel> lista = this.Contexto.Marcas.ToList();
            return View(lista);
        }

        public ActionResult Desplegable()
        {
            //ChatGPT
            //List<MarcaModel> lista = this.Contexto.Marcas.ToList();

        //ViewBag IMPORTANTE !!!!!!!!!!!!!!!!!!!!!!!!!!!
            ViewBag.Marcas = new SelectList(Contexto.Marcas, "Id", "NomMarca");
            ViewBag.Marcas2 = this.Contexto.Marcas.ToList();
            return View();
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
            // Busca el elemento directamente en la base de datos usando el id proporcionado
            MarcaModel elemento = this.Contexto.Marcas.Find(id);

            // Verifica si se encontró el elemento
            if (elemento != null)
            {
                // Si se encontró, pasa el elemento correspondiente a la vista
                return View(elemento);
            }
            else
            {
                // Si no se encontró, puedes manejarlo de la manera que consideres apropiada
                // Por ejemplo, puedes redirigir a una página de error.
                return RedirectToAction("Error");
            }
            //List<MarcaModel> lista = this.Contexto.Marcas.ToList();
            //// Encuentra el índice del elemento con el ID especificado
            //int indice = lista.FindIndex(item => item.Id == id);

            //// Verifica si se encontró el elemento
            //if (indice != -1)
            //{
            //    // Si se encontró, pasa el elemento correspondiente a la vista
            //    return View(lista[indice]);
            //}
            //else
            //{
            //    // Si no se encontró, puedes manejarlo de la manera que consideres apropiada
            //    // Por ejemplo, puedes redirigir a una página de error.
            //    return RedirectToAction("Error");
            //}
            //return View();
        }

        // POST: MarcaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,MarcaModel marca)
        {
            try
            {
                // Buscar el elemento en la base de datos por el ID
                MarcaModel elemento = Contexto.Marcas.Find(id);

                if (elemento != null)
                {
                    // Actualizar propiedades del elemento con los valores proporcionados por el modelo
                    elemento.Id = marca.Id; // Reemplaza Propiedad1 con el nombre real de tus propiedades
                    elemento.NomMarca = marca.NomMarca; // Reemplaza Propiedad2 con el nombre real de tus propiedades
                                                            // ...

                    // Guardar los cambios en la base de datos
                    Contexto.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si el elemento no se encuentra, puedes manejarlo como consideres apropiado
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción de manera adecuada para tu aplicación
                // Por ejemplo, puedes agregar registros de errores, mostrar un mensaje al usuario, etc.
                return RedirectToAction("Error");
            }
            //MarcaModel elemento = Contexto.Marcas.Find(id);
            //// como cambio elemento por marca?
            //Contexto.Database.EnsureCreated();
            //Contexto.SaveChanges();
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: MarcaController/Delete/5
        public ActionResult Delete(int id)
        {
            // Busca el elemento directamente en la base de datos usando el id proporcionado
            MarcaModel elemento = this.Contexto.Marcas.Find(id);

            // Verifica si se encontró el elemento
            if (elemento != null)
            {
                // Si se encontró, pasa el elemento correspondiente a la vista
                return View(elemento);
            }
            else
            {
                // Si no se encontró, puedes manejarlo de la manera que consideres apropiada
                // Por ejemplo, puedes redirigir a una página de error.
                return RedirectToAction("Error");
            }
        }

        // POST: MarcaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                MarcaModel elemento = Contexto.Marcas.Find(id);
                if (elemento != null)
                {
                    Contexto.Marcas.Remove(elemento);
                    Contexto.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
