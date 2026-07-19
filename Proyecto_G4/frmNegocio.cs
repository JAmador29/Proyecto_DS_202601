using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        public Image ByteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageBytes,0,imageBytes.Length);
            Image image = new Bitmap(ms);

            return image;
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_Negocio().ObtenerLogo(out obtenido);

            if (obtenido)
                picLogo.Image = ByteToImage(byteimage);

            Negocio datos = new CN_Negocio().ObtenerDatos();

            txtNombre.Text = datos.Nombre;
            txtRTN.Text = datos.RTN;
            txtDireccion.Text = datos.Direccion;

        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.FileName = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage = File.ReadAllBytes(oOpenFileDialog.FileName);
                bool respuesta = new CN_Negocio().ActualizarLogo(byteimage, out mensaje);

                if (respuesta)
                {
                    picLogo.Image = ByteToImage(byteimage);
                }
                else 
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private bool ValidarLongitudCampos()
        {

            if (txtNombre.Text.Trim().Length > 60)
            {
                MessageBox.Show("El campo 'Nombre' no puede superar los 60 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (txtRTN.Text.Trim().Length > 14)
            {
                MessageBox.Show("El campo 'RTN' es demasiado largo. El máximo permitido son 14 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRTN.Focus();
                return false;
            }

            if (txtDireccion.Text.Trim().Length > 200)
            {
                MessageBox.Show("El campo 'Dirección' es demasiado largo. El máximo permitido son 200 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            return true; // Todos los campos cumplen con la longitud permitida
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // Validación de Longitudes (Llamada a la nueva función externa)
            if (!ValidarLongitudCampos())
            {
                return; // Se detiene porque la función interna ya mostró el MessageBox y dio Focus
            }

            Negocio obj = new Negocio()
            {
                Nombre = txtNombre.Text,
                RTN = txtRTN.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
