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

        private bool mostrarPassword = false;
        private CN_Usuario objCNUsuario = new CN_Usuario();

        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnIngresar;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtDocument.Focus();
            CentrarGroupBox();
            HacerCircular(pblogo);

            // Leemos los valores guardados en los Settings de la aplicación
            if (Properties.Settings.Default.RecordarUsuario)
            {
                txtDocument.Text = Properties.Settings.Default.UsuarioRecordado;
                chkrecordar.Checked = true;

                // Mueve el foco de escritura directo a la contraseña, ya que el usuario ya está lleno
                txtPassword.Select();
            }
            else
            {
                chkrecordar.Checked = false;
                txtDocument.Select(); // Foco en el usuario si no hay nada guardado
            }
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

        /*private void linkUppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecuperarPass recuperar = new RecuperarPass();
            recuperar.ShowDialog();
        }*/

        private void btncancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            List<Usuario> listaUsuarios = new CN_Usuario().Listar();

            Usuario ousuario = listaUsuarios
                .Where(u => u.Documento.Trim() == documento)
                .FirstOrDefault();

            // Verificamos la clave usando BCrypt (compara el texto escrito contra el hash guardado)
            bool claveCorrecta = ousuario != null && BCrypt.Net.BCrypt.Verify(clave, ousuario.Clave);

            if (claveCorrecta)
            {
                string mensajeBitacora;

                bool registro = objCNUsuario.Registrar_Bitacora(ousuario.IdUsuario, "LOGIN", $"IdUsuario={ousuario.IdUsuario}, Nombre={ousuario.NombreCompleto}", out mensajeBitacora);

                //Guardar o limpiar documento de usuario
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

                // Guardar físicamente los cambios en el equipo del cliente
                Properties.Settings.Default.Save();


                MenuPrincipal form = new MenuPrincipal(ousuario);

                form.FormClosing += frm_closing;

                form.Show();
                this.Hide();
            }
            else
            {
                if (ousuario != null)
                {
                    if(objCNUsuario.Usuario_Bloqueado(ousuario.IdUsuario))
                    {
                        MessageBox.Show("Esta cuenta está bloqueada. Por favor, contacte al administrador.", "Cuenta Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int intentos = objCNUsuario.Intentos_Fallidos(ousuario.IdUsuario);

                    if (intentos >= 3)
                    {
                        objCNUsuario.Bloquear_Usuario(ousuario.IdUsuario);

                        string mensajeBitacora;

                        objCNUsuario.Registrar_Bitacora(ousuario.IdUsuario, "BLOQUEO", $"IdUsuario={ousuario.IdUsuario}, Nombre={ousuario.NombreCompleto}", out mensajeBitacora);
                        
                        MessageBox.Show("Se ha bloqueado la cuenta debido a múltiples intentos fallidos de inicio de sesión.", "Cuenta Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Usuario o contraseña incorrectos. Intentos {intentos} de 3.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
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

        private void btnmostrarclave_Click(object sender, EventArgs e)
        {
            mostrarPassword = !mostrarPassword;

            if (mostrarPassword)
            {
                txtPassword.PasswordChar = '\0'; // Muestra el texto
                btnmostrarclave.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
            }
            else
            {
                txtPassword.PasswordChar = '*'; // Oculta el texto
                btnmostrarclave.IconChar = FontAwesome.Sharp.IconChar.Eye;
            }
            txtPassword.Focus();
        }

        private void lnkOlvidarContraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmOlvidoContraseña OlvContr = new frmOlvidoContraseña();
            OlvContr.Show();
            this.Hide();
        }
    }
}