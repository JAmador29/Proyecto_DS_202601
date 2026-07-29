using Capa_Negocio;
using System;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmOlvidoContraseña : Form
    {
        // Instancia a la Capa de Negocio
        private readonly CN_Usuario _cnUsuario = new CN_Usuario();

        public frmOlvidoContraseña()
        {
            InitializeComponent();
        }

        private void frmOlvidoContraseña_Load(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void frmOlvidoContraseña_Resize(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correoUsuario = txtCorreo.Text.Trim();

            // Llamada a la Capa de Negocio: 
            // Valida el formato, verifica que el correo exista, genera el token numérico y envía el SMTP.
            if (_cnUsuario.SolicitarCodigoRecuperacion(correoUsuario, out string codigoGenerado, out string mensajeError))
            {
                MostrarMensaje("Se ha enviado un código de recuperación a su correo electrónico.", "Código Enviado", MessageBoxIcon.Information);

                // Transferir correo y token generado al formulario de verificación
                frmCodigoVerificacion frmCodigo = new frmCodigoVerificacion(correoUsuario, codigoGenerado);
                frmCodigo.Show();
                this.Close();
            }
            else
            {
                MostrarMensaje(mensajeError, "Atención", MessageBoxIcon.Warning);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Close();
        }

        #region Métodos Auxiliares de Interfaz (Clean Code)

        private void CentrarGroupBox()
        {
            if (groupBox1 != null)
            {
                groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
                groupBox1.Top = (this.ClientSize.Height - groupBox1.Height) / 2;
            }
        }

        private void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }

        #endregion
    }
}