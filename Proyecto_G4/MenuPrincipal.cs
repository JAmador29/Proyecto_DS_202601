using Capa_Entidad;
using Capa_Negocio;
using FontAwesome.Sharp;
using Proyecto_G4.Modales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class MenuPrincipal : Form
    {
        // Se remueven los modificadores 'static' para evitar fugas de memoria e interferencia entre sesiones
        private readonly Usuario _usuarioActual;
        private IconMenuItem _menuActivo = null;
        private Form _formularioActivo = null;

        public MenuPrincipal(Usuario objusuario)
        {
            InitializeComponent();
            _usuarioActual = objusuario;
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            HacerCircular(pblogo);

            if (_usuarioActual != null)
            {
                lblUsuario.Text = _usuarioActual.NombreCompleto;

                // Filtrar permisos de usuario
                List<Permiso> listaPermisos = new CN_Permiso().Listar(_usuarioActual.IdUsuario);

                foreach (IconMenuItem iconmenu in menu.Items)
                {
                    bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                    if (!encontrado)
                    {
                        iconmenu.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gestiona la apertura de los formularios dentro del panel contenedor
        /// y el cambio de estilos visuales en el menú activo.
        /// </summary>
        private void AbrirFormulario(IconMenuItem menuSeleccionado, Form formulario)
        {
            if (_menuActivo != null)
            {
                _menuActivo.BackColor = Color.FromArgb(34, 36, 52);
                _menuActivo.IconColor = Color.FromArgb(110, 81, 181);
                _menuActivo.ForeColor = Color.FromArgb(110, 81, 181);
            }

            menuSeleccionado.BackColor = Color.FromArgb(66, 55, 105);
            menuSeleccionado.IconColor = Color.White;
            menuSeleccionado.ForeColor = Color.White;
            _menuActivo = menuSeleccionado;

            if (_formularioActivo != null)
            {
                _formularioActivo.Close();
            }

            _formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(28, 25, 44);

            contenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void HacerCircular(PictureBox pictureBox)
        {
            if (pictureBox == null || pictureBox.Width <= 0 || pictureBox.Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
                pictureBox.Region = new Region(path);
            }
        }

        // ==========================================
        // Eventos de Menús y Submenús
        // ==========================================

        private void menuusuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuario());
        }

        private void subMenuCategory_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menugestor, new frmCategoria());
        }

        private void SubMenuProducts_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menugestor, new frmProductos());
        }

        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menugestor, new frmNegocio());
        }

        private void SubMenuRegistrar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentas(_usuarioActual));
        }

        private void SubMenuDV_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmDetalle_Venta());
        }

        private void SubmenuRegistrarC_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompras(_usuarioActual));
        }

        private void SubMenuDC_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmDetalle_Compra());
        }

        private void menuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void submenureportecompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteCompras());
        }

        private void submenureporteventas_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteVentas());
        }

        private void menubitacora_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menubitacora, new frmbitacora());
        }

        private void menuacercade_Click(object sender, EventArgs e)
        {
            using (mdAcercade md = new mdAcercade())
            {
                md.ShowDialog();
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_usuarioActual != null)
            {
                CN_Usuario objCNUsuario = new CN_Usuario();
                objCNUsuario.Registrar_Bitacora(
                    _usuarioActual.IdUsuario,
                    "LOGOUT",
                    $"IdUsuario={_usuarioActual.IdUsuario}, Nombre={_usuarioActual.NombreCompleto}",
                    out string mensajeBitacora
                );
            }
        }
    }
}  