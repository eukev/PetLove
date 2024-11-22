using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class CruceController : Controller
    {
        // Acción para mostrar todos los registros de cruces
        public ActionResult Index()
        {
            return View(new Cruce().GetCruces()); // Obtiene y muestra todos los cruces
        }

        // Acción para mostrar los detalles de un cruce específico
        public ActionResult Details(int id)
        {
            var cruce = new Cruce().GetCruceById(id);
            return View(cruce); // Muestra los detalles del cruce seleccionado
        }

        // Acción para mostrar el formulario de creación de un nuevo cruce
        public ActionResult Create()
        {
            return View(); // Muestra el formulario vacío para ingresar los datos del cruce
        }

        // Acción POST para guardar un nuevo cruce después de enviar el formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var cruce = new Cruce
                {
                    id_mascota = int.Parse(collection["id_mascota"]),
                    id_mascota2 = int.Parse(collection["id_mascota2"]),
                    fecha = collection["fecha"],
                    lugar = collection["lugar"]
                };
                cruce.AddCruce(cruce); // Guarda el nuevo registro de cruce
                return RedirectToAction(nameof(Index)); // Redirige a la lista de cruces después de guardar
            }
            catch
            {
                return View(); // Muestra el formulario de nuevo si ocurre un error
            }
        }

        // Acción para mostrar el formulario de edición de un cruce existente por ID
        public ActionResult Edit(int id)
        {
            var cruce = new Cruce().GetCruceById(id);
            return View(cruce); // Muestra el formulario con los datos existentes del cruce
        }

        // Acción POST para guardar los cambios de un cruce después de enviar el formulario de edición
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var cruce = new Cruce
                {
                    IdCruce = id,
                    id_mascota = int.Parse(collection["id_mascota"]),
                    id_mascota2 = int.Parse(collection["id_mascota2"]),
                    fecha = collection["fecha"],
                    lugar = collection["lugar"]
                };
                cruce.EditCruce(cruce); // Actualiza el registro de cruce
                return RedirectToAction(nameof(Index)); // Redirige a la lista de cruces después de guardar
            }
            catch
            {
                return View(); // Muestra el formulario de nuevo si ocurre un error
            }
        }

        // Acción para mostrar una página de confirmación para eliminar un cruce por ID
        public ActionResult Delete(int id)
        {
            var cruce = new Cruce().GetCruceById(id);
            return View(cruce); // Muestra la página de confirmación de eliminación
        }

        // Acción POST para eliminar un cruce después de que se confirme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                new Cruce().DeleteCruce(id); // Elimina el registro de cruce
                return RedirectToAction(nameof(Index)); // Redirige a la lista de cruces después de eliminar
            }
            catch
            {
                return View(); // Muestra la página de eliminación de nuevo si ocurre un error
            }
        }
    }
}
