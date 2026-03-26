using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Form1 : Form
    {
        // Credenciales hardcodeadas
        private const string USUARIO_VALIDO = "Admin";
        private const string PASSWORD_VALIDO = "LaboruSublima";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtContraseña.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña.",
                                "Campos vacíos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            /*if (usuario == USUARIO_VALIDO && password == PASSWORD_VALIDO)
            {
                // Login exitoso: abre el siguiente formulario
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide(); 
                // Oculta el login
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.",
                                "Error de acceso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                txtContraseña.Clear();
                txtContraseña.Focus();
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void Link_Agregar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAgregarUsuario formAgregar = new FormAgregarUsuario();
            formAgregar.ShowDialog();
        }
    }
}