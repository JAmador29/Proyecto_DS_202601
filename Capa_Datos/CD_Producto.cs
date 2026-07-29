using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdProducto, Codigo, Nombre, p.Descripcion, c.IdCategoria, c.Descripcion AS DescripcionCategoria, Stock, PrecioCompra, PrecioVenta, p.Estado FROM Producto p");
                    query.AppendLine("INNER JOIN Categoria c ON c.IdCategoria = p.IdCategoria");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion)
                    {
                        CommandType = CommandType.Text
                    };

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["DescripcionCategoria"].ToString()
                                },
                                Stock = Convert.ToInt32(dr["Stock"].ToString()),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR REAL: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idProductogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarProducto", oconexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    // >>> PARÁMETRO PARA LA BITÁCORA <<<
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario != null ? obj.oUsuario.IdUsuario : (object)DBNull.Value);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idProductogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idProductogenerado = 0;
                Mensaje = ex.Message;
            }
            return idProductogenerado;
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ModificarProducto", oconexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    // >>> PARÁMETRO PARA LA BITÁCORA <<<
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario != null ? obj.oUsuario.IdUsuario : (object)DBNull.Value);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }
    }
}