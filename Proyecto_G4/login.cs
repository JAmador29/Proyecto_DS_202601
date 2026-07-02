using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capa_Entidad;
using Capa_Negocio;


namespace Proyecto_G4
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtDocument.Focus();
            CentrarGroupBox();

        }

        private void Login_Resize(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void CentrarGroupBox()
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
            groupBox1.Top = (this.ClientSize.Height - groupBox1.Height) / 2;
        }

        /*private void linkUppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecuperarPass recuperar = new RecuperarPass();
            recuperar.ShowDialog();
        }*/

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
                    
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string documento = txtDocument.Text.Trim();
            string clave = txtPassword.Text;

            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(documento) || string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("Por favor ingrese documento y contraseña.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Buscamos el usuario SOLO por documento (la clave se verifica aparte con BCrypt)
            Usuario ousuario = new CN_Usuario().Listar()
                .Where(u => u.Documento.Trim() == documento)
                .FirstOrDefault();

            // Verificamos la clave usando BCrypt (compara el texto escrito contra el hash guardado)
            bool claveCorrecta = ousuario != null && BCrypt.Net.BCrypt.Verify(clave, ousuario.Clave);

            if (claveCorrecta)
            {
                MenuPrincipal form = new MenuPrincipal(ousuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        //Evento creado para que al cerrar el menu principal se muestre nuevamente el login    
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtDocument.Clear();
            txtPassword.Clear();
            this.Show();
        }
        private void txtDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y retroceso
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquea la entrada
            }
        }
    }
}