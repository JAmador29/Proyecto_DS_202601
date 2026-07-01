using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    /*public partial class RecuperarPass : Form
    {
        public RecuperarPass()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUserRecuperar.Text.Trim();
            string nuevaContrasena = txtnewpass.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(nuevaContrasena))
            {
                MessageBox.Show("Por favor, ingrese el nombre de usuario y la nueva contraseña.",
                                "Campos requeridos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el nombre de usuario existe en la base de datos
            if (UsuarioExiste(nombreUsuario))
            {
                // Actualizar la contraseña
                if (ActualizarContrasena(nombreUsuario, nuevaContrasena))
                {
                    MessageBox.Show("¡Contraseña actualizada con éxito! Ya puede iniciar sesión.",
                                    "Éxito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la contraseña. Intente de nuevo.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El nombre de usuario ingresado no existe en el sistema.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private bool UsuarioExiste(string nombre)
        {
            string query = "SELECT COUNT(1) FROM Usuarios WHERE Nombre = @Nombre";
            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el usuario: " + ex.Message,
                                    "Error de conexión",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private bool ActualizarContrasena(string nombre, string nuevaContrasena)
        {
            string query = "UPDATE Usuarios SET Contrasena = @NuevaContrasena WHERE Nombre = @Nombre";
            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@NuevaContrasena", nuevaContrasena);
                try
                {
                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la contraseña: " + ex.Message,
                                    "Error de base de datos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }*/
}