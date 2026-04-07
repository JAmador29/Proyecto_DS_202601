using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Ventas : Form
    {
        private const string connectionString = "Server=DESKTOP-RL8BNUQ\\SQLEXPRESS;Database=BD__LAROBU_SUMBLIMA;Integrated Security=True;";

        // Constructor con parámetro opcional para recibir el usuario autenticado
        public Ventas(string usuario = "")
        {
            InitializeComponent();
            this.Load += Ventas_Load;
            btnBuscar.Click += btnBuscar_Click;
            btnClean.Click += btnClean_Click;
            //btnNewVenta.Click += btnNewVenta_Click;

            btnVenta.Click += btnVenta_Click;
            btnInventario.Click += btnInventario_Click;
            btnProductos.Click += btnProductos_Click;
            btnCliente.Click += btnCliente_Click;
            btnproveedores.Click += btnproveedores_Click;
            btnReporte.Click += btnReporte_Click;

            // Mostrar el usuario logueado en el lblUser (si se proporcionó)
            lblUser.Text = "Usuario: " + usuario;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void CargarVentas(string whereClause = "")
        {
            // Consulta que une Ventas con Clientes y Usuarios para mostrar nombres
            string query = @"
                SELECT 
                    v.ID_venta AS ID,
                    v.Num_factura AS [N° Factura],
                    c.Nombre AS Cliente,
                    u.Nombre AS Usuario,
                    v.Fecha,
                    v.Subtotal,
                    v.Impuesto,
                    v.Total,
                    CASE v.Metodo_pago 
                        WHEN 1 THEN 'Efectivo'
                        WHEN 2 THEN 'Tarjeta'
                        WHEN 3 THEN 'Transferencia'
                        ELSE 'Otro'
                    END AS [Método Pago],
                    CASE v.Estado 
                        WHEN 0 THEN 'Anulada'
                        WHEN 1 THEN 'Activa'
                        ELSE 'Desconocido'
                    END AS Estado
                FROM Ventas v
                INNER JOIN Clientes c ON v.ID_Cliente = c.ID_cliente
                INNER JOIN Usuarios u ON v.ID_Usuario = u.ID_usuario";

            if (!string.IsNullOrWhiteSpace(whereClause))
                query += " WHERE " + whereClause;
            query += " ORDER BY v.ID_venta DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    // Opcional: ocultar columnas que no necesites
                    if (dataGridView1.Columns.Contains("categoria"))
                        dataGridView1.Columns["categoria"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string where = "";

            if (int.TryParse(txtIdF.Text.Trim(), out int idVenta))
                where = $"v.ID_venta = {idVenta}";

            if (int.TryParse(txtNUmfact.Text.Trim(), out int numFactura))
            {
                if (!string.IsNullOrEmpty(where)) where += " AND ";
                where += $"v.Num_factura = {numFactura}";
            }

            DateTime fecha = dateTimePicker1.Value.Date;
            if (!string.IsNullOrEmpty(where)) where += " AND ";
            where += $"CONVERT(DATE, v.Fecha) = '{fecha:yyyy-MM-dd}'";

            if (string.IsNullOrWhiteSpace(where))
                CargarVentas();
            else
                CargarVentas(where);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtIdF.Clear();
            txtNUmfact.Clear();
            dateTimePicker1.Value = DateTime.Today;
            CargarVentas();
        }

        /*private void btnNewVenta_Click(object sender, EventArgs e)
        {
            NuevaVenta nueva = new NuevaVenta();
            nueva.ShowDialog();
            CargarVentas(); // Refrescar
        }*/

        // ================== Navegación ==================
        private void btnVenta_Click(object sender, EventArgs e)
        {
            CargarVentas(); // Recargar lista
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            Productos prod = new Productos();
            prod.ShowDialog();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.ShowDialog();
        }

        private void btnproveedores_Click(object sender, EventArgs e)
        {
            Proveedores prov = new Proveedores();
            prov.ShowDialog();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes rep = new Reportes();
            rep.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}