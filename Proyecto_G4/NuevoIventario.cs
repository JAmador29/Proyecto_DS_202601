using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class NuevoInventario : Form
    {
       /* public NuevoInventario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtIdInventario.Text) ||
                string.IsNullOrWhiteSpace(txtIdProducto.Text) ||
                string.IsNullOrWhiteSpace(txtStockActual.Text) ||
                string.IsNullOrWhiteSpace(txtStockMin.Text) ||
                string.IsNullOrWhiteSpace(txtIdUsuario.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que sean números enteros
            if (!int.TryParse(txtIdInventario.Text, out int idInventario) ||
                !int.TryParse(txtIdProducto.Text, out int idProducto) ||
                !int.TryParse(txtStockActual.Text, out int stockActual) ||
                !int.TryParse(txtStockMin.Text, out int stockMin) ||
                !int.TryParse(txtIdUsuario.Text, out int idUsuario))
            {
                MessageBox.Show("Los campos ID, Stock e ID Usuario deben ser números enteros.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime fecha = dtpUltimaActualizacion.Value;

            string query = @"INSERT INTO Inventario (ID_Inventario, ID_Producto, Stock_actual, Stock_min, Ultima_actualizacion, ID_usuario)
                             VALUES (@idInv, @idProd, @stockAct, @stockMin, @fecha, @idUser)";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idInv", idInventario);
                cmd.Parameters.AddWithValue("@idProd", idProducto);
                cmd.Parameters.AddWithValue("@stockAct", stockActual);
                cmd.Parameters.AddWithValue("@stockMin", stockMin);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@idUser", idUsuario);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro de inventario guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    // Error por duplicado de clave primaria u otra restricción
                    if (ex.Number == 2627) // Violación de PK
                        MessageBox.Show("Ya existe un registro con ese ID de inventario. Use otro ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (ex.Message.Contains("FK_Inv_Producto"))
                        MessageBox.Show("El ID de producto no existe en la tabla Productos.", "Error de clave foránea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (ex.Message.Contains("FK_Inv_Usuario"))
                        MessageBox.Show("El ID de usuario no existe en la tabla Usuarios.", "Error de clave foránea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }*/
    }
}