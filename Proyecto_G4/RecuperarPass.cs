using System;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class RecuperarPass : Form
    {
        public RecuperarPass()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUserRecuperar.Text.Trim();
            string nuevaContrasena = txtnewpass.Text.Trim();

            // 1. Validar que no haya campos vacíos
            if (string.IsNullOrEmpty(usuarioIngresado) || string.IsNullOrEmpty(nuevaContrasena))
            {
                MessageBox.Show("Por favor, ingrese el usuario y la nueva contraseña.",
                                "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Verificar que el usuario ingresado sea el correcto
            if (usuarioIngresado == Form1.UsuarioValido)
            {
                // 3. ¡Actualizar la contraseña en el Form1!
                Form1.PasswordValido = nuevaContrasena;

                MessageBox.Show("¡Contraseña actualizada con éxito! Ya puede iniciar sesión.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cerramos la ventana de recuperación
            }
            else
            {
                MessageBox.Show("El usuario ingresado no existe en el sistema.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}