using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Capa_Datos
{
    public class CD_Bitacora
    {
        /// <summary>
        /// Consulta y retorna el historial completo de la bitácora ordenado del más reciente al más antiguo.
        /// </summary>
        public List<Bitacora> Listar()
        {
            List<Bitacora> lista = new List<Bitacora>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT b.IdBitacora, b.TablaAfectada, b.Accion, b.IdUsuario, ");
                    query.AppendLine("ISNULL(u.NombreCompleto, 'SISTEMA / DESCONOCIDO') AS NombreUsuario, ");
                    query.AppendLine("b.Detalle, CONVERT(VARCHAR, b.FechaRegistro, 120) AS FechaRegistro ");
                    query.AppendLine("FROM BITACORA b ");
                    query.AppendLine("LEFT JOIN USUARIO u ON u.IdUsuario = b.IdUsuario ");
                    query.AppendLine("ORDER BY b.IdBitacora DESC");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion)
                    {
                        CommandType = CommandType.Text
                    };

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Bitacora()
                            {
                                IdBitacora = Convert.ToInt32(dr["IdBitacora"]),
                                TablaAfectada = dr["TablaAfectada"].ToString(),
                                Accion = dr["Accion"].ToString(),
                                IdUsuario = dr["IdUsuario"] != DBNull.Value ? Convert.ToInt32(dr["IdUsuario"]) : (int?)null,
                                Detalle = dr["Detalle"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                oUsuario = new Usuario()
                                {
                                    NombreCompleto = dr["NombreUsuario"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Bitacora>();
                }
            }
            return lista;
        }
    }
}