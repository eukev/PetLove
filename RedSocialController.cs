using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class RedSocialController : Controller
    {
        // Esta acción se ejecuta cuando queremos ver la lista de todas las redes sociales
        public ActionResult Index()
        {
            // Obtenemos todas las redes sociales y las mostramos en la vista
            return View(new Red_Social().GetRedesSociales());
        }

        // Esta acción muestra los detalles de una red social en particular
        public ActionResult Details(int id)
        {
            // Mostramos la vista con los detalles de la red social
            return View(new Red_Social().GetRedSocialById(id));
        }

        // Esta acción muestra el formulario para crear una nueva red social
        public ActionResult Create()
        {
            // Mostramos la vista con el formulario vacío para agregar una nueva red social
            return View();
        }

        // Esta acción guarda los datos de la red social que fue enviada a través del formulario
        [HttpPost]
        [ValidateAntiForgeryToken] // Esta etiqueta ayuda a prevenir ataques de seguridad
        public ActionResult Create(IFormCollection collection, Red_Social redSocial)
        {
            try
            {
                // Llamamos al método para agregar la nueva red social a la base de datos
                redSocial.AddRedSocial(redSocial);

                // Después de agregarla, redirigimos a la página principal con todas las redes sociales
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si algo sale mal, volvemos a mostrar el formulario para corregirlo
                return View();
            }
        }

        // Esta acción muestra el formulario para editar los datos de una red social existente
        [HttpGet] // Esta es la versión GET, que se usa cuando queremos mostrar el formulario
        public ActionResult Edit(int id)
        {
            // Obtenemos los detalles de la red social que se quiere editar y los mostramos en el formulario
            return View(new Red_Social().GetRedSocialById(id));
        }

        // Esta acción guarda los cambios hechos a una red social después de enviar el formulario
        [HttpPost] // Esta es la versión POST, que se usa cuando enviamos los cambios desde el formulario
        [ValidateAntiForgeryToken] // Al igual que en el método de creación, esto ayuda a proteger la aplicación
        public ActionResult Edit(int id, IFormCollection collection, Red_Social redSocial)
        {
            try
            {
                // Llamamos al método para actualizar los datos de la red social en la base de datos
                redSocial.EditRedSocial(redSocial);

                // Después de editar, redirigimos a la página principal con todas las redes sociales
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si ocurre un error, mostramos el formulario de nuevo para que se puedan corregir los datos
                return View();
            }
        }

        // Esta acción muestra una página de confirmación para eliminar una red social
        public ActionResult Delete(int id)
        {
            // Obtenemos los detalles de la red social para confirmar la eliminación
            return View(new Red_Social().GetRedSocialById(id));
        }

        // Esta acción elimina la red social después de que se haya confirmado
        [HttpPost] // Esta es la versión POST, que se usa cuando se confirma la eliminación
        [ValidateAntiForgeryToken] // Protege contra ataques de seguridad
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Llamamos al método para eliminar la red social de la base de datos
                new Red_Social().DeleteRedSocial(id);

                // Después de eliminarla, redirigimos a la página principal con todas las redes sociales
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
