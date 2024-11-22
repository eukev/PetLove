using Npgsql;
using System.Data;

namespace Sanchez_Campos_Kevin_Alexis.Models
{
    public class ConexionMascotas
    {
        private string strConexion;

        // Constructor de la conexión, define la cadena de conexión a la base de datos
        protected ConexionMascotas()
        {
            // Configura los datos de conexión: servidor, usuario, base de datos y contraseña
            strConexion = "Server=localhost;Username=postgres;Database=mascotas;Password=root;";
            // Nota: Si el puerto no es el estándar, se debe añadir después del 'Server'
        }

        // Método para ejecutar una consulta y devolver los resultados en una tabla
        protected DataTable GetQuery(string sql)
        {
            DataTable tabla = new DataTable(); // Crea una tabla para los resultados
            NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter(); // Adaptador que ayuda a manejar los datos (aquí no se usa mucho)

            try
            {
                // "using" asegura que _con se cierre y elimine al salir del bloque
                using NpgsqlConnection _con = new NpgsqlConnection();
                _con.ConnectionString = strConexion;
                _con.Open(); // Abre la conexión a la base de datos

                // Crea el comando para ejecutar la consulta SQL
                using NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = _con;
                comando.CommandText = sql; // La consulta SQL que se va a ejecutar
                adaptador.SelectCommand = comando; // Asocia el comando al adaptador (no usado aquí)

                // Ejecuta la consulta y usa el lector para cargar los datos en la tabla
                using NpgsqlDataReader lector = comando.ExecuteReader();
                if (lector.HasRows) // Si hay resultados, carga los datos en la tabla
                {
                    tabla.Load(lector);
                }

                // Cierra el lector y la conexión
                lector.Close();
                _con.Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message); // Muestra el error si hay uno
            }
            return tabla; // Devuelve la tabla (llena o vacía)
        }

        // Método para ejecutar una consulta con parámetros y devolver los resultados en una tabla
        protected DataTable GetQuery(string sql, List<NpgsqlParameter> parametros)
        {
            DataTable tabla = new DataTable();
            NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter(); // Esta vez se usa para llenar la tabla

            using (NpgsqlConnection _con = new NpgsqlConnection()) // Conexión temporal a la base de datos
            {
                _con.ConnectionString = strConexion;
                _con.Open(); // Abre la conexión

                // Crea el comando para enviar la consulta y los parámetros
                using (NpgsqlCommand comando = new NpgsqlCommand())
                {
                    try
                    {
                        comando.Connection = _con; // Asocia el comando con la conexión
                        comando.CommandText = sql; // Asigna la consulta SQL al comando
                        comando.Parameters.Clear(); // Limpia los parámetros para evitar problemas

                        // Añade cada parámetro a la lista de parámetros del comando
                        foreach (NpgsqlParameter param in parametros)
                        {
                            comando.Parameters.Add(param);
                        }

                        adaptador.SelectCommand = comando; // Asocia el adaptador con el comando
                        adaptador.Fill(tabla); // Llena la tabla con los resultados
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message); // Muestra el error si hay uno
                    }
                }
                _con.Close(); // Cierra la conexión
            }
            return tabla; // Devuelve la tabla con los resultados
        }
    }
}
