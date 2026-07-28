using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Negocio;

namespace Proyecto_G4
{
    public partial class Login : Form
    {
        private const int MaxIntentosFallidos = 3;

        private bool mostrarPassword;
        private readonly CN_Usuario usuarioNegocio;

        public Login()
        {
            InitializeComponent();

            usuarioNegocio = new CN_Usuario();
            AcceptButton = btnIngresar;
        }

        // ================================================================
        // EVENTOS DEL FORMULARIO
        // ================================================================

        private void Login_Load(object sender, EventArgs e)
        {
            CentrarFormularioLogin();
            HacerCircular(pblogo);
            CargarUsuarioRecordado();
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            CentrarFormularioLogin();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string documento = txtDocument.Text;
            string clave = txtPassword.Text;

            if (!ValidarCampos(documento, clave))
                return;

            documento = documento.Trim();

            Usuario usuario = BuscarUsuarioPorDocumento(documento);

            if (!ValidarAcceso(usuario, clave))
                return;

            IniciarSesion(usuario);
        }

        private void btnmostrarclave_Click(object sender, EventArgs e)
        {
            AlternarVisibilidadPassword();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            PermitirSoloNumeros(e);
        }

        private void lnkOlvidarContraseña_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            AbrirRecuperacionPassword();
        }

        private void MenuPrincipal_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            LimpiarLogin();
            Show();
        }

        // ================================================================
        // VALIDACIÓN DE CAMPOS
        // ================================================================

        private bool ValidarCampos(string documento, string clave)
        {
            if (AmbosCamposVacios(documento, clave))
            {
                MostrarAdvertencia("Por favor ingrese documento y contraseña.", "Campos vacíos");

                txtDocument.Focus();
                return false;
            }

            if (CampoVacio(documento))
            {
                MostrarAdvertencia("El campo de documento no puede estar vacío.", "Campo vacío");

                txtDocument.Focus();
                return false;
            }

            if (CampoVacio(clave))
            {
                MostrarAdvertencia("El campo de contraseña no puede estar vacío.", "Campo vacío");

                txtPassword.Focus();
                return false;
            }

            if (AmbosCamposTienenEspacioInicial(documento, clave))
            {
                MostrarAdvertencia("Los campos de documento y contraseña no pueden comenzar con espacios en blanco.", "Espacios no permitidos");

                txtDocument.Focus();
                return false;
            }

            if (TieneEspacioInicial(documento))
            {
                MostrarAdvertencia("El campo de documento no puede comenzar con espacios en blanco.", "Espacios no permitidos");

                txtDocument.Focus();
                return false;
            }

            if (TieneEspacioInicial(clave))
            {
                MostrarAdvertencia("El campo de contraseña no puede comenzar con espacios en blanco.", "Espacios no permitidos");

                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarAcceso(Usuario usuario, string clave)
        {
            if (usuario == null)
            {
                MostrarCredencialesIncorrectas();
                return false;
            }

            if (UsuarioEstaBloqueado(usuario))
            {
                MostrarError("Esta cuenta está bloqueada. Por favor, contacte al administrador.", "Cuenta bloqueada");

                ReiniciarCampoClave();
                return false;
            }

            if (!PasswordCorrecto(usuario, clave))
            {
                ProcesarIntentoFallido(usuario);
                ReiniciarCampoClave();
                return false;
            }

            if (!usuario.Estado)
            {
                MostrarError("Esta cuenta se encuentra inactiva. Contacte al administrador.", "Cuenta inactiva");

                ReiniciarCampoClave();
                return false;
            }

            return true;
        }

        private static bool AmbosCamposVacios(string documento, string clave)
        {
            return CampoVacio(documento) && CampoVacio(clave);
        }

        private static bool CampoVacio(string texto)
        {
            return string.IsNullOrWhiteSpace(texto);
        }

        private static bool AmbosCamposTienenEspacioInicial(
            string documento,
            string clave)
        {
            return TieneEspacioInicial(documento) && TieneEspacioInicial(clave);
        }

        private static bool TieneEspacioInicial(string texto)
        {
            return !string.IsNullOrEmpty(texto) && char.IsWhiteSpace(texto[0]);
        }

        // ================================================================
        // AUTENTICACIÓN
        // ================================================================

        private Usuario BuscarUsuarioPorDocumento(string documento)
        {
            return usuarioNegocio
                .Listar()
                .FirstOrDefault(usuario =>
                    string.Equals(
                        usuario.Documento?.Trim(),
                        documento,
                        StringComparison.Ordinal));
        }

        private bool UsuarioEstaBloqueado(Usuario usuario)
        {
            return usuarioNegocio.Usuario_Bloqueado(usuario.IdUsuario);
        }

        private static bool PasswordCorrecto(Usuario usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario.Clave))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(clave, usuario.Clave);
            }
            catch
            {
                return false;
            }
        }

        private void ProcesarIntentoFallido(Usuario usuario)
        {
            int intentos = usuarioNegocio.Intentos_Fallidos(
                usuario.IdUsuario);

            if (intentos >= MaxIntentosFallidos)
            {
                BloquearUsuario(usuario);
                return;
            }

            MostrarAdvertencia($"Usuario o contraseña incorrectos. Intentos {intentos} de {MaxIntentosFallidos}.", "Credenciales incorrectas");
        }

        private void BloquearUsuario(Usuario usuario)
        {
            usuarioNegocio.Bloquear_Usuario(usuario.IdUsuario);

            RegistrarBitacora(usuario, "BLOQUEO", $"IdUsuario={usuario.IdUsuario}, Nombre={usuario.NombreCompleto}");

            MostrarError("Se ha bloqueado la cuenta debido a múltiples intentos fallidos de inicio de sesión.", "Cuenta bloqueada");
        }

        private void IniciarSesion(Usuario usuario)
        {
            RegistrarBitacora(usuario, "LOGIN", $"IdUsuario={usuario.IdUsuario}, Nombre={usuario.NombreCompleto}");

            GuardarPreferenciaUsuario(usuario.Documento);
            AbrirMenuPrincipal(usuario);
        }

        private void RegistrarBitacora(Usuario usuario, string accion, string detalle)
        {
            string mensaje;

            usuarioNegocio.Registrar_Bitacora(usuario.IdUsuario, accion, detalle, out mensaje);
        }

        // ================================================================
        // PREFERENCIAS
        // ================================================================

        private void CargarUsuarioRecordado()
        {
            bool recordarUsuario = Properties.Settings.Default.RecordarUsuario;

            chkrecordar.Checked = recordarUsuario;

            if (recordarUsuario)
            {
                txtDocument.Text = Properties.Settings.Default.UsuarioRecordado;

                txtPassword.Focus();
                return;
            }

            txtDocument.Focus();
        }

        private void GuardarPreferenciaUsuario(string documento)
        {
            Properties.Settings.Default.RecordarUsuario = chkrecordar.Checked;

            Properties.Settings.Default.UsuarioRecordado = chkrecordar.Checked ? documento : string.Empty;

            Properties.Settings.Default.Save();
        }

        // ================================================================
        // NAVEGACIÓN
        // ================================================================

        private void AbrirMenuPrincipal(Usuario usuario)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal(usuario);

            menuPrincipal.FormClosing += MenuPrincipal_FormClosing;
            menuPrincipal.Show();

            Hide();
        }

        private void AbrirRecuperacionPassword()
        {
            frmOlvidoContraseña formulario = new frmOlvidoContraseña();

            formulario.Show();
            Hide();
        }

        // ================================================================
        // INTERFAZ
        // ================================================================

        private void CentrarFormularioLogin()
        {
            groupBox1.Left = (ClientSize.Width - groupBox1.Width) / 2;

            groupBox1.Top = (ClientSize.Height - groupBox1.Height) / 2;
        }

        private static void HacerCircular(PictureBox pictureBox)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);

                pictureBox.Region = new Region(path);
            }
        }

        private void AlternarVisibilidadPassword()
        {
            mostrarPassword = !mostrarPassword;

            txtPassword.PasswordChar = mostrarPassword ? '\0' : '*';

            btnmostrarclave.IconChar = mostrarPassword ? FontAwesome.Sharp.IconChar.EyeSlash : FontAwesome.Sharp.IconChar.Eye;

            txtPassword.Focus();
        }

        private static void PermitirSoloNumeros(KeyPressEventArgs e)
        {
            bool esNumero = char.IsDigit(e.KeyChar);
            bool esTeclaControl = char.IsControl(e.KeyChar);

            if (!esNumero && !esTeclaControl)
                e.Handled = true;
        }

        private void LimpiarLogin()
        {
            txtDocument.Clear();
            txtPassword.Clear();
            txtDocument.Focus();
        }

        private void ReiniciarCampoClave()
        {
            txtPassword.Clear();
            txtPassword.Focus();
        }

        // ================================================================
        // MENSAJES
        // ================================================================

        private void MostrarCredencialesIncorrectas()
        {
            MostrarAdvertencia("Usuario o contraseña incorrectos.", "Acceso denegado");

            ReiniciarCampoClave();
        }

        private static void MostrarAdvertencia(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void MostrarError(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}