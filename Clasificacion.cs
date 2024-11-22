using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class Clasificacion : ConexionMascotas
    {
        // Propiedades de la clasificación
        public int IdClasificacion { get; set; }
        public string TipoAnimal { get; set; }

        // Constructor vacío
        public Clasificacion() { }

        // Constructor con ID
        public Clasificacion(int IdClasificacion)
        {
            this.IdClasificacion = IdClasificacion;
        }

        // Constructor completo
        public Clasificacion(int IdClasificacion, string TipoAnimal) : this(IdClasificacion)
        {
            this.TipoAnimal = TipoAnimal;
        }

        // Agregar una nueva clasificación a la base de datos
        public void AddClasificacion(Clasificacion clasificacion)
        {
            try
            {
                const string SQL = "INSERT INTO public.clasificacion(tipo_animal) VALUES(:tipo_animal);";
                List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
                {
                    new NpgsqlParameter(":tipo_animal", clasificacion.TipoAnimal ?? string.Empty)
                };
                GetQuery(SQL, lstParams); // Ejecuta la consulta para agregar
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al agregar clasificación", ex); // Manejo de error
            }
        }

        // Obtener todas las clasificaciones de la base de datos
        public List<Clasificacion> GetClasificaciones()
        {
            const string SQL = "SELECT * FROM public.clasificacion ORDER BY id_clasificacion ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Clasificacion> lstClasificacion = new List<Clasificacion>();

            // Si no hay registros, regresa la lista vacía
            if (tabla.Rows.Count < 1)
                return lstClasificacion;

            // Recorrer los resultados y agregarlos a la lista
            foreach (DataRow fila in tabla.Rows)
            {
                lstClasificacion.Add(new Clasificacion(
                    (int)fila["id_clasificacion"],
                    (string)fila["tipo_animal"]));
            }
            return lstClasificacion;
        }

        // Obtener una clasificación por su ID
        public Clasificacion GetClasificacionById(int id)
        {
            const string SQL = "SELECT * FROM clasificacion WHERE id_clasificacion = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            // Si no encuentra registros, regresa un objeto vacío
            if (tabla.Rows.Count < 1) return new Clasificacion();

            // Asigna los valores a un nuevo objeto Clasificacion
            DataRow row = tabla.Rows[0];
            return new Clasificacion
            {
                IdClasificacion = row["id_clasificacion"] != DBNull.Value ? (int)row["id_clasificacion"] : 0,
                TipoAnimal = row["tipo_animal"] != DBNull.Value ? (string)row["tipo_animal"] : string.Empty
            };
        }

        // Editar la información de una clasificación
        public void EditClasificacion(Clasificacion clasificacion)
        {
            const string SQL = "UPDATE clasificacion SET tipo_animal=:tipo_animal WHERE id_clasificacion=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", clasificacion.IdClasificacion),
                new NpgsqlParameter(":tipo_animal", clasificacion.TipoAnimal)
            };
            GetQuery(SQL, lstParams); // Ejecuta la consulta de actualización
        }

        // Eliminar una clasificación por su ID
        public void DeleteClasificacion(int id)
        {
            const string SQL = "DELETE FROM clasificacion WHERE id_clasificacion = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams); // Ejecuta la consulta de eliminación
        }
    }
}
