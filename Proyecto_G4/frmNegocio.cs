using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmNegocio : Form
    {
        private readonly CN_Negocio _cnNegocio = new CN_Negocio();

        public frmNegocio()
        {
            InitializeComponent();
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            CargarLogo();
            CargarDatosNegocio();
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog { Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                byte[] byteImage = File.ReadAllBytes(dialog.FileName);

                if (_cnNegocio.ActualizarLogo(byteImage, out string mensaje))
                {
                    ActualizarLogoEnPantalla(byteImage);
                }
                else
                {
                    MostrarMensaje(mensaje, "Error", MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Negocio obj = new Negocio
            {
                Nombre = txtNombre.Text.Trim(),
                RTN = txtRTN.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
            };

            // Intentar guardar (CN_Negocio valida y retorna la respuesta)
            if (_cnNegocio.GuardarDatos(obj, out string mensaje, out string campoConError))
            {
                MostrarMensaje("Los cambios fueron guardados exitosamente.", "Mensaje", MessageBoxIcon.Information);
            }
            else
            {
                MostrarMensaje(mensaje, "Validación", MessageBoxIcon.Warning);
                EnfocarCampo(campoConError);
            }
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region Métodos Auxiliares de Interfaz

        private void CargarLogo()
        {
            byte[] byteImage = _cnNegocio.ObtenerLogo(out bool obtenido);
            if (obtenido)
            {
                ActualizarLogoEnPantalla(byteImage);
            }
        }

        private void CargarDatosNegocio()
        {
            Negocio datos = _cnNegocio.ObtenerDatos();
            if (datos == null)
                return;

            txtNombre.Text = datos.Nombre;
            txtRTN.Text = datos.RTN;
            txtDireccion.Text = datos.Direccion;
        }

        private void ActualizarLogoEnPantalla(byte[] byteImage)
        {
            Image logo = ByteToImage(byteImage);
            if (logo != null)
            {
                picLogo.Image?.Dispose(); // Liberar memoria de la imagen anterior
                picLogo.Image = logo;
            }
        }

        private Image ByteToImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                using (Image tempImage = Image.FromStream(ms))
                {
                    return new Bitmap(tempImage);
                }
            }
        }

        private void EnfocarCampo(string nombreCampo)
        {
            switch (nombreCampo)
            {
                case "Nombre":
                    txtNombre.Focus();
                    break;
                case "RTN":
                    txtRTN.Focus();
                    break;
                case "Direccion":
                    txtDireccion.Focus();
                    break;
            }
        }

        private void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }

        #endregion
    }
}