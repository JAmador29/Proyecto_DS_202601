using Capa_Entidad;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {

            Negocio obj = new Negocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select IdNegocio, Nombre, RTN, Direccion from NEGOCIO where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Negocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                Nombre = dr["Nombre"].ToString(),
                                RTN = dr["RTN"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };
                        }
                    }
                }

            }
            catch
            {
                obj = new Negocio();
            }

            return obj;
        }

        public bool GuardarDatos(Negocio objeto, out string mensaje)
        {

            mensaje = string.Empty;
            bool respuesta = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    // CORRECCIÓN: antes solo se hacía UPDATE, y si la fila
                    // IdNegocio = 1 no existía todavía en la tabla NEGOCIO,
                    // la instrucción afectaba 0 filas y el guardado fallaba
                    // en silencio. Ahora se hace UPSERT: si la fila existe
                    // se actualiza, si no existe se inserta.
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("IF EXISTS (SELECT 1 FROM NEGOCIO WHERE IdNegocio = 1)");
                    query.AppendLine("BEGIN");
                    query.AppendLine("    UPDATE NEGOCIO SET");
                    query.AppendLine("    Nombre = @nombre,");
                    query.AppendLine("    RTN = @rtn,");
                    query.AppendLine("    Direccion = @direccion");
                    query.AppendLine("    WHERE IdNegocio = 1");
                    query.AppendLine("END");
                    query.AppendLine("ELSE");
                    query.AppendLine("BEGIN");
                    query.AppendLine("    INSERT INTO NEGOCIO (IdNegocio, Nombre, RTN, Direccion)");
                    query.AppendLine("    VALUES (1, @nombre, @rtn, @direccion)");
                    query.AppendLine("END");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);

                    cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@rtn", objeto.RTN);
                    cmd.Parameters.AddWithValue("@direccion", objeto.Direccion);

                    cmd.CommandType = System.Data.CommandType.Text;

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        respuesta = true;
                        mensaje = "Los datos fueron guardados correctamente.";
                    }
                    else
                    {
                        mensaje = "No se pudo guardar los datos del negocio.";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public byte[] ObtenerLogo(out bool obtenido) //El out es para verificar si realmente estamos obteniendo la imagen
        {
            // CORRECCIÓN: antes se inicializaba en 'true' y solo se ponía en 'false'
            // dentro del catch. Si la fila no tenía logo (NULL o vacío), el método
            // seguía reportando obtenido = true con un arreglo de 0 bytes, lo cual
            // provocaba el ArgumentException al construir el Bitmap en frmNegocio.
            obtenido = false;
            byte[] LogoBytes = new byte[0];

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select Logo from NEGOCIO where IdNegocio = 1";

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // El campo puede venir NULL (DBNull) si nunca se ha
                            // subido un logo; en ese caso no se debe intentar el cast.
                            if (dr["Logo"] != DBNull.Value)
                            {
                                LogoBytes = (byte[])dr["Logo"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                LogoBytes = new byte[0];
                return LogoBytes;
            }

            // Solo se reporta 'obtenido = true' si realmente hay bytes de imagen.
            obtenido = LogoBytes != null && LogoBytes.Length > 0;

            return LogoBytes;
        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    // Mismo criterio de UPSERT que en GuardarDatos: si la fila
                    // IdNegocio = 1 no existe todavía, se crea con el logo
                    // recibido en lugar de fallar con "0 filas afectadas".
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("IF EXISTS (SELECT 1 FROM NEGOCIO WHERE IdNegocio = 1)");
                    query.AppendLine("BEGIN");
                    query.AppendLine("    UPDATE NEGOCIO SET Logo = @imagen WHERE IdNegocio = 1");
                    query.AppendLine("END");
                    query.AppendLine("ELSE");
                    query.AppendLine("BEGIN");
                    query.AppendLine("    INSERT INTO NEGOCIO (IdNegocio, Nombre, RTN, Direccion, Logo)");
                    query.AppendLine("    VALUES (1, '', '', '', @imagen)");
                    query.AppendLine("END");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image);
                    cmd.CommandType = System.Data.CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo!";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}