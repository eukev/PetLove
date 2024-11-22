using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class Cruce : ConexionMascotas
    {
        public int IdCruce { get; set; }
        public int id_mascota { get; set; }
        public int id_mascota2 { get; set; }
        public string fecha { get; set; }
        public string lugar { get; set; }

        public Cruce() { }

        public Cruce(int idCruce, int id_mascota, int id_mascota2, string fecha, string lugar)
        {
            this.IdCruce = idCruce;
            this.id_mascota = id_mascota;
            this.id_mascota2 = id_mascota2;
            this.fecha = fecha;
            this.lugar = lugar;
        }

        public List<Cruce> GetCruces()
        {
            const string SQL = "SELECT * FROM cruce ORDER BY id_cruce ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Cruce> lstCruce = new List<Cruce>();

            if (tabla.Rows.Count < 1) return lstCruce;

            foreach (DataRow fila in tabla.Rows)
            {
                lstCruce.Add(new Cruce(
                    (int)fila["id_cruce"],
                    (int)fila["id_mascota"],
                    (int)fila["id_mascota2"],
                    (string)fila["fecha"],
                    (string)fila["lugar"]
                ));
            }
            return lstCruce;
        }

        public Cruce GetCruceById(int id)
        {
            const string SQL = "SELECT * FROM cruce WHERE id_cruce = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            if (tabla.Rows.Count < 1) return new Cruce();

            DataRow row = tabla.Rows[0];
            return new Cruce(
                (int)row["id_cruce"],
                (int)row["id_mascota"],
                (int)row["id_mascota2"],
                (string)row["fecha"],
                (string)row["lugar"]
            );
        }

        public void AddCruce(Cruce cruce)
        {
            const string SQL = "INSERT INTO cruce (id_mascota, id_mascota2, fecha, lugar) VALUES (:id_mascota, :id_mascota2, :fecha, :lugar);";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id_mascota", cruce.id_mascota),
                new NpgsqlParameter(":id_mascota2", cruce.id_mascota2),
                new NpgsqlParameter(":fecha", cruce.fecha),
                new NpgsqlParameter(":lugar", cruce.lugar)
            };
            GetQuery(SQL, lstParams);
        }

        public void EditCruce(Cruce cruce)
        {
            const string SQL = "UPDATE cruce SET id_mascota=:id_mascota, id_mascota2=:id_mascota2, fecha=:fecha, lugar=:lugar WHERE id_cruce=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", cruce.IdCruce),
                new NpgsqlParameter(":id_mascota", cruce.id_mascota),
                new NpgsqlParameter(":id_mascota2", cruce.id_mascota2),
                new NpgsqlParameter(":fecha", cruce.fecha),
                new NpgsqlParameter(":lugar", cruce.lugar)
            };
            GetQuery(SQL, lstParams);
        }

        public void DeleteCruce(int id)
        {
            const string SQL = "DELETE FROM cruce WHERE id_cruce = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams);
        }
    }
}

