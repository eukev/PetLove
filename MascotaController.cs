using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class MascotaController : Controller
    {
        // Esta acción muestra todas las mascotas
        public ActionResult Index()
        {
            return View(new Mascota().GetMascotas()); // Traemos todas las mascotas y las mostramos
        }

        // Esta acción muestra los detalles de una mascota específica
        public ActionResult Details(int id)
        {
            return View(); // Mostramos los detalles de la mascota
        }

        // Esta acción muestra el formulario para crear una nueva mascota
        public ActionResult Create()
        {
            return View(); // Mostramos un formulario vacío para llenar los datos de la mascota
        }

        // Esta acción guarda una nueva mascota cuando se envía el formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Mascota masco)
        {
            try
            {
                masco.AddMascota(masco); // Guardamos la nueva mascota en la base de datos
                return RedirectToAction(nameof(Index)); // Después de guardar, volvemos a la lista de mascotas
            }
            catch
            {
                return View(); // Si ocurre un error, mostramos de nuevo el formulario
            }
        }

        // Esta acción muestra el formulario para editar una mascota
        public ActionResult Edit(int id)
        {
            Mascota mascota = new Mascota().GetMascotaById(id); // Traemos la mascota por su ID
            
            return View(new Mascota().GetMascotaById(id)); // Mostramos los datos de la mascota para editar
        }

        // Esta acción guarda los cambios después de editar una mascota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Mascota mascota)
        {
            try
            {
                mascota.EditMascota(mascota); // Guardamos los cambios de la mascota
                return RedirectToAction(nameof(Index)); // Después de editar, volvemos a la lista de mascotas
            }
            catch
            {
                return View(); // Si ocurre un error, mostramos de nuevo el formulario
            }
        }

        // Esta acción muestra la vista para confirmar la eliminación de una mascota
        public ActionResult Delete(int id)
        {
            Mascota mascota = new Mascota().GetMascotaById(id); // Traemos la mascota por su ID
            if (mascota == null)
            {
                return NotFound(); // Si no encontramos la mascota, mostramos un error
            }
            return View(mascota); // Mostramos la página para confirmar la eliminación de la mascota
        }

        // Esta acción elimina una mascota de la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Mascota mascota = new Mascota(); // Creamos una nueva instancia de Mascota
                mascota.DeleteMascota(id); // Llamamos al método para eliminar la mascota

                return RedirectToAction(nameof(Index)); // Después de eliminar, volvemos a la lista de mascotas
            }
            catch
            {
                return View(); // Si ocurre un error, mostramos de nuevo la página de eliminación
            }
        }
    }
}
