using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.IdRol, u.Estado, r.IdRol, u.Bloqueado, r.Descripcion from USUARIO u");
                    query.AppendLine("inner join ROL r on r.IdRol = u.IdRol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                Bloqueado = Convert.ToBoolean(dr["Bloqueado"]),
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString() }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR REAL: " + ex.Message);
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }
            return idusuariogenerado;
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public static bool Correo_Existe(string correo)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                string query = "select count(*) from USUARIO where Correo = @Correo";

                using (SqlCommand cmd = new SqlCommand(query, oconexion))
                {
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correo;
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool Actualizar_Contraseña(string correo, string nuevaContraseña, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = "";

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    string query = "UPDATE USUARIO SET Clave = @Clave WHERE Correo = @Correo";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@Clave", nuevaContraseña);
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public string Obtener_ClaveCorreo(string correo)
        {
            string clave = "";

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                string query = "SELECT Clave FROM USUARIO WHERE Correo = @Correo";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Correo", correo);

                con.Open();

                object result = cmd.ExecuteScalar();

                if(result != null)
                {
                    clave = result.ToString();
                }
            }
            return clave;
        }

        public bool Registrar_Bitacora(int idUsuario, string accion, string detalle, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = "";

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    string query = "insert into BITACORA (TablaAfectada, Accion, IdUsuario, Detalle) values ('USUARIO', @Accion, @IdUsuario, @Detalle)";
                    
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Detalle", detalle);

                    con.Open();

                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Registrar_BitacoraSinUsuario(int idUsuario, string accion, string detalle, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = "";

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    string query = @"INSERT INTO BITACORA (TablaAfectada, Accion, IdUsuario, Detalle) VALUES ('USUARIO', @Accion, @IdUsuario, @Detalle)";

                    SqlCommand cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Detalle", detalle);

                    conexion.Open();

                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Usuario_Bloqueado(int idUsuario)
        {
            bool resultado = false;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                string query = "select Bloqueado from USUARIO where IdUsuario = @IdUsuario";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                
                con.Open();
                
                resultado = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            return resultado;
        }

        public int Aumentar_Intentos(int idUsuario)
        {
            int intentos = 0;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                string query = @"update USUARIO set IntentosFallidos = IntentosFallidos + 1 where IdUsuario = @IdUsuario; 
                                 select IntentosFallidos from USUARIO where IdUsuario = @IdUsuario;";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                con.Open();

                intentos = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return intentos;
        }

        public void Bloquear_Usuario(int idUsuario)
        {
            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                string query = "update USUARIO set Bloqueado = 1, FechaBloqueo = GETDATE() where IdUsuario = @IdUsuario";
                
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                
                con.Open();
                
                cmd.ExecuteNonQuery();
            }
        }

        public bool Desbloquear_Usuario(int idUsuario, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    string query = "update USUARIO set Bloqueado = 0, IntentosFallidos = 0, FechaBloqueo = NULL where IdUsuario = @IdUsuario";
                    
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    con.Open();

                    resultado = cmd.ExecuteNonQuery() > 0;

                    if (resultado)
                    {
                        Mensaje = "Usuario desbloqueado correctamente.";
                    }
                    else
                    {
                        Mensaje = "No se pudo desbloquear el usuario.";
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje += ex.Message;
                return false;
            }
            return resultado;
        }
    }
}