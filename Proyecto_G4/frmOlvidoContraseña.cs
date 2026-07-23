using Capa_Datos;
using System;
using System.Configuration;

using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class frmOlvidoContraseña : Form
    {
        public frmOlvidoContraseña()
        {
            InitializeComponent();
        }

        private string codigoGenerado;

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

        private void frmOlvidoContraseña_Resize(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void CentrarGroupBox()
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
            groupBox1.Top = (this.ClientSize.Height - groupBox1.Height) / 2;
        }

        private void frmOlvidoContraseña_Load(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correoUsuario = txtCorreo.Text.Trim();

            if (string.IsNullOrEmpty(correoUsuario))
            {
                MessageBox.Show("Por favor, ingrese su correo electrónico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(CD_Usuario.Correo_Existe(correoUsuario))
            {
                codigoGenerado = new Random().Next(100000, 999999).ToString();
                EnviarCodigoCorreo(correoUsuario, codigoGenerado);

                MessageBox.Show("Se ha enviado un código de recuperación a su correo electrónico.", "Código enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmCodigoVerificacion CodigoV = new frmCodigoVerificacion(correoUsuario, codigoGenerado);
                CodigoV.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El correo electrónico ingresado no está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
    }
}
