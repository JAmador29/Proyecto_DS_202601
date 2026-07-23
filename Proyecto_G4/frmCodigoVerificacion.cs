using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmCodigoVerificacion : Form
    {
        private string correoUsuario;
        private string codigoCorrecto;

        public frmCodigoVerificacion(string correo, string codigo)
        {
            InitializeComponent();
            correoUsuario = correo;
            codigoCorrecto = codigo;
        }

        private void CentrarGroupBox()
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
            groupBox1.Top = (this.ClientSize.Height - groupBox1.Height) / 2;
        }

        private void EnviarCodigoCorreo(string destino, string codigo)
        {
            string correoOrigen = ConfigurationManager.AppSettings["CorreoSoporte"];
            string claveApp = ConfigurationManager.AppSettings["ClaveAppCorreo"];


            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress(correoOrigen, "Soporte Loboru Sublima");
            mensaje.To.Add(destino);
            mensaje.Subject = "Código de recuperación - Loboru Sublima";
            mensaje.Body = $"Tú código de recuperación es: {codigo}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(correoOrigen, claveApp),
                EnableSsl = true

            };
            try
            {
                smtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo: " + ex.Message);
            }
        }

        private void Solo_Numeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCodigo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Solo_Numeros(sender, e);
        }

        private void txtCodigo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Solo_Numeros(sender, e);
        }

        private void txtCodigo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Solo_Numeros(sender, e);
        }

        private void txtCodigo4_KeyPress(object sender, KeyPressEventArgs e)
        {
            Solo_Numeros(sender, e);
        }

        private void txtCodigo5_KeyPress(object sender, KeyPressEventArgs e)
        {
            Solo_Numeros(sender, e);
        }

        private void txtCodigo6_KeyPress(object sender, KeyPressEventArgs e)
        {
            Solo_Numeros(sender, e);
        }

        private void Mover_Siguiente(TextBox actual, TextBox siguiente)
        {
            if(actual.Text.Length == 1)
            {
                siguiente.Focus();
            }
        }

        private void txtCodigo1_TextChanged(object sender, EventArgs e)
        {
            Mover_Siguiente(txtCodigo1, txtCodigo2);
        }

        private void txtCodigo2_TextChanged(object sender, EventArgs e)
        {
            Mover_Siguiente(txtCodigo2, txtCodigo3);
        }

        private void txtCodigo3_TextChanged(object sender, EventArgs e)
        {
            Mover_Siguiente(txtCodigo3, txtCodigo4);
        }

        private void txtCodigo4_TextChanged(object sender, EventArgs e)
        {
            Mover_Siguiente(txtCodigo4 , txtCodigo5);
        }

        private void txtCodigo5_TextChanged(object sender, EventArgs e)
        {
            Mover_Siguiente(txtCodigo5, txtCodigo6);
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo1.Text) || string.IsNullOrWhiteSpace(txtCodigo2.Text) ||
               string.IsNullOrWhiteSpace(txtCodigo3.Text) || string.IsNullOrWhiteSpace(txtCodigo4.Text) ||
               string.IsNullOrWhiteSpace(txtCodigo5.Text) || string.IsNullOrWhiteSpace(txtCodigo6.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos del código.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string codigoIngresado = txtCodigo1.Text + txtCodigo2.Text + txtCodigo3.Text + txtCodigo4.Text +
                txtCodigo5.Text + txtCodigo6.Text;

            if (codigoIngresado == codigoCorrecto)
            {
                MessageBox.Show("Código correcto. Continua para cambiar tu contraseña.", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmCambioContraseña frmCambio = new frmCambioContraseña(correoUsuario);
                frmCambio.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Código incorrecto. Intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolverEnviar_Click(object sender, EventArgs e)
        {
            codigoCorrecto = new Random().Next(100000, 999999).ToString();

            try
            {
                EnviarCodigoCorreo(correoUsuario, codigoCorrecto);

                MessageBox.Show("El código ha sido enviado nuevamente a su correo", "Codigo enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reenviar el código: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }

        private void frmCodigoVerificacion_Load(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void frmCodigoVerificacion_Resize(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }
    }
}
