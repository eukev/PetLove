using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class Red_Social : ConexionMascotas
    {
        public int IdRedSocial { get; set; }
        public int IdDueno { get; set; }       // ID del dueño asociado
        public string Tipo { get; set; }       // Tipo de red social (Ej. Facebook, Instagram)
        public string Url { get; set; }        // URL del perfil en la red social

        public Red_Social() { }

        public Red_Social(int idRedSocial, int idDueno, string tipo, string url)
        {
            this.IdRedSocial = idRedSocial;
            this.IdDueno = idDueno;
            this.Tipo = tipo;
            this.Url = url;
        }

        // Método para obtener todas las redes sociales de la base de datos
        public List<Red_Social> GetRedesSociales()
        {
            const string SQL = "SELECT * FROM red_social ORDER BY id_red_social ASC;";
            DataTable tabla = GetQuery(SQL);
            List<Red_Social> lstRedesSociales = new List<Red_Social>();

            if (tabla.Rows.Count < 1) return lstRedesSociales;

            foreach (DataRow fila in tabla.Rows)
            {
                lstRedesSociales.Add(new Red_Social(
                    (int)fila["id_red_social"],
                    (int)fila["id_dueno"],
                    (string)fila["tipo"],
                    (string)fila["url"]
                ));
            }
            return lstRedesSociales;
        }

        // Obtener una red social específica por ID
        public Red_Social GetRedSocialById(int id)
        {
            const string SQL = "SELECT * FROM red_social WHERE id_red_social = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            DataTable tabla = GetQuery(SQL, lstParams);

            if (tabla.Rows.Count < 1) return new Red_Social();

            DataRow row = tabla.Rows[0];
            return new Red_Social(
                (int)row["id_red_social"],
                (int)row["id_dueno"],
                (string)row["tipo"],
                (string)row["url"]
            );
        }

        // Método para agregar una nueva red social
        public void AddRedSocial(Red_Social redSocial)
        {
            const string SQL = "INSERT INTO red_social (id_dueno, tipo, url) VALUES (:id_dueno, :tipo, :url);";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id_dueno", redSocial.IdDueno),
                new NpgsqlParameter(":tipo", redSocial.Tipo),
                new NpgsqlParameter(":url", redSocial.Url)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para editar una red social existente
        public void EditRedSocial(Red_Social redSocial)
        {
            const string SQL = "UPDATE red_social SET id_dueno=:id_dueno, tipo=:tipo, url=:url WHERE id_red_social=:id";
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":id", redSocial.IdRedSocial),
                new NpgsqlParameter(":id_dueno", redSocial.IdDueno),
                new NpgsqlParameter(":tipo", redSocial.Tipo),
                new NpgsqlParameter(":url", redSocial.Url)
            };
            GetQuery(SQL, lstParams);
        }

        // Método para eliminar una red social por ID
        public void DeleteRedSocial(int id)
        {
            const string SQL = "DELETE FROM red_social WHERE id_red_social = :id;";
            NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
            List<NpgsqlParameter> lstParams = new List<NpgsqlParameter> { paramId };
            GetQuery(SQL, lstParams);
        }
    }
}
