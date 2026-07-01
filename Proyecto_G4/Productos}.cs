using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    /*public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
            // Asignar eventos manualmente para evitar errores del diseñador
            this.Load += async (s, e) => await CargarProductosAsync();
            button10.Click += async (s, e) => await BtnBuscar_Click(s, e);
            button11.Click += async (s, e) => await BtnLimpiar_Click(s, e);
            button7.Click += BtnNuevoProducto_Click;

            // Botones del menú (si existen en el diseñador)
            button1.Click += BtnVentas_Click;
            button2.Click += BtnInventario_Click;
            button3.Click += BtnProductos_Click;
            button4.Click += BtnClientes_Click;
            button5.Click += BtnProveedores_Click;
            button6.Click += BtnReportes_Click;
        }

        private async Task CargarProductosAsync(string whereClause = "")
        {
            string query = @"
                SELECT TOP 500
                    p.ID_Producto AS ID,
                    p.Nombre,
                    c.Descripcion AS Categoria,
                    p.Precio_Venta AS [Precio Venta],
                    p.Precio_costo AS [Precio Costo],
                    p.Descripcion
                FROM Productos p
                INNER JOIN Categoria c ON p.ID_categoria = c.ID_categoria";
            if (!string.IsNullOrWhiteSpace(whereClause))
                query += " WHERE " + whereClause;
            query += " ORDER BY p.ID_Producto";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    await conn.OpenAsync();
                    DataTable dt = new DataTable();
                    dt.Load(await cmd.ExecuteReaderAsync());
                    dataGridView1.DataSource = dt;
                    // Ajustar ancho de columnas
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task BtnBuscar_Click(object sender, EventArgs e)
        {
            string where = "";

            // Filtro por ID
            if (int.TryParse(textBox1.Text.Trim(), out int idProducto))
                where = $"p.ID_Producto = {idProducto}";

            // Filtro por nombre (coincidencia parcial)
            string nombre = textBox2.Text.Trim();
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                if (!string.IsNullOrEmpty(where)) where += " AND ";
                where += $"p.Nombre LIKE '%{nombre.Replace("'", "''")}%'";
            }

            if (string.IsNullOrWhiteSpace(where))
                await CargarProductosAsync();
            else
                await CargarProductosAsync(where);
        }

        private async Task BtnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            await CargarProductosAsync();
        }

        private void BtnNuevoProducto_Click(object sender, EventArgs e)
        {
            NuevoProducto nuevo = new NuevoProducto();
            nuevo.ShowDialog();
            _ = CargarProductosAsync(); // Refrescar después de cerrar
        }

        // ================== Navegación ==================
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
            // Recargar productos
            _ = CargarProductosAsync();
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
        }
    }*/
}