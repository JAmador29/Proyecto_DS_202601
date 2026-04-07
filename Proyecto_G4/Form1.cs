using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Form1 : Form
    {
        // Cadena de conexión a SQL Server (ajústala si usas autenticación SQL)
        private const string connectionString = "Server=DESKTOP-RL8BNUQ\\SQLEXPRESS;Database=BD__LAROBU_SUMBLIMA;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUser.Text.Trim();
            string password = txtpass.Text;

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor ingresa el nombre de usuario y la contraseña.",
                                "Campos vacíos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Verificar credenciales contra la base de datos
            if (ValidarUsuario(nombreUsuario, password))
            {
                // Abrir el formulario principal (Ventas) pasando el usuario autenticado
                Ventas form2 = new Ventas(nombreUsuario);
                form2.Show();
                this.Hide();
                form2.FormClosed += (s, args) => Application.Exit();
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
            // Consulta que busca coincidencia exacta en Nombre y Contrasena
            string query = "SELECT COUNT(1) FROM Usuarios WHERE Nombre = @Nombre AND Contrasena = @Contrasena";

            using (SqlConnection conn = new SqlConnection(connectionString))
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
            // Abre el formulario de recuperación de contraseña
            RecuperarPass recuperar = new RecuperarPass();
            recuperar.ShowDialog();
        }
    }
}