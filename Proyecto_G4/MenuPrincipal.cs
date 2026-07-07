using System;
using System.Windows.Forms;
using System.Linq;
using FontAwesome.Sharp;
using System.Drawing;

using Capa_Entidad;
using System.Net.Http;
using Capa_Negocio;
using System.Collections.Generic;

namespace Proyecto_G4
{
    public partial class MenuPrincipal : Form
    {

        private static Usuario usuarioActual;
        private static IconMenuItem menuActivo = null;
        private static Form FormularioActivo = null;
        public MenuPrincipal(Usuario objusuario)
        {
            usuarioActual = objusuario;

            InitializeComponent();

        }
        //Evento para mostrar el respectivo formulario por menu
        private void AbrirFormulario(IconMenuItem menu, Form Fromulario)
        {
            if(menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(64,64,64);
            }
            menu.BackColor = Color.FromArgb(45,45,45);
            menuActivo = menu;

            if(FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = Fromulario;
            Fromulario.TopLevel = false;
            Fromulario.FormBorderStyle = FormBorderStyle.None;
            Fromulario.Dock = DockStyle.Fill;
            Fromulario.BackColor = Color.MediumPurple;
            //Activar los Formularios en el panel contenedor
            contenedor.Controls.Add(Fromulario);
            Fromulario.Show();

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
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
            AbrirFormulario((menuventas), new frmVentas());
        }
        //Submenu de Detalle de Ventas
        private void SubMenuDV_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuventas), new frmDetalle_Venta());
        }
        //Formularios de Compras/Registrar Compras
        private void SubmenuRegistrarC_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menucompras), new frmCompras());
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
        //Formulario de Reportes
        private void menureportes_click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReportes());
        }

        private void menuacercade_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmAcercade());
        }
    }
}