using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;

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
                    // Agregamos un query limpio
                    string query = "select IdUsuario, Documento, NombreCompleto, Correo, Clave, IdRol, Estado from Usuario";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
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

                                // ¡NUEVO! Mapeo del objeto Rol relacionado (Ajustar según tus entidades)
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]) }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Devolvemos una lista vacía pero relanzamos el error para que puedas 
                    // ver en la consola de depuración exactamente qué falló (ej. problemas de Azure)
                    lista = new List<Usuario>();
                    throw ex;
                }
            }
            return lista;
        }
    }
}