using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;

namespace Proyecto_G4
{
    public partial class frmCambioContraseña : Form
    {
        private string correoUsuario;

        private CN_Usuario objCNUsuario = new CN_Usuario();

        public frmCambioContraseña(string correo)
        {
            InitializeComponent();
            correoUsuario = correo;
        }

        private void CentrarGroupBox()
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
            groupBox1.Top = (this.ClientSize.Height - groupBox1.Height) / 2;
        }

        private void frmCambioContraseña_Resize(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void frmCambioContraseña_Load(object sender, EventArgs e)
        {
            CentrarGroupBox();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtContraseña.Text;
            string confirmarContraseña = txtContraseña2.Text;

            if(string.IsNullOrWhiteSpace(nuevaContraseña) || string.IsNullOrWhiteSpace(confirmarContraseña))
            {
                MessageBox.Show("Por favor, complete ambos campos de contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nuevaContraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string mensaje;

            if (!objCNUsuario.Validar_Contraseña(nuevaContraseña, out mensaje))
            {
                MessageBox.Show(mensaje, "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(objCNUsuario.Validar_ContraseñaAntigua(correoUsuario, nuevaContraseña))
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la anterior. Por favor, elija una contraseña diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool resultado = objCNUsuario.Actualizar_Contraseña(correoUsuario, nuevaContraseña, out mensaje);

            if (resultado)
            {
                MessageBox.Show("La contraseña se actualizó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Login frmlog = new Login();
                frmlog.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
