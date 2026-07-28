using System;
using System.Windows.Forms;
using Capa_Negocio;

namespace Proyecto_G4
{
    public partial class frmCambioContraseña : Form
    {
        private readonly string correoUsuario;
        private readonly CN_Usuario usuarioNegocio;

        public frmCambioContraseña(string correo)
        {
            InitializeComponent();

            correoUsuario = correo;
            usuarioNegocio = new CN_Usuario();
        }

        // ================================================================
        // EVENTOS DEL FORMULARIO
        // ================================================================

        private void frmCambioContraseña_Load(object sender, EventArgs e)
        {
            CentrarFormulario();
            txtContraseña.Focus();
        }

        private void frmCambioContraseña_Resize(object sender, EventArgs e)
        {
            CentrarFormulario();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtContraseña.Text;
            string confirmarContraseña = txtContraseña2.Text;

            if (!ValidarCampos(nuevaContraseña, confirmarContraseña))
                return;

            if (!ValidarNuevaContraseña(nuevaContraseña))
                return;

            ActualizarContraseña(nuevaContraseña);
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            RegresarAlLogin();
        }

        // ================================================================
        // VALIDACIONES
        // ================================================================

        private bool ValidarCampos(
            string nuevaContraseña,
            string confirmarContraseña)
        {
            if (AmbosCamposVacios(nuevaContraseña, confirmarContraseña))
            {
                MostrarError(
                    "Por favor, complete ambos campos de contraseña.",
                    "Campos vacíos");

                txtContraseña.Focus();
                return false;
            }

            if (CampoVacio(nuevaContraseña))
            {
                MostrarError(
                    "Ingrese la nueva contraseña.",
                    "Campo vacío");

                txtContraseña.Focus();
                return false;
            }

            if (CampoVacio(confirmarContraseña))
            {
                MostrarError(
                    "Confirme la nueva contraseña.",
                    "Campo vacío");

                txtContraseña2.Focus();
                return false;
            }

            if (AmbosCamposTienenEspacioInicial(
                    nuevaContraseña,
                    confirmarContraseña))
            {
                MostrarAdvertencia(
                    "Los campos de contraseña no pueden comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txtContraseña.Focus();
                return false;
            }

            if (TieneEspacioInicial(nuevaContraseña))
            {
                MostrarAdvertencia(
                    "La nueva contraseña no puede comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txtContraseña.Focus();
                return false;
            }

            if (TieneEspacioInicial(confirmarContraseña))
            {
                MostrarAdvertencia(
                    "La confirmación de contraseña no puede comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txtContraseña2.Focus();
                return false;
            }

            if (nuevaContraseña != confirmarContraseña)
            {
                MostrarError(
                    "Las contraseñas no coinciden. Por favor, inténtelo nuevamente.",
                    "Contraseñas diferentes");

                ReiniciarConfirmacion();
                return false;
            }

            return true;
        }

        private bool ValidarNuevaContraseña(string nuevaContraseña)
        {
            if (!usuarioNegocio.Validar_Contraseña(
                    nuevaContraseña,
                    out string mensaje))
            {
                MostrarAdvertencia(
                    mensaje,
                    "Contraseña inválida");

                txtContraseña.Focus();
                return false;
            }

            if (EsContraseñaAnterior(nuevaContraseña))
            {
                MostrarError(
                    "La nueva contraseña no puede ser igual a la anterior. " +
                    "Por favor, elija una contraseña diferente.",
                    "Contraseña repetida");

                ReiniciarCampos();
                return false;
            }

            return true;
        }

        private bool EsContraseñaAnterior(string nuevaContraseña)
        {
            return usuarioNegocio.Validar_ContraseñaAntigua(
                correoUsuario,
                nuevaContraseña);
        }

        private static bool AmbosCamposVacios(
            string nuevaContraseña,
            string confirmarContraseña)
        {
            return CampoVacio(nuevaContraseña) &&
                   CampoVacio(confirmarContraseña);
        }

        private static bool CampoVacio(string texto)
        {
            return string.IsNullOrWhiteSpace(texto);
        }

        private static bool AmbosCamposTienenEspacioInicial(
            string nuevaContraseña,
            string confirmarContraseña)
        {
            return TieneEspacioInicial(nuevaContraseña) &&
                   TieneEspacioInicial(confirmarContraseña);
        }

        private static bool TieneEspacioInicial(string texto)
        {
            return !string.IsNullOrEmpty(texto) &&
                   char.IsWhiteSpace(texto[0]);
        }

        // ================================================================
        // ACTUALIZACIÓN DE CONTRASEÑA
        // ================================================================

        private void ActualizarContraseña(string nuevaContraseña)
        {
            bool resultado = usuarioNegocio.Actualizar_Contraseña(
                correoUsuario,
                nuevaContraseña,
                out string mensaje);

            if (!resultado)
            {
                MostrarError(
                    $"Error al actualizar la contraseña: {mensaje}",
                    "Error de actualización");

                return;
            }

            MostrarInformacion(
                "La contraseña se actualizó correctamente.",
                "Contraseña actualizada");

            RegresarAlLogin();
        }

        // ================================================================
        // NAVEGACIÓN
        // ================================================================

        private void RegresarAlLogin()
        {
            Login formularioLogin = new Login();
            formularioLogin.Show();

            Close();
        }

        // ================================================================
        // INTERFAZ
        // ================================================================

        private void CentrarFormulario()
        {
            groupBox1.Left =
                (ClientSize.Width - groupBox1.Width) / 2;

            groupBox1.Top =
                (ClientSize.Height - groupBox1.Height) / 2;
        }

        private void ReiniciarConfirmacion()
        {
            txtContraseña2.Clear();
            txtContraseña2.Focus();
        }

        private void ReiniciarCampos()
        {
            txtContraseña.Clear();
            txtContraseña2.Clear();
            txtContraseña.Focus();
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
    }
}