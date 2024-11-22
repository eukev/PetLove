using Npgsql;
using System.Data;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    // La clase Persona hereda de Conexion y representa a una persona en el sistema
    public class Persona : Conexion
    {
        // Propiedades de la clase Persona
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Curp { get; set; }
        public int Edad { get; set; }

        // Constructor vacío de la clase Persona
        public Persona() { }

        // Constructor para inicializar la propiedad IdPersona
        public Persona(int IdPersona)
        {
            this.IdPersona = IdPersona;
        }

        // Método para agregar una nueva persona a la base de datos
        public void AddPersona(Persona person)
        {
            // Consulta SQL para insertar una nueva persona
            const string SQL = "INSERT INTO persona(nombre, apellidos, curp, edad) VALUES(:nom, :ap, :curp, :ed);";
            // Lista para almacenar los parámetros que vamos a usar en la consulta
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>();
            // Creamos los parámetros a partir de las propiedades de la persona
            NpgsqlParameter paramNombre = new NpgsqlParameter(":nom", person.Nombre);
            NpgsqlParameter paramAp = new NpgsqlParameter(":ap", person.Apellidos);
            NpgsqlParameter paramCurp = new NpgsqlParameter(":curp", person.Curp);
            NpgsqlParameter paramEdad = new NpgsqlParameter(":ed", person.Edad);
            // Agregamos los parámetros a la lista
            lstParams.Add(paramNombre);
            lstParams.Add(paramAp);
            lstParams.Add(paramCurp);
            lstParams.Add(paramEdad);
            // Ejecutamos la consulta con los parámetros
            GetQuery(SQL, lstParams);
        }

        // Método para actualizar los datos de una persona existente
        public void EditPersona(Persona person)
        {
            // Consulta SQL para actualizar los datos de la persona
            const string SQL = "UPDATE persona SET curp=:curp, nombre=:nom, apellidos=:ap, edad=:ed WHERE id_persona=:id";
            // Creamos los parámetros para la actualización
            NpgsqlParameter paramId = new NpgsqlParameter(":id", person.IdPersona);
            NpgsqlParameter paramNom = new NpgsqlParameter(":nom", person.Nombre);
            NpgsqlParameter paramAp = new NpgsqlParameter(":ap", person.Apellidos);
            NpgsqlParameter paramCurp = new NpgsqlParameter(":curp", person.Curp);
            NpgsqlParameter paramEdad = new NpgsqlParameter(":ed", person.Edad);
            // Lista de parámetros que usaremos en la consulta
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>() { paramId, paramNom, paramAp, paramCurp, paramEdad };
            // Ejecutamos la consulta para actualizar los datos
            GetQuery(SQL, lstParams);
        }

        // Método para obtener una persona por su ID
        public Persona GetPersonaById(int id)
        {
            // Consulta SQL para obtener una persona por ID
            const string SQL = "SELECT * FROM persona WHERE id_persona =:id;";
            // Creamos el parámetro para el ID
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            // Lista con el parámetro de ID
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>() { paramId };
            // Ejecutamos la consulta y guardamos el resultado en una tabla
            DataTable tabla = GetQuery(SQL, lstParams);
            // Si no se encuentra ninguna persona, devolvemos una nueva persona vacía
            if (tabla.Rows.Count < 1) return new Persona();
            // Procesamos los datos de la primera fila y devolvemos un objeto Persona
            foreach (DataRow row in tabla.Rows)
            {
                Persona person = new Persona();
                person.Nombre = (string)row["nombre"];
                person.IdPersona = (int)row["id_persona"];
                person.Apellidos = (string)row["apellidos"];
                person.Curp = (string)row["curp"];
                person.Edad = (int)row["edad"];
                return person;
            }
            return new Persona();
        }

        // Método para obtener todas las personas de la base de datos
        public List<Persona> GetPersonas()
        {
            // Consulta SQL para obtener todas las personas ordenadas por su ID
            const string SQL = "SELECT * FROM public.persona ORDER BY id_persona ASC;";
            // Ejecutamos la consulta y guardamos el resultado en una tabla
            DataTable tabla = GetQuery(SQL);
            // Lista donde guardaremos las personas
            List<Persona> lstPersona = new List<Persona>();
            // Si no hay resultados, devolvemos la lista vacía
            if (tabla.Rows.Count < 1)
            {
                return lstPersona;
            }
            // Procesamos cada fila de la tabla y agregamos una persona a la lista
            foreach (DataRow fila in tabla.Rows)
            {
                lstPersona.Add(new Persona(
                    (int)fila["id_persona"],
                    (string)fila["nombre"],
                    (string)fila["apellidos"],
                    (string)fila["curp"],
                    (int)fila["edad"]));
            }
            return lstPersona;
        }

        // Método para eliminar una persona por su ID
        public void DelPersona(int id)
        {
            // Consulta SQL para eliminar una persona por su ID
            const string SQL = "DELETE FROM persona WHERE id_persona = :id;";
            // Creamos el parámetro para el ID
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            // Ejecutamos la consulta de eliminación
            GetQuery(SQL);
        }

        // Método para calcular la edad de una persona usando su CURP
        public int ObtenerEdad()
        {
            // Variables para almacenar las partes de la fecha extraídas del CURP
            string yearC, monthC, dayC;
            int yearCIn, monthCIn, dayCIn, yearA, monthA, dayA;

            // Ejemplo de CURP
            Curp = "DSJA050926HMCZNRA0";

            // Extraemos el año, mes y día de nacimiento del CURP
            yearC = Curp.Substring(4, 2);
            monthC = Curp.Substring(6, 2);
            dayC = Curp.Substring(8, 2);

            // Convertimos las partes de la fecha de string a enteros
            yearCIn = int.Parse(yearC);
            monthCIn = int.Parse(monthC);
            dayCIn = int.Parse(dayC);

            // Fechas actuales
            yearA = DateTime.Now.Year;
            monthA = DateTime.Now.Month;
            dayA = DateTime.Now.Day;

            // Ajustamos el año para los nacidos en el siglo XX y XXI
            if (yearCIn > 24)
            {
                yearCIn = yearCIn + 1900;
            }
            else
            {
                yearCIn = yearCIn + 2000;
            }

            // Calculamos la edad
            Edad = yearA - yearCIn;

            // Ajustamos la edad si la persona no ha cumplido años este año
            if (monthCIn == monthA)
            {
                if (dayCIn > dayA)
                {
                    Edad = Edad - 1;
                }
            }
            else if (monthCIn > monthA)
            {
                Edad = Edad - 1;
            }

            return Edad;
        }

        // Constructor para inicializar todas las propiedades de la persona
        public Persona(int idPersona, string nombre, string apellidos, string curp, int edad) : this(idPersona)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Curp = curp;
            Edad = edad;
        }
    }
}
