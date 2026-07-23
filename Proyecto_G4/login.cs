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
using System.Drawing.Drawing2D;


namespace Proyecto_G4
{
    public partial class Login : Form
    {
        // Cantidad máxima de intentos fallidos permitidos antes de bloquear la cuenta.
        private const int MAX_INTENTOS_FALLIDOS = 3;

        private bool mostrarPassword = false;
        private readonly CN_Usuario objCNUsuario = new CN_Usuario();

        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        // ------------------------------------------------------------------
        // Ciclo de vida / UI
        // ------------------------------------------------------------------

        private void Login_Load(object sender, EventArgs e)
        {
            txtDocument.Focus();
            CentrarGroupBox();
            HacerCircular(pblogo);
            CargarUsuarioRecordado();
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

        private void HacerCircular(PictureBox pictureBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Region = new Region(path);
        }

        private void CargarUsuarioRecordado()
        {
            if (Properties.Settings.Default.RecordarUsuario)
            {
                txtDocument.Text = Properties.Settings.Default.UsuarioRecordado;
                chkrecordar.Checked = true;
                txtPassword.Select(); // El usuario ya está lleno, el foco va a la clave
            }
            else
            {
                chkrecordar.Checked = false;
                txtDocument.Select();
            }
        }

        private void btnmostrarclave_Click(object sender, EventArgs e)
        {
            mostrarPassword = !mostrarPassword;

            if (mostrarPassword)
            {
                txtPassword.PasswordChar = '\0';
                btnmostrarclave.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
            }
            else
            {
                txtPassword.PasswordChar = '*';
                btnmostrarclave.IconChar = FontAwesome.Sharp.IconChar.Eye;
            }
            txtPassword.Focus();
        }

        private void txtDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y retroceso
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkOlvidarContraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmOlvidoContraseña OlvContr = new frmOlvidoContraseña();
            OlvContr.Show();
            this.Hide();
        }

        // Evento creado para que al cerrar el menú principal se muestre nuevamente el login
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtDocument.Clear();
            txtPassword.Clear();
            this.Show();
        }

        // ------------------------------------------------------------------
        // Autenticación
        // ------------------------------------------------------------------

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string documento = txtDocument.Text.Trim();
            string clave = txtPassword.Text;

            if (!CamposCompletos(documento, clave))
            {
                MostrarAdvertencia("Por favor ingrese documento y contraseña.", "Campos vacíos");
                return;
            }

            Usuario usuario = BuscarUsuarioPorDocumento(documento);

            if (usuario != null && CredencialesValidas(usuario, clave))
            {
                if (!usuario.Estado)
                {
                    MostrarError("Esta cuenta se encuentra inactiva. Contacte al administrador.", "Cuenta Inactiva");
                    ReiniciarCampoClave();
                    return;
                }

                IniciarSesion(usuario);
            }
            else
            {
                ManejarLoginFallido(usuario);
            }
        }

        private bool CamposCompletos(string documento, string clave)
        {
            return !string.IsNullOrWhiteSpace(documento) && !string.IsNullOrWhiteSpace(clave);
        }

        private Usuario BuscarUsuarioPorDocumento(string documento)
        {
            List<Usuario> listaUsuarios = objCNUsuario.Listar();

            return listaUsuarios
                .Where(u => u.Documento.Trim() == documento)
                .FirstOrDefault();
        }

        private bool CredencialesValidas(Usuario usuario, string clave)
        {
            return BCrypt.Net.BCrypt.Verify(clave, usuario.Clave);
        }

        private void IniciarSesion(Usuario usuario)
        {
            string mensajeBitacora;
            objCNUsuario.Registrar_Bitacora(usuario.IdUsuario, "LOGIN", $"IdUsuario={usuario.IdUsuario}, Nombre={usuario.NombreCompleto}", out mensajeBitacora);

            GuardarPreferenciaRecordarUsuario(usuario.Documento);

            AbrirMenuPrincipal(usuario);
        }

        private void GuardarPreferenciaRecordarUsuario(string documento)
        {
            if (chkrecordar.Checked)
            {
                Properties.Settings.Default.UsuarioRecordado = documento;
                Properties.Settings.Default.RecordarUsuario = true;
            }
            else
            {
                Properties.Settings.Default.UsuarioRecordado = string.Empty;
                Properties.Settings.Default.RecordarUsuario = false;
            }

            Properties.Settings.Default.Save();
        }

        private void AbrirMenuPrincipal(Usuario usuario)
        {
            MenuPrincipal form = new MenuPrincipal(usuario);
            form.FormClosing += frm_closing;
            form.Show();
            this.Hide();
        }

        private void ManejarLoginFallido(Usuario usuario)
        {
            if (usuario == null)
            {
                MostrarAdvertencia("Usuario o contraseña incorrectos.", "Mensaje");
                ReiniciarCampoClave();
                return;
            }

            if (objCNUsuario.Usuario_Bloqueado(usuario.IdUsuario))
            {
                MostrarError("Esta cuenta está bloqueada. Por favor, contacte al administrador.", "Cuenta Bloqueada");
                ReiniciarCampoClave();
                return;
            }

            RegistrarIntentoFallido(usuario);
            ReiniciarCampoClave();
        }

        private void RegistrarIntentoFallido(Usuario usuario)
        {
            int intentos = objCNUsuario.Intentos_Fallidos(usuario.IdUsuario);

            if (intentos >= MAX_INTENTOS_FALLIDOS)
            {
                objCNUsuario.Bloquear_Usuario(usuario.IdUsuario);

                string mensajeBitacora;
                objCNUsuario.Registrar_Bitacora(usuario.IdUsuario, "BLOQUEO", $"IdUsuario={usuario.IdUsuario}, Nombre={usuario.NombreCompleto}", out mensajeBitacora);

                MostrarError("Se ha bloqueado la cuenta debido a múltiples intentos fallidos de inicio de sesión.", "Cuenta Bloqueada");
            }
            else
            {
                MostrarAdvertencia($"Usuario o contraseña incorrectos. Intentos {intentos} de {MAX_INTENTOS_FALLIDOS}.", "Error");
            }
        }

        private void ReiniciarCampoClave()
        {
            txtPassword.Clear();
            txtPassword.Focus();
        }

        // ------------------------------------------------------------------
        // Helpers de mensajes (evitan repetir MessageBox.Show(...) por todo el archivo)
        // ------------------------------------------------------------------

        private void MostrarAdvertencia(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarError(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}