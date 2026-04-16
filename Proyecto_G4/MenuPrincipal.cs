using System;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            // ¡No borres esta línea! Es la que crea los botones físicamente
            InitializeComponent();

            // --- ENLACE DE EVENTOS MANUAL ---
            // Los agregamos aquí para que el programa sepa qué hacer al dar clic
            this.btnVenta.Click += new System.EventHandler(this.ButtonVentas_Click);
            this.btnInv.Click += new System.EventHandler(this.ButtonInventario_Click);
            this.btnProductos.Click += new System.EventHandler(this.ButtonProductos_Click);
            this.btnClientes.Click += new System.EventHandler(this.ButtonClientes_Click);
            this.btnProveedores.Click += new System.EventHandler(this.ButtonProveedores_Click);
            this.btnReporte.Click += new System.EventHandler(this.ButtonReportes_Click);
            this.button8.Click += new System.EventHandler(this.ButtonSalir_Click);
        }

        // --- MÉTODOS DE ACCIÓN ---

        private void ButtonVentas_Click(object sender, EventArgs e)
        {
            NuevaVenta frm = new NuevaVenta();
            frm.ShowDialog();
        }

        private void ButtonInventario_Click(object sender, EventArgs e)
        {
            Inventario inventario = new Inventario();
            inventario.ShowDialog();
        }

        private void ButtonProductos_Click(object sender, EventArgs e)
        {
            NuevoProducto nuevoProducto = new NuevoProducto();
            nuevoProducto.ShowDialog();
        }

        private void ButtonClientes_Click(object sender, EventArgs e)
        {
            NuevoCliente nuevoCliente = new NuevoCliente();
            nuevoCliente.ShowDialog();
        }

        private void ButtonProveedores_Click(object sender, EventArgs e)
        {
            NuevoProveedor nuevoProveedor = new NuevoProveedor();
            nuevoProveedor.ShowDialog();
        }

        private void ButtonReportes_Click(object sender, EventArgs e)
        {
            Reportes reportes = new Reportes();
            reportes.ShowDialog();
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}