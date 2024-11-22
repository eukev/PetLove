using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class Vacunas : ConexionMascotas
    {
        public int IdVacunas { get; set; }            // ID de la vacuna
        public string NombreVacuna { get; set; }      // Nombre de la vacuna
        public string Descripcion { get; set; }       // Descripción de la vacuna
        public string FechaVacunas { get; set; }      // Fecha de administración de la vacuna

        public Vacunas() { }

        public Vacunas(int idVacunas, string nombreVacuna, string descripcion, string fechaVacunas)
        {
            this.IdVacunas = idVacunas;
            this.NombreVacuna = nombreVacuna;
            this.Descripcion = descripcion;
            this.FechaVacunas = fechaVacunas;
        }

        // Método para obtener todas las vacunas de la base de datos
        public List<Vacunas> GetVacunas()
        {
            const string SQL = "SELECT * FROM vacunas ORDER BY id_vacunas ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Vacunas> lstVacunas = new List<Vacunas>();

            if (tabla.Rows.Count < 1) return lstVacunas;

            foreach (DataRow fila in tabla.Rows)
            {
                lstVacunas.Add(new Vacunas(
                    (int)fila["id_vacunas"],
                    (string)fila["nombre_vacuna"],
                    (string)fila["descripcion"],
                    (string)fila["fecha_vacunas"]
                ));
            }
            return lstVacunas;
        }

        // Obtener una vacuna específica por ID
        public Vacunas GetVacunaById(int id)
        {
            const string SQL = "SELECT * FROM vacunas WHERE id_vacunas = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            if (tabla.Rows.Count < 1) return new Vacunas();

            DataRow row = tabla.Rows[0];
            return new Vacunas(
                (int)row["id_vacunas"],
                (string)row["nombre_vacuna"],
                (string)row["descripcion"],
                (string)row["fecha_vacunas"]
            );
        }

        // Método para agregar una nueva vacuna
        public void AddVacuna(Vacunas vacuna)
        {
            const string SQL = "INSERT INTO vacunas (nombre_vacuna, descripcion, fecha_vacunas) VALUES (:nombre_vacuna, :descripcion, :fecha_vacunas);";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":nombre_vacuna", vacuna.NombreVacuna),
                new NpgsqlParameter(":descripcion", vacuna.Descripcion),
                new NpgsqlParameter(":fecha_vacunas", vacuna.FechaVacunas)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para editar una vacuna existente
        public void EditVacuna(Vacunas vacuna)
        {
            const string SQL = "UPDATE vacunas SET nombre_vacuna=:nombre_vacuna, descripcion=:descripcion, fecha_vacunas=:fecha_vacunas WHERE id_vacunas=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", vacuna.IdVacunas),
                new NpgsqlParameter(":nombre_vacuna", vacuna.NombreVacuna),
                new NpgsqlParameter(":descripcion", vacuna.Descripcion),
                new NpgsqlParameter(":fecha_vacunas", vacuna.FechaVacunas)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para eliminar una vacuna por ID
        public void DeleteVacuna(int id)
        {
            const string SQL = "DELETE FROM vacunas WHERE id_vacunas = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams);
        }
    }
}
