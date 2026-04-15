using System;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            // Asignar eventos a los botones (si no están enlazados desde el diseñador)
            btnVentas.Click += ButtonVentas_Click;
            btnInv.Click += ButtonInventario_Click;
            btnProductos.Click += ButtonProductos_Click;
            btnProveedores.Click += ButtonProveedores_Click;
            btnReporte.Click += ButtonReportes_Click;
            button8.Click += ButtonSalir_Click;
            // Opcional: button7 podría mostrar información del usuario
            
        }

        private void ButtonVentas_Click(object sender, EventArgs e)
        {
            // Abrir formulario para crear una nueva venta
            NuevaVenta nuevaVenta = new NuevaVenta();
            nuevaVenta.ShowDialog(); // Modal
        }

        private void ButtonInventario_Click(object sender, EventArgs e)
        {
            // Abrir formulario de listado de inventario
            Inventario inventario = new Inventario();
            inventario.ShowDialog();
        }

        private void ButtonProductos_Click(object sender, EventArgs e)
        {
            // Abrir formulario para crear un nuevo producto
            NuevoProducto nuevoProducto = new NuevoProducto();
            nuevoProducto.ShowDialog();
        }

        private void ButtonClientes_Click(object sender, EventArgs e)
        {
            // Abrir formulario para crear un nuevo cliente
            NuevoCliente nuevoCliente = new NuevoCliente();
            nuevoCliente.ShowDialog();
        }

        private void ButtonProveedores_Click(object sender, EventArgs e)
        {
            // Abrir formulario para crear un nuevo proveedor
            NuevoProveedor nuevoProveedor = new NuevoProveedor();
            nuevoProveedor.ShowDialog();
        }

        private void ButtonReportes_Click(object sender, EventArgs e)
        {
            // Abrir formulario de reportes de ventas
            Reportes reportes = new Reportes();
            reportes.ShowDialog();
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicación
            Application.Exit();
        }
    }
}