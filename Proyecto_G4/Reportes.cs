using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
            this.Load += async (s, e) => await CargarDetalleVentasAsync();
            button10.Click += async (s, e) => await BtnBuscar_Click(s, e);
            button11.Click += async (s, e) => await BtnLimpiar_Click(s, e);
            //button7.Click += BtnNuevoReporte_Click;

            button1.Click += BtnVentas_Click;
            button2.Click += BtnInventario_Click;
            button3.Click += BtnProductos_Click;
            button4.Click += BtnClientes_Click;
            button5.Click += BtnProveedores_Click;
            button6.Click += BtnReportes_Click;
        }

        private async Task CargarDetalleVentasAsync(string whereClause = "")
        {
            string query = @"
                SELECT TOP 500
                    dv.ID_detalle AS ID,
                    dv.ID_Venta,
                    dv.ID_Producto,
                    dv.Cantidad,
                    dv.Precio_unitario,
                    dv.Total
                FROM Detalle_Venta dv";
            if (!string.IsNullOrWhiteSpace(whereClause))
                query += " WHERE " + whereClause;
            query += " ORDER BY dv.ID_detalle DESC";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    await conn.OpenAsync();
                    DataTable dt = new DataTable();
                    dt.Load(await cmd.ExecuteReaderAsync());
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task BtnBuscar_Click(object sender, EventArgs e)
        {
            string where = "";
            if (int.TryParse(textBox1.Text.Trim(), out int idDetalle))
                where = $"dv.ID_detalle = {idDetalle}";

            DateTime fecha = dateTimePicker1.Value.Date;
            if (!string.IsNullOrEmpty(where)) where += " AND ";
            where += $"EXISTS (SELECT 1 FROM Ventas v WHERE v.ID_venta = dv.ID_Venta AND CONVERT(DATE, v.Fecha) = '{fecha:yyyy-MM-dd}')";

            if (string.IsNullOrWhiteSpace(where))
                await CargarDetalleVentasAsync();
            else
                await CargarDetalleVentasAsync(where);
        }

        private async Task BtnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Today;
            await CargarDetalleVentasAsync();
        }

        /*private void BtnNuevoReporte_Click(object sender, EventArgs e)
        {
            NuevoReporte nuevo = new NuevoReporte();
            nuevo.ShowDialog();
        }*/

        // Navegación
        private void BtnVentas_Click(object sender, EventArgs e)
        {
            Ventas nv = new Ventas();
            nv.Show();
            this.Hide();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.Show();
            this.Hide();
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            Productos prod = new Productos();
            prod.Show();
            this.Hide();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Hide();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            Proveedores prov = new Proveedores();
            prov.Show();
            this.Hide();
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            // Recargar
            _ = CargarDetalleVentasAsync();
        }
    }
}