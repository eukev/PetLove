using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class ClasificacionController : Controller
    {
        // Esta acción muestra todas las clasificaciones
        public ActionResult Index()
        {
            return View(new Clasificacion().GetClasificaciones()); // Traemos todas las clasificaciones y las mostramos
        }

        // Esta acción muestra los detalles de una clasificación específica
        public ActionResult Details(int id)
        {
            return View(); // Mostramos los detalles de la clasificación
        }

        // Esta acción muestra el formulario para crear una nueva clasificación
        public ActionResult Create()
        {
            return View(); // Mostramos un formulario vacío para llenar los datos de la clasificación
        }

        // Esta acción guarda una nueva clasificación cuando se envía el formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Clasificacion clasif)
        {
            try
            {
                clasif.AddClasificacion(clasif); // Guardamos la nueva clasificación en la base de datos
                return RedirectToAction(nameof(Index)); // Después de guardar, volvemos a la lista de clasificaciones
            }
            catch
            {
                return View(); // Si ocurre un error, mostramos de nuevo el formulario
            }
        }

        // Esta acción muestra el formulario para editar una clasificación
        public ActionResult Edit(int id)
        {
            Clasificacion clasificacion = new Clasificacion().GetClasificacionById(id); // Traemos la clasificación por su ID

            return View(clasificacion); // Mostramos los datos de la clasificación para editar
        }

        // Esta acción guarda los cambios después de editar una clasificación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Clasificacion clasificacion)
        {
            try
            {
                clasificacion.EditClasificacion(clasificacion); // Guardamos los cambios de la clasificación
                return RedirectToAction(nameof(Index)); // Después de editar, volvemos a la lista de clasificaciones
            }
            catch
            {
                return View(); // Si ocurre un error, mostramos de nuevo el formulario
            }
        }

        // Esta acción muestra la vista para confirmar la eliminación de una clasificación
        public ActionResult Delete(int id)
        {
            Clasificacion clasificacion = new Clasificacion().GetClasificacionById(id); // Traemos la clasificación por su ID
            if (clasificacion == null)
            {
                return NotFound(); // Si no encontramos la clasificación, mostramos un error
            }
            return View(clasificacion); // Mostramos la página para confirmar la eliminación de la clasificación
        }

        // Esta acción elimina una clasificación de la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Clasificacion clasificacion = new Clasificacion(); // Creamos una nueva instancia de Clasificacion
                clasificacion.DeleteClasificacion(id); // Llamamos al método para eliminar la clasificación

                return RedirectToAction(nameof(Index)); // Después de eliminar, volvemos a la lista de clasificaciones
            }
            catch
            {
                return View(); // Si ocurre un error, mostramos de nuevo la página de eliminación
            }
        }
    }
}
