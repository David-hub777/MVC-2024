using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_2024.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MVC_2024.Controllers
{
    public class ClienteController : Controller
    {
        public List<Cliente> Clientes { get; set; }

        public ClienteController()
        {
            // URL de la Web API
            string apiUrl = "https://localhost:44332/api/Alumno";

            // Llama a la función para obtener la lista de Clientes
            Clientes = ObtenerClientes(apiUrl).Result;
        }

        private async Task<List<Cliente>> ObtenerClientes(string apiUrl)
            // asincronia: continuar con la app sin esperar la respuesta
        {
            using (HttpClient client = new HttpClient())
            {
                // Configura la solicitud
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Realiza la solicitud GET
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Verifica si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Lee y deserializa la respuesta en una lista de objetos Cliente
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(jsonResponse);

                    return clientes;
                }
                else
                {
                    // Maneja el error de la solicitud
                    // Aquí puedes decidir cómo manejar el error, lanzar una excepción, mostrar un mensaje, etc.
                    return new List<Cliente>();
                }
            }
        }






        // GET: MarcaController
        public ActionResult Index()
        {
            List<Cliente> lista = this.Clientes.ToList();
            return View(lista);
        }

        //public ActionResult Listado()
        //{
        //    List<MarcaModel> lista = this.Contexto.Marcas.ToList();
        //    return View(lista);
        //}

        //public ActionResult Desplegable()
        //{
        //    //ChatGPT
        //    //List<MarcaModel> lista = this.Contexto.Marcas.ToList();

        ////ViewBag IMPORTANTE !!!!!!!!!!!!!!!!!!!!!!!!!!!
        //    ViewBag.Marcas = new SelectList(Contexto.Marcas, "Id", "NomMarca");
        //    ViewBag.Marcas2 = this.Contexto.Marcas.ToList();
        //    return View();
        //}


        // GET: MarcaController/Details/5
        //public ActionResult Details(int id)
        //{
        //    List<MarcaModel> lista = this.Contexto.Marcas.ToList();
        //    // Encuentra el índice del elemento con el ID especificado
        //    int indice = lista.FindIndex(item => item.Id == id);

        //    // Verifica si se encontró el elemento
        //    if (indice != -1)
        //    {
        //        // Si se encontró, pasa el elemento correspondiente a la vista
        //        return View(lista[indice]);
        //    }
        //    else
        //    {
        //        // Si no se encontró, puedes manejarlo de la manera que consideres apropiada
        //        // Por ejemplo, puedes redirigir a una página de error.
        //        return RedirectToAction("Error");
        //    }
        //}

        // GET: MarcaController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: MarcaController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(MarcaModel marca)
        //{
        //    Contexto.Marcas.Add(marca);
        //    Contexto.Database.EnsureCreated();
        //    //Contexto.Database.AutoSavepointsEnabled = true;//Que hace esto?
        //    Contexto.SaveChanges();
            
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: MarcaController/Edit/5
        public ActionResult Edit(int id)
        {
            Cliente elemento = this.Clientes.Find(item => item.Id == id);

            if (elemento != null)
            {
                return View(elemento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // POST: MarcaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,Cliente cliente)
        {
            try
            {
                Cliente elemento = this.Clientes.Find(item => item.Id == id);
                if (elemento != null)
                {
                    elemento.Id = cliente.Id; 
                    elemento.nombre = cliente.nombre; 
                    elemento.apellidos = cliente.apellidos;
                    elemento.direccion = cliente.direccion;
                    elemento.fnac = cliente.fnac;
                    elemento.sexo = cliente.sexo;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Excepción llamada Error 1234567890");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Captura una EXPLOSION de .NET   : )");
            }
        }

        // GET: MarcaController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    // Busca el elemento directamente en la base de datos usando el id proporcionado
        //    MarcaModel elemento = this.Contexto.Marcas.Find(id);

        //    // Verifica si se encontró el elemento
        //    if (elemento != null)
        //    {
        //        // Si se encontró, pasa el elemento correspondiente a la vista
        //        return View(elemento);
        //    }
        //    else
        //    {
        //        // Si no se encontró, puedes manejarlo de la manera que consideres apropiada
        //        // Por ejemplo, puedes redirigir a una página de error.
        //        return RedirectToAction("Error");
        //    }
        //}

        // POST: MarcaController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        MarcaModel elemento = Contexto.Marcas.Find(id);
        //        if (elemento != null)
        //        {
        //            Contexto.Marcas.Remove(elemento);
        //            Contexto.SaveChanges();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            return RedirectToAction("Error");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Error");
        //    }
        //}
    }
}
