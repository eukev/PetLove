
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class DuenoController : Controller
    {
        // Esta acción muestra todos los dueños
        public ActionResult Index()
        {
            return View(new Dueno().GetDuenos()); // Traemos todos los dueños y los mostramos
        }

        // Esta acción muestra los detalles de un dueño específico
        public ActionResult Details(int id)
        {
            return View(); // Aquí podríamos mostrar detalles del dueño
        }

        // Esta acción muestra el formulario para crear un nuevo dueño
        public ActionResult Create()
        {
            return View(); // Aquí mostramos un formulario vacío para llenar
        }

        // Esta acción guarda el nuevo dueño cuando se envía el formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Dueno duen)
        {
            try
            {
                duen.AddDueno(duen); // Guardamos el nuevo dueño en la base de datos
                return RedirectToAction(nameof(Index)); // Después de guardar, vamos a la lista de dueños
            }
            catch
            {
                return View(); // Si hay un error, mostramos el formulario de nuevo
            }
        }

        // Esta acción muestra el formulario para editar un dueño
        public ActionResult Edit(int id)
        {
            return View(new Dueno().GetDuenosById(id)); // Traemos la información del dueño para editarla
        }

        // Esta acción guarda los cambios después de editar un dueño
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Dueno duen)
        {
            try
            {
                duen.EditDueno(duen); // Guardamos los cambios en el dueño
                return RedirectToAction(nameof(Index)); // Después de editar, vamos a la lista de dueños
            }
            catch
            {
                return View(); // Si hay un error, mostramos el formulario de nuevo
            }
        }

        // Esta acción muestra la vista para confirmar la eliminación de un dueño
        public ActionResult Delete(int id)
        {
            Dueno duen = new Dueno().GetDuenosById(id); // Buscamos al dueño por su ID
            if (duen.IdDueno == 0)
            {
                return NotFound(); // Si no encontramos al dueño, mostramos un error
            }
            return View(duen); // Si encontramos al dueño, mostramos la página para confirmar su eliminación
        }

        // Esta acción elimina un dueño de la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Dueno duen = new Dueno(); // Creamos una nueva instancia de Dueno
                duen.DeleteDueno(id); // Llamamos al método para eliminar al dueño

                return RedirectToAction(nameof(Index)); // Después de eliminar, vamos a la lista de dueños
            }
            catch
            {
                return View(); // Si hay un error, mostramos de nuevo la página de eliminación
            }
        }

    }
}
