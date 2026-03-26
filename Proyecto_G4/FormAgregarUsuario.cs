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
    public partial class FormAgregarUsuario : Form
    {
        public FormAgregarUsuario()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged_1(object sender, EventArgs e) { }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string nombre = txtName.Text.Trim();
            string email = txtMail.Text.Trim();
            string password = txtPass.Text;
            string rol = txtRol.Text.Trim();
            string telefono = txtphone.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrEmpty(id)       ||
                string.IsNullOrEmpty(nombre)   ||
                string.IsNullOrEmpty(email)    ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(rol)      ||
                string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Por favor completa todos los campos.",
                                "Campos vacíos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Validar formato de email básico
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Por favor ingresa un correo válido.",
                                "Email inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Validar que ID sea numérico
            if (!int.TryParse(id, out _))
            {
                MessageBox.Show("El ID debe ser un número entero.",
                                "ID inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Validar que teléfono sea numérico
            if (!long.TryParse(telefono, out _))
            {
                MessageBox.Show("El teléfono debe contener solo números.",
                                "Teléfono inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Todo OK
            MessageBox.Show($"Usuario '{nombre}' agregado correctamente.",
                            "Éxito",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            LimpiarCampos();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtName.Clear();
            txtMail.Clear();
            txtPass.Clear();
            txtRol.Clear();
            txtphone.Clear();
        }
    }
}