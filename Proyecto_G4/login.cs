using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtDocumento.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidarUsuario(nombreUsuario, password, out int idUsuario))
            {
                /*// Guardar datos del usuario en la sesión global
                Sesion.IdUsuarioActual = idUsuario;
                Sesion.NombreUsuario = nombreUsuario;
                */
                Proyecto_G4.MenuPrincipal menuPrincipal = new Proyecto_G4.MenuPrincipal();
                menuPrincipal.Show();
                this.Hide();
                menuPrincipal.FormClosed += (s, args) => Application.Exit();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private bool ValidarUsuario(string nombre, string contrasena, out int idUsuario)
        {
            idUsuario = -1;
            string query = "SELECT ID_usuario FROM Usuarios WHERE Nombre = @Nombre AND Contrasena = @Contrasena";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out idUsuario))
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDocumento.Focus();
        }

        /*private void linkUppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecuperarPass recuperar = new RecuperarPass();
            recuperar.ShowDialog();
        }*/

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}