using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmCodigoVerificacion : Form
    {
        private readonly string correoUsuario;

        private string codigoCorrecto;

        public frmCodigoVerificacion(string correo, string codigo)
        {
            InitializeComponent();

            correoUsuario = correo;
            codigoCorrecto = codigo;
        }

        // ================================================================
        // EVENTOS DEL FORMULARIO
        // ================================================================

        private void frmCodigoVerificacion_Load(
            object sender,
            EventArgs e)
        {
            CentrarGroupBox();
            ConfigurarCamposCodigo();
            txtCodigo1.Focus();
        }

        private void frmCodigoVerificacion_Resize(
            object sender,
            EventArgs e)
        {
            CentrarGroupBox();
        }

        private void btnValidar_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidarCamposCodigo())
                return;

            string codigoIngresado = ObtenerCodigoIngresado();

            if (!CodigoEsCorrecto(codigoIngresado))
            {
                MostrarError(
                    "Código incorrecto. Intente nuevamente.",
                    "Código incorrecto");

                LimpiarCamposCodigo();
                return;
            }

            MostrarInformacion(
                "Código correcto. Continúe para cambiar su contraseña.",
                "Código confirmado");

            AbrirCambioContraseña();
        }

        private void btnVolverEnviar_Click(
            object sender,
            EventArgs e)
        {
            ReenviarCodigo();
        }

        private void btncancelar_Click(
            object sender,
            EventArgs e)
        {
            RegresarAlLogin();
        }

        // ================================================================
        // CONFIGURACIÓN DE CAMPOS
        // ================================================================

        private void ConfigurarCamposCodigo()
        {
            TextBox[] camposCodigo = ObtenerCamposCodigo();

            foreach (TextBox campo in camposCodigo)
            {
                campo.MaxLength = 1;
                campo.TextAlign = HorizontalAlignment.Center;
            }
        }

        private TextBox[] ObtenerCamposCodigo()
        {
            return new[]
            {
                txtCodigo1,
                txtCodigo2,
                txtCodigo3,
                txtCodigo4,
                txtCodigo5,
                txtCodigo6
            };
        }

        // ================================================================
        // VALIDACIÓN DEL CÓDIGO
        // ================================================================

        private bool ValidarCamposCodigo()
        {
            TextBox campoVacio = ObtenerPrimerCampoVacio();

            if (campoVacio == null)
                return true;

            MostrarAdvertencia(
                "Por favor, complete todos los campos del código.",
                "Campos incompletos");

            campoVacio.Focus();
            return false;
        }

        private TextBox ObtenerPrimerCampoVacio()
        {
            foreach (TextBox campo in ObtenerCamposCodigo())
            {
                if (string.IsNullOrWhiteSpace(campo.Text))
                    return campo;
            }

            return null;
        }

        private string ObtenerCodigoIngresado()
        {
            return string.Concat(
                txtCodigo1.Text,
                txtCodigo2.Text,
                txtCodigo3.Text,
                txtCodigo4.Text,
                txtCodigo5.Text,
                txtCodigo6.Text);
        }

        private bool CodigoEsCorrecto(string codigoIngresado)
        {
            return string.Equals(
                codigoIngresado,
                codigoCorrecto,
                StringComparison.Ordinal);
        }

        // ================================================================
        // REENVÍO DEL CÓDIGO
        // ================================================================

        private void ReenviarCodigo()
        {
            string nuevoCodigo = GenerarCodigoVerificacion();

            try
            {
                EnviarCodigoCorreo(
                    correoUsuario,
                    nuevoCodigo);

                /*
                 * El código correcto se reemplaza solamente después
                 * de confirmar que el correo fue enviado correctamente.
                 */
                codigoCorrecto = nuevoCodigo;

                LimpiarCamposCodigo();

                MostrarInformacion(
                    "El código fue enviado nuevamente a su correo.",
                    "Código enviado");
            }
            catch (SmtpException ex)
            {
                MostrarError(
                    $"No se pudo enviar el código por correo.\n\n{ex.Message}",
                    "Error de envío");
            }
            catch (Exception ex)
            {
                MostrarError(
                    $"Ocurrió un error al reenviar el código.\n\n{ex.Message}",
                    "Error");
            }
        }

        private static string GenerarCodigoVerificacion()
        {
            /*
             * Next(100000, 1000000) genera códigos
             * desde 100000 hasta 999999.
             */
            return new Random()
                .Next(100000, 1000000)
                .ToString();
        }

        private void EnviarCodigoCorreo(
            string destino,
            string codigo)
        {
            string correoOrigen =
                ConfigurationManager.AppSettings["CorreoSoporte"];

            string claveAplicacion =
                ConfigurationManager.AppSettings["ClaveAppCorreo"];

            ValidarConfiguracionCorreo(
                correoOrigen,
                claveAplicacion);

            using (MailMessage mensaje = CrearMensajeCorreo(
                       correoOrigen,
                       destino,
                       codigo))
            using (SmtpClient smtp = CrearClienteSmtp(
                       correoOrigen,
                       claveAplicacion))
            {
                smtp.Send(mensaje);
            }
        }

        private static MailMessage CrearMensajeCorreo(
            string correoOrigen,
            string destino,
            string codigo)
        {
            MailMessage mensaje = new MailMessage
            {
                From = new MailAddress(
                    correoOrigen,
                    "Soporte Loboru Sublima"),

                Subject =
                    "Código de recuperación - Loboru Sublima",

                Body =
                    $"Tu código de recuperación es: {codigo}",

                IsBodyHtml = false
            };

            mensaje.To.Add(destino);

            return mensaje;
        }

        private static SmtpClient CrearClienteSmtp(
            string correoOrigen,
            string claveAplicacion)
        {
            return new SmtpClient(
                "smtp.gmail.com",
                587)
            {
                Credentials = new NetworkCredential(
                    correoOrigen,
                    claveAplicacion),

                EnableSsl = true
            };
        }

        private static void ValidarConfiguracionCorreo(
            string correoOrigen,
            string claveAplicacion)
        {
            if (string.IsNullOrWhiteSpace(correoOrigen))
            {
                throw new ConfigurationErrorsException(
                    "No se encontró la configuración CorreoSoporte.");
            }

            if (string.IsNullOrWhiteSpace(claveAplicacion))
            {
                throw new ConfigurationErrorsException(
                    "No se encontró la configuración ClaveAppCorreo.");
            }
        }

        // ================================================================
        // CONTROL DE LOS TEXTBOX
        // ================================================================

        private void SoloNumeros_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            bool esNumero = char.IsDigit(e.KeyChar);
            bool esControl = char.IsControl(e.KeyChar);

            if (!esNumero && !esControl)
                e.Handled = true;
        }

        private static void MoverAlSiguienteCampo(
            TextBox campoActual,
            TextBox campoSiguiente)
        {
            if (campoActual.Text.Length == 1)
                campoSiguiente.Focus();
        }

        private void txtCodigo1_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCodigo2_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCodigo3_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCodigo4_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCodigo5_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCodigo6_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCodigo1_TextChanged(
            object sender,
            EventArgs e)
        {
            MoverAlSiguienteCampo(
                txtCodigo1,
                txtCodigo2);
        }

        private void txtCodigo2_TextChanged(
            object sender,
            EventArgs e)
        {
            MoverAlSiguienteCampo(
                txtCodigo2,
                txtCodigo3);
        }

        private void txtCodigo3_TextChanged(
            object sender,
            EventArgs e)
        {
            MoverAlSiguienteCampo(
                txtCodigo3,
                txtCodigo4);
        }

        private void txtCodigo4_TextChanged(
            object sender,
            EventArgs e)
        {
            MoverAlSiguienteCampo(
                txtCodigo4,
                txtCodigo5);
        }

        private void txtCodigo5_TextChanged(
            object sender,
            EventArgs e)
        {
            MoverAlSiguienteCampo(
                txtCodigo5,
                txtCodigo6);
        }

        // ================================================================
        // LIMPIEZA
        // ================================================================

        private void LimpiarCamposCodigo()
        {
            foreach (TextBox campo in ObtenerCamposCodigo())
                campo.Clear();

            txtCodigo1.Focus();
        }

        // ================================================================
        // NAVEGACIÓN
        // ================================================================

        private void AbrirCambioContraseña()
        {
            frmCambioContraseña formularioCambio =
                new frmCambioContraseña(correoUsuario);

            formularioCambio.Show();
            Close();
        }

        private void RegresarAlLogin()
        {
            Login formularioLogin = new Login();

            formularioLogin.Show();
            Close();
        }

        // ================================================================
        // INTERFAZ
        // ================================================================

        private void CentrarGroupBox()
        {
            groupBox1.Left =
                (ClientSize.Width - groupBox1.Width) / 2;

            groupBox1.Top =
                (ClientSize.Height - groupBox1.Height) / 2;
        }

        // ================================================================
        // MENSAJES
        // ================================================================

        private static void MostrarAdvertencia(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private static void MostrarInformacion(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static void MostrarError(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}