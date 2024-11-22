using Npgsql;
using System.Data;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    // La clase Raza representa una raza de mascotas en el sistema
    public class Raza : ConexionMascotas
    {
        // Representa el ID de la raza
        public int IdRaza { get; set; }

        // Representa el nombre de la mascota asociada a la raza
        public string NombreMascota { get; set; }

        // Constructor vacío
        public Raza() { }

        // Constructor con parámetros
        public Raza(int idRaza, string nombreMascota)
        {
            this.IdRaza = idRaza;
            this.NombreMascota = nombreMascota;
        }

        // Método para obtener todas las razas de mascotas desde la base de datos
        public List<Raza> GetRazas()
        {
            const string SQL = "SELECT * FROM raza ORDER BY id_raza ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Raza> lstRazas = new List<Raza>();

            if (tabla.Rows.Count < 1) return lstRazas;

            foreach (DataRow fila in tabla.Rows)
            {
                lstRazas.Add(new Raza(
                    (int)fila["id_raza"],
                    (string)fila["nombre_mascota"]
                ));
            }
            return lstRazas;
        }

        // Método para obtener una raza específica por ID
        public Raza GetRazaById(int id)
        {
            const string SQL = "SELECT * FROM raza WHERE id_raza = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            if (tabla.Rows.Count < 1) return new Raza();

            DataRow row = tabla.Rows[0];
            return new Raza(
                (int)row["id_raza"],
                (string)row["nombre_mascota"]
            );
        }

        // Método para agregar una nueva raza de mascota a la base de datos
        public void AddRaza(Raza raza)
        {
            const string SQL = "INSERT INTO raza (id_raza, nombre_mascota) VALUES (:id_raza, :nombre_mascota);";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id_raza", raza.IdRaza),
                new NpgsqlParameter(":nombre_mascota", raza.NombreMascota)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para editar una raza existente
        public void EditRaza(Raza raza)
        {
            const string SQL = "UPDATE raza SET id_raza=:id_raza, nombre_mascota=:nombre_mascota WHERE id_raza=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", raza.IdRaza),
                new NpgsqlParameter(":id_raza", raza.IdRaza),
                new NpgsqlParameter(":nombre_mascota", raza.NombreMascota)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para eliminar una raza por ID
        public void DeleteRaza(int id)
        {
            const string SQL = "DELETE FROM raza WHERE id_raza = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams);
        }
    }
}
