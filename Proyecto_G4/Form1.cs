using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUser.Text.Trim();
            string password = txtpass.Text;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor ingresa el nombre de usuario y la contraseña.",
                                "Campos vacíos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (ValidarUsuario(nombreUsuario, password))
            {
                MenuPrincipal menuPrincipal = new MenuPrincipal(); 
                menuPrincipal.Show();
                this.Hide();
                menuPrincipal.FormClosed += (s, args) => Application.Exit();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.",
                                "Error de acceso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtpass.Clear();
                txtpass.Focus();
            }
        }

        private bool ValidarUsuario(string nombre, string contrasena)
        {
            string query = "SELECT COUNT(1) FROM Usuarios WHERE Nombre = @Nombre AND Contrasena = @Contrasena";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count == 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar con la base de datos: " + ex.Message,
                                    "Error de conexión",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void linkUppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecuperarPass recuperar = new RecuperarPass();
            recuperar.ShowDialog();
        }
    }
}