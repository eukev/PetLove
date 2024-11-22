using Sanchez_Campos_Kevin_Alexis.Models;
using System;
using System.ComponentModel;
using System.Data;
using Npgsql;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sanchez_Campos_Kevin_Alexis.Models
{

    public class Dueno : ConexionMascotas
    {
        public int IdDueno { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public long Telefono { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Mascota> Mascotas { get; set; }

        public Dueno() { }

        // Constructor con ID
        public Dueno(int IdDueno)
        {
            this.IdDueno = IdDueno;
        }

        // Constructor completo
        public Dueno(int IdDueno, string Nombre, string ApellidoPaterno, string ApellidoMaterno, long Telefono, string Correo, string Contrasena) : this(IdDueno)
        {
            this.Nombre = Nombre;
            this.ApellidoPaterno = ApellidoPaterno;
            this.ApellidoMaterno = ApellidoMaterno;
            this.Telefono = Telefono;
            this.Correo = Correo;
            this.Contrasena = Contrasena;
        }

        // Agregar un nuevo dueño a la base de datos
        public void AddDueno(Dueno dueno)
        {
            try
            {
                const string SQL = "INSERT INTO public.dueno(nombre, apellido_paterno, apellido_materno, telefono, correo_electronico, contrasena) VALUES(:nom, :ap_paterno, :ap_materno, :telefono, :correo, :contrasena);";
                List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
                {
                    new NpgsqlParameter(":nom", dueno.Nombre ?? string.Empty),
                    new NpgsqlParameter(":ap_paterno", dueno.ApellidoPaterno ?? string.Empty),
                    new NpgsqlParameter(":ap_materno", dueno.ApellidoMaterno ?? string.Empty),
                    new NpgsqlParameter(":telefono", dueno.Telefono),
                    new NpgsqlParameter(":correo", dueno.Correo ?? string.Empty),
                    new NpgsqlParameter(":contrasena", dueno.Contrasena ?? string.Empty)
                };
                GetQuery(SQL, lstParams); // Ejecuta la consulta para agregar
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar dueño", ex); // Manejo de error
            }
        }

        // Obtener todos los dueños de la base de datos
        public List<Dueno> GetDuenos()
        {
            const string SQL = "SELECT * FROM public.dueno ORDER BY id_dueno ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Dueno> lstDueno = new List<Dueno>();

            // Si no hay registros, regresa la lista vacía
            if (tabla.Rows.Count < 1)
                return lstDueno;

            // Recorrer los resultados y agregarlos a la lista
            foreach (DataRow fila in tabla.Rows)
            {
                lstDueno.Add(new Dueno(
                    (int)fila["id_dueno"],
                    (string)fila["nombre"],
                    (string)fila["apellido_paterno"],
                    (string)fila["apellido_materno"],
                    (long)fila["telefono"],
                    (string)fila["correo_electronico"],
                    (string)fila["contrasena"]));
            }
            return lstDueno;
        }

        // Obtener un dueño por su ID
        public Dueno GetDuenosById(int id)
        {
            const string SQL = "SELECT * FROM dueno WHERE id_dueno = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            // Si no encuentra registros, regresa un objeto vacío
            if (tabla.Rows.Count < 1) return new Dueno();

            // Asigna los valores a un nuevo objeto Dueno
            DataRow row = tabla.Rows[0];
            return new Dueno
            {
                IdDueno = row["id_dueno"] != DBNull.Value ? (int)row["id_dueno"] : 0,
                Nombre = row["nombre"] != DBNull.Value ? (string)row["nombre"] : string.Empty,
                ApellidoPaterno = row["apellido_paterno"] != DBNull.Value ? (string)row["apellido_paterno"] : string.Empty,
                ApellidoMaterno = row["apellido_materno"] != DBNull.Value ? (string)row["apellido_materno"] : string.Empty,
                Telefono = row["telefono"] != DBNull.Value ? (long)row["telefono"] : 0,
                Correo = row["correo_electronico"] != DBNull.Value ? (string)row["correo_electronico"] : string.Empty,
                Contrasena = row["contrasena"] != DBNull.Value ? (string)row["contrasena"] : string.Empty
            };
        }

        // Editar la información de un dueño
        public void EditDueno(Dueno dueno)
        {
            const string SQL = "UPDATE dueno SET nombre=:nom, apellido_paterno=:ap_pat, apellido_materno=:ap_mat, telefono=:telefono, correo_electronico=:correo, contrasena=:contrasena WHERE id_dueno=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", dueno.IdDueno),
                new NpgsqlParameter(":nom", dueno.Nombre),
                new NpgsqlParameter(":ap_pat", dueno.ApellidoPaterno),
                new NpgsqlParameter(":ap_mat", dueno.ApellidoMaterno),
                new NpgsqlParameter(":telefono", dueno.Telefono),
                new NpgsqlParameter(":correo", dueno.Correo),
                new NpgsqlParameter(":contrasena", dueno.Contrasena)
            };
            GetQuery(SQL, lstParams); // Ejecuta la consulta de actualización
        }

        // Eliminar un dueño por su ID
        public void DeleteDueno(int id)
        {
            const string SQL = "DELETE FROM dueno WHERE id_dueno = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams); // Ejecuta la consulta de eliminación
        }
    }
}
