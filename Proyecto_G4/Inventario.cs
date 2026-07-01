using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Inventario : Form
    {
        /*public Inventario()
        {
            InitializeComponent();
            // Asignar eventos manualmente (para evitar errores del diseñador)
            this.Load += async (s, e) => await CargarInventarioAsync();
            button10.Click += async (s, e) => await BtnBuscar_Click(s, e);
            button11.Click += async (s, e) => await BtnLimpiar_Click(s, e);
            button7.Click += BtnNuevoInventario_Click;
            button1.Click += BtnVentas_Click;
            button2.Click += BtnInventario_Click;
            button3.Click += BtnProductos_Click;
            button4.Click += BtnClientes_Click;
            button5.Click += BtnProveedores_Click;
            button6.Click += BtnReportes_Click;
        }

        private async Task CargarInventarioAsync(string whereClause = "")
        {
            string query = @"
                SELECT TOP 500
                    i.ID_Inventario AS ID,
                    i.ID_Producto,
                    i.Stock_actual,
                    i.Stock_min,
                    i.Ultima_actualizacion,
                    i.ID_usuario
                FROM Inventario i";
            if (!string.IsNullOrWhiteSpace(whereClause))
                query += " WHERE " + whereClause;
            query += " ORDER BY i.ID_Inventario DESC";

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
                    MessageBox.Show("Error al cargar inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task BtnBuscar_Click(object sender, EventArgs e)
        {
            string where = "";
            if (int.TryParse(textBox1.Text.Trim(), out int idInv))
                where = $"i.ID_Inventario = {idInv}";

            DateTime fecha = dateTimePicker1.Value.Date;
            if (!string.IsNullOrEmpty(where)) where += " AND ";
            where += $"CONVERT(DATE, i.Ultima_actualizacion) = '{fecha:yyyy-MM-dd}'";

            if (string.IsNullOrWhiteSpace(where))
                await CargarInventarioAsync();
            else
                await CargarInventarioAsync(where);
        }

        private async Task BtnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Today;
            await CargarInventarioAsync();
        }

        private void BtnNuevoInventario_Click(object sender, EventArgs e)
        {
            // Usar el formulario correcto (NuevoInventario.cs)
            NuevoInventario nuevo = new NuevoInventario();
            nuevo.ShowDialog();
            _ = CargarInventarioAsync();
        }

        // Navegación
        private void BtnVentas_Click(object sender, EventArgs e)
        {
            NuevaVenta nv = new NuevaVenta();
            nv.Show();
            this.Hide();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            _ = CargarInventarioAsync();
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            // Usar el formulario de listado de productos (Productos})
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
            Reportes rep = new Reportes();
            rep.Show();
            this.Hide();
        }*/
    }
}