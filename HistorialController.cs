using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class HistorialController : Controller
    {
        // Esta acción se ejecuta cuando queremos ver la lista de todos los historiales
        public ActionResult Index()
        {
            // Obtenemos todos los historiales y los mostramos en la vista
            return View(new Historial().GetHistoriales());
        }

        // Esta acción muestra los detalles de un historial en particular
        public ActionResult Details(int id)
        {
            // Mostramos la vista con los detalles del historial
            return View(new Historial().GetHistorialById(id));
        }

        // Esta acción muestra el formulario para crear un nuevo historial
        public ActionResult Create()
        {
            // Mostramos la vista con el formulario vacío para agregar un nuevo historial
            return View();
        }

        // Esta acción guarda los datos del historial que fue enviado a través del formulario
        [HttpPost]
        [ValidateAntiForgeryToken] // Esta etiqueta ayuda a prevenir ataques de seguridad
        public ActionResult Create(IFormCollection collection, Historial historial)
        {
            try
            {
                // Llamamos al método para agregar el nuevo historial a la base de datos
                historial.AddHistorial(historial);

                // Después de agregarlo, redirigimos a la página principal con todos los historiales
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si algo sale mal, volvemos a mostrar el formulario para corregirlo
                return View();
            }
        }

        // Esta acción muestra el formulario para editar los datos de un historial existente
        [HttpGet] // Esta es la versión GET, que se usa cuando queremos mostrar el formulario
        public ActionResult Edit(int id)
        {
            // Obtenemos los detalles del historial que se quiere editar y los mostramos en el formulario
            return View(new Historial().GetHistorialById(id));
        }

        // Esta acción guarda los cambios hechos a un historial después de enviar el formulario
        [HttpPost] // Esta es la versión POST, que se usa cuando enviamos los cambios desde el formulario
        [ValidateAntiForgeryToken] // Al igual que en el método de creación, esto ayuda a proteger la aplicación
        public ActionResult Edit(int id, IFormCollection collection, Historial historial)
        {
            try
            {
                // Llamamos al método para actualizar los datos del historial en la base de datos
                historial.EditHistorial(historial);

                // Después de editar, redirigimos a la página principal con todos los historiales
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si ocurre un error, mostramos el formulario de nuevo para que se puedan corregir los datos
                return View();
            }
        }

        // Esta acción muestra una página de confirmación para eliminar un historial
        public ActionResult Delete(int id)
        {
            // Obtenemos los detalles del historial para confirmar la eliminación
            return View(new Historial().GetHistorialById(id));
        }

        // Esta acción elimina el historial después de que se haya confirmado
        [HttpPost] // Esta es la versión POST, que se usa cuando se confirma la eliminación
        [ValidateAntiForgeryToken] // Protege contra ataques de seguridad
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Llamamos al método para eliminar el historial de la base de datos
                new Historial().DeleteHistorial(id);

                // Después de eliminarlo, redirigimos a la página principal con todos los historiales
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si algo sale mal, mostramos la página de eliminación de nuevo
                return View();
            }
        }
    }
}
