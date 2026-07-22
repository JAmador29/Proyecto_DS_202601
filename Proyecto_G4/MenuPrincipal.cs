using Capa_Entidad;
using Capa_Negocio;
using FontAwesome.Sharp;
using Microsoft.Diagnostics.Tracing.Parsers.IIS_Trace;
using Proyecto_G4.Modales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class MenuPrincipal : Form
    {

        private static Usuario usuarioActual;
        private static IconMenuItem menuActivo = null;
        private static Form FormularioActivo = null;
        
        public MenuPrincipal(Usuario objusuario)
        {
            InitializeComponent();
            usuarioActual = objusuario;
        }

        //Evento para mostrar el respectivo formulario por menu
        private void AbrirFormulario(IconMenuItem menu, Form Fromulario)
        {
            if(menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(34, 36, 52);
                menuActivo.IconColor = Color.FromArgb(110, 81, 181);
                menuActivo.ForeColor = Color.FromArgb(110, 81, 181);
            }
            menu.BackColor = Color.FromArgb(66, 55, 105);
            menu.IconColor = Color.White;
            menu.ForeColor = Color.White;
            menuActivo = menu;

            if(FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = Fromulario;
            Fromulario.TopLevel = false;
            Fromulario.FormBorderStyle = FormBorderStyle.None;
            Fromulario.Dock = DockStyle.Fill;
            Fromulario.BackColor = Color.FromArgb(28, 25, 44);
            //Activar los Formularios en el panel contenedor
            contenedor.Controls.Add(Fromulario);
            Fromulario.Show();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

            HacerCircular(pblogo);

            //Para poder determianr que tipo de permisos tendra el usuario que se loguea al sistema.
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);
            foreach (IconMenuItem iconmenu in menu.Items)
            {

                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }

            }

            lblUsuario.Text = usuarioActual.NombreCompleto;
        }

        private void HacerCircular(PictureBox pictureBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);

            pictureBox.Region = new Region(path);
        }

        //MenuStrip con botones para aperturar formularios
        //Formulario de Usuarios
        private void menuusuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender,new frmUsuario());
        }
        //Formulario de Categorias
        private void subMenuCategory_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menugestor), new frmCategoria());
        }
        //Formulario de Productos
        private void SubMenuProducts_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menugestor), new frmProductos());
        }
        //Formularios de Ventas/Registrar Ventas
        private void SubMenuRegistrar_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuventas), new frmVentas(usuarioActual));
        }
        //Submenu de Detalle de Ventas
        private void SubMenuDV_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuventas), new frmDetalle_Venta());
        }
        //Formularios de Compras/Registrar Compras
        private void SubmenuRegistrarC_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menucompras), new frmCompras(usuarioActual));
        }
        //Submenu de Detalle de Compras
        private void SubMenuDC_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menucompras), new frmDetalle_Compra());
        }
        //Formulario de Proveedores
        private void menuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }
        //Formulario de Clientes
        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }
        
        //Formulario de acerca de
        private void menuacercade_Click(object sender, EventArgs e)
        {
            mdAcercade md = new mdAcercade();
            md.ShowDialog();
        }

        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menugestor, new frmNegocio());
        }

        private void submenureportecompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menureportes), new frmReporteCompras());
        }

        private void submenureporteventas_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario((menureportes), new frmReporteVentas());
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
            string mensajeBitacora;

            CN_Usuario objCNUsuario = new CN_Usuario();

            objCNUsuario.Registrar_Bitacora(usuarioActual.IdUsuario, "LOGOUT", $"IdUsuario={usuarioActual.IdUsuario}, Nombre={usuarioActual.NombreCompleto}", out mensajeBitacora);
        }
    }
}