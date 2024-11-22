using Sanchez_Campos_Kevin_Alexis.Models;
using System;
using System.ComponentModel;
using System.Data;
using Npgsql;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class Mascota : ConexionMascotas
    {
        public int IdMascota { get; set; }
        public int id_dueno { get; set; }
        public int id_raza { get; set; }
        public int id_clasificacion { get; set; }
        public string nombre { get; set; }
        public string sexo { get; set; }


        public Mascota() { }

        // Constructor con ID
        public Mascota(int IdMascota)
        {
            this.IdMascota = IdMascota;
        }

        // Constructor completo
        public Mascota(int IdMascota, int id_dueno, int id_raza, int id_clasificacion, string nombre, string sexo) : this(IdMascota)
        {
            this.id_dueno = id_dueno;
            this.id_raza = id_raza;
            this.id_clasificacion = id_clasificacion;
            this.nombre = nombre;
            this.sexo = sexo;
        }

        // Obtener todas las mascotas de la base de datos

        public void AddMascota(Mascota mascota)
        {
            const string SQL = "INSERT INTO mascota(id_dueno, id_raza, id_clasificacion, nombre, sexo) VALUES(:id_dueno, :id_raza, :id_clasificacion, :nombre, :sexo);";

            // Creamos una lista de parámetros para la consulta SQL
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id_dueno", mascota.id_dueno),
                new NpgsqlParameter(":id_raza", mascota.id_raza),
                new NpgsqlParameter(":id_clasificacion", mascota.id_clasificacion),
                new NpgsqlParameter(":nombre", mascota.nombre),
                new NpgsqlParameter(":sexo", mascota.sexo)
            };
            // Ejecutamos la consulta en la base de datos
            GetQuery(SQL, lstParams);
        }
    



public List<Mascota> GetMascotas()
        {
            const string SQL = "SELECT * FROM public.mascota ORDER BY id_mascota ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Mascota> lstMascota = new List<Mascota>();

            // Si no hay registros, regresa la lista vacía
            if (tabla.Rows.Count < 1)
            {
                return lstMascota;
            }

            // Recorrer los resultados y agregarlos a la lista
            foreach (DataRow fila in tabla.Rows)
            {
                lstMascota.Add(new Mascota(
                    (int)fila["id_mascota"],
                    (int)fila["id_dueno"],
                    (int)fila["id_raza"],
                    (int)fila["id_clasificacion"],
                    (string)fila["nombre"],
                    (string)fila["sexo"]));
            }
            return lstMascota;
        }

        // Obtener una mascota por su ID
        public Mascota GetMascotaById(int id)
        {
            const string SQL = "SELECT * FROM mascota WHERE id_mascota = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            // Si no encuentra registros, regresa un objeto vacío
            if (tabla.Rows.Count < 1) return new Mascota();

            // Asigna los valores a un nuevo objeto Mascota
            DataRow row = tabla.Rows[0];
            return new Mascota
            {
                IdMascota = row["id_mascota"] != DBNull.Value ? (int)row["id_mascota"] : 0,
                id_dueno = row["id_dueno"] != DBNull.Value ? (int)row["id_dueno"] : 0,
                id_raza = row["id_raza"] != DBNull.Value ? (int)row["id_raza"] : 0,
                id_clasificacion = row["id_clasificacion"] != DBNull.Value ? (int)row["id_clasificacion"] : 0,
                nombre = row["nombre"] != DBNull.Value ? (string)row["nombre"] : string.Empty,
                sexo = row["sexo"] != DBNull.Value ? (string)row["sexo"] : string.Empty
            };
        }

        // Editar la información de una mascota
        public void EditMascota(Mascota mascota)
        {
            const string SQL = "UPDATE mascota SET id_dueno=:id_dueno, id_raza=:id_raza, id_clasificacion=:id_clasificacion, nombre=:nombre, sexo=:sexo WHERE id_mascota=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", mascota.IdMascota),
                new NpgsqlParameter(":id_dueno", mascota.id_dueno),
                new NpgsqlParameter(":id_raza", mascota.id_raza),
                new NpgsqlParameter(":id_clasificacion", mascota.id_clasificacion),
                new NpgsqlParameter(":nombre", mascota.nombre),
                new NpgsqlParameter(":sexo", mascota.sexo)
            };
            GetQuery(SQL, lstParams); // Ejecuta la consulta de actualización
        }

        // Eliminar una mascota por su ID
        public void DeleteMascota(int id)
        {
            const string SQL = "DELETE FROM mascota WHERE id_mascota = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams); // Ejecuta la consulta de eliminación
        }
    }
}
