using System.Data.SqlClient;

namespace Proyecto_G4
{
    internal class Conexion
    {
        // Cadena de conexión única para toda la aplicación
        private static readonly string connectionString = "Server=DESKTOP-RL8BNUQ\\SQLEXPRESS;Database=BD__LAROBU_SUMBLIMA;Integrated Security=True;";

        // Propiedad pública para obtener la cadena
        public static string ConnectionString => connectionString;

        // Método para obtener una nueva conexión abierta (opcional)
        public static SqlConnection GetOpenConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        // Método para obtener una conexión cerrada (si se prefiere abrir después)
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}