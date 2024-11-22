using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class Historial : ConexionMascotas
    {
        public int IdHistorial { get; set; }         // ID del historial
        public int IdMascota { get; set; }           // ID de la mascota asociada
        public int IdVacuna { get; set; }            // ID de la vacuna administrada
        public string EstadoSalud { get; set; }      // Estado de salud de la mascota

        public Historial() { }

        public Historial(int idHistorial, int idMascota, int idVacuna, string estadoSalud)
        {
            this.IdHistorial = idHistorial;
            this.IdMascota = idMascota;
            this.IdVacuna = idVacuna;
            this.EstadoSalud = estadoSalud;
        }

        // Método para obtener todos los historiales de vacunas de la base de datos
        public List<Historial> GetHistoriales()
        {
            const string SQL = "SELECT * FROM historial ORDER BY id_historial ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Historial> lstHistoriales = new List<Historial>();

            if (tabla.Rows.Count < 1) return lstHistoriales;

            foreach (DataRow fila in tabla.Rows)
            {
                lstHistoriales.Add(new Historial(
                    (int)fila["id_historial"],
                    (int)fila["id_mascota"],
                    (int)fila["id_vacuna"],
                    (string)fila["estado_salud"]
                ));
            }
            return lstHistoriales;
        }

        // Obtener un historial específico por ID
        public Historial GetHistorialById(int id)
        {
            const string SQL = "SELECT * FROM historial WHERE id_historial = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            if (tabla.Rows.Count < 1) return new Historial();

            DataRow row = tabla.Rows[0];
            return new Historial(
                (int)row["id_historial"],
                (int)row["id_mascota"],
                (int)row["id_vacuna"],
                (string)row["estado_salud"]
            );
        }

        // Método para agregar un nuevo historial
        public void AddHistorial(Historial historial)
        {
            const string SQL = "INSERT INTO historial (id_mascota, id_vacuna, estado_salud) VALUES (:id_mascota, :id_vacuna, :estado_salud);";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id_mascota", historial.IdMascota),
                new NpgsqlParameter(":id_vacuna", historial.IdVacuna),
                new NpgsqlParameter(":estado_salud", historial.EstadoSalud)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para editar un historial existente
        public void EditHistorial(Historial historial)
        {
            const string SQL = "UPDATE historial SET id_mascota=:id_mascota, id_vacuna=:id_vacuna, estado_salud=:estado_salud WHERE id_historial=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", historial.IdHistorial),
                new NpgsqlParameter(":id_mascota", historial.IdMascota),
                new NpgsqlParameter(":id_vacuna", historial.IdVacuna),
                new NpgsqlParameter(":estado_salud", historial.EstadoSalud)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para eliminar un historial por ID
        public void DeleteHistorial(int id)
        {
            const string SQL = "DELETE FROM historial WHERE id_historial = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams);
        }
    }
}
