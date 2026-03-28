using System;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Form1 : Form
    {
        // Credenciales hardcodeadas (Considerar DB en el futuro)
        public static string UsuarioValido = "admin";
        public static string PasswordValido = "1234";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUser.Text.Trim();
            string password = txtpass.Text;

            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña.",
                                "Campos vacíos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Verificación de credenciales
            if (usuario == UsuarioValido && password == PasswordValido)
            {
                // Asegúrate de que la clase Ventas exista en tu proyecto
                Ventas form2 = new Ventas();
                form2.Show();

                this.Hide();

                // Importante: Configura que al cerrar Ventas, se cierre toda la app
                form2.FormClosed += (s, args) => Application.Exit();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.",
                                "Error de acceso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                txtpass.Clear();
                txtpass.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void linkUppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Aquí pones lo que quieres que pase
            MessageBox.Show("Pantalla de recuperación de usuario/contraseña en proceso...");

            RecuperarPass recuperarPass =new RecuperarPass();
            recuperarPass.Show();
        }
    }
}
