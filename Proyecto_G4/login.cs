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
            List<Usuario> listaUsuarios = new CN_Usuario().Listar();

            Usuario ousuario = listaUsuarios
                .Where(u => u.Documento == txtDocument.Text.Trim() && u.Clave == txtPassword.Text)
                .FirstOrDefault();

            if (ousuario != null)
            {
                MenuPrincipal menuprincipal = new MenuPrincipal();
                menuprincipal.Show();
                this.Hide();
                menuprincipal.FormClosing += frm_closing;
            }
            else
            {
                MessageBox.Show("No se encontró el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDocument.Clear();
                txtPassword.Clear();
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
        }
    }
}