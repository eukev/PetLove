using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sanchez_Campos_Kevin_Alexis.Models;

namespace Sanchez_Campos_Kevin_Alexis.Controllers
{
    public class PersonaController : Controller
    {
        // Esta acción se ejecuta cuando queremos ver la lista de todas las personas
        public ActionResult Index()
        {
            // Obtenemos todas las personas y las mostramos en la vista
            return View(new Persona().GetPersonas());
        }

        // Esta acción muestra los detalles de una persona en particular
        public ActionResult Details(int id)
        {
            // Mostramos la vista con los detalles de la persona
            return View();
        }

        // Esta acción muestra el formulario para crear una nueva persona
        public ActionResult Create()
        {
            // Mostramos la vista con el formulario vacío para agregar una nueva persona
            return View();
        }

        // Esta acción guarda los datos de la persona que fue enviada a través del formulario
        [HttpPost]
        [ValidateAntiForgeryToken] // Esta etiqueta ayuda a prevenir ataques de seguridad
        public ActionResult Create(IFormCollection collection, Persona person)
        {
            try
            {
                // Llamamos al método para agregar la nueva persona a la base de datos
                person.AddPersona(person);

                // Después de agregarla, redirigimos a la página principal con todas las personas
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si algo sale mal, volvemos a mostrar el formulario para corregirlo
                return View();
            }
        }

        // Esta acción muestra el formulario para editar los datos de una persona existente
        [HttpGet] // Esta es la versión GET, que se usa cuando queremos mostrar el formulario
        public ActionResult Edit(int id)
        {
            // Obtenemos los detalles de la persona que se quiere editar y los mostramos en el formulario
            return View(new Persona().GetPersonaById(id));
        }

        // Esta acción guarda los cambios hechos a una persona después de enviar el formulario
        [HttpPost] // Esta es la versión POST, que se usa cuando enviamos los cambios desde el formulario
        [ValidateAntiForgeryToken] // Al igual que en el método de creación, esto ayuda a proteger la aplicación
        public ActionResult Edit(int id, IFormCollection collection, Persona person)
        {
            try
            {
                // Llamamos al método para actualizar los datos de la persona en la base de datos
                person.EditPersona(person);

                // Después de editar, redirigimos a la página principal con todas las personas
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si ocurre un error, mostramos el formulario de nuevo para que se puedan corregir los datos
                return View();
            }
        }

        // Esta acción muestra una página de confirmación para eliminar una persona
        public ActionResult Delete(int id)
        {
            // Simplemente mostramos la vista para confirmar si queremos eliminar a la persona
            return View();
        }

        // Esta acción elimina la persona después de que se haya confirmado
        [HttpPost] // Esta es la versión POST, que se usa cuando se confirma la eliminación
        [ValidateAntiForgeryToken] // Protege contra ataques de seguridad
        public ActionResult Delete(int id, IFormCollection collection, Persona person)
        {
            try
            {
                // Llamamos al método para eliminar a la persona de la base de datos
                person.DelPersona(id);

                // Después de eliminarla, redirigimos a la página principal con todas las personas
                return View();
            }
            catch
            {
                // Si algo sale mal, mostramos la página de eliminación de nuevo
                return View();
            }
        }
    }
}
