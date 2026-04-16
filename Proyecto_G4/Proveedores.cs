using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
            // Asignar eventos manualmente para evitar errores del diseñador
            this.Load += async (s, e) => await CargarProveedoresAsync();
            button10.Click += async (s, e) => await BtnBuscar_Click(s, e);
            button11.Click += async (s, e) => await BtnLimpiar_Click(s, e);
            button7.Click += BtnNuevoProveedor_Click;

            // Botones del menú (si existen en el diseñador)
            button1.Click += BtnVentas_Click;
            button2.Click += BtnInventario_Click;
            button3.Click += BtnProductos_Click;
            button4.Click += BtnClientes_Click;
            button5.Click += BtnProveedores_Click;
            button6.Click += BtnReportes_Click;
        }

        private async Task CargarProveedoresAsync(string whereClause = "")
        {
            string query = @"
                SELECT TOP 500
                    ID_Proveedor AS ID,
                    Nombre_Proveedor AS Nombre,
                    RTN,
                    Telefono,
                    Contacto
                FROM Proveedores";
            if (!string.IsNullOrWhiteSpace(whereClause))
                query += " WHERE " + whereClause;
            query += " ORDER BY ID_Proveedor";

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
                    MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task BtnBuscar_Click(object sender, EventArgs e)
        {
            string where = "";

            // Filtro por ID
            if (int.TryParse(textBox1.Text.Trim(), out int idProveedor))
                where = $"ID_Proveedor = {idProveedor}";

            // Filtro por nombre (coincidencia parcial)
            string nombre = textBox2.Text.Trim();
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                if (!string.IsNullOrEmpty(where)) where += " AND ";
                where += $"Nombre_Proveedor LIKE '%{nombre.Replace("'", "''")}%'";
            }

            if (string.IsNullOrWhiteSpace(where))
                await CargarProveedoresAsync();
            else
                await CargarProveedoresAsync(where);
        }

        private async Task BtnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            await CargarProveedoresAsync();
        }

        private void BtnNuevoProveedor_Click(object sender, EventArgs e)
        {
            NuevoProveedor nuevo = new NuevoProveedor();
            nuevo.ShowDialog();
            _ = CargarProveedoresAsync(); // Refrescar después de cerrar
        }

        // ================== Navegación ==================
        private void BtnVentas_Click(object sender, EventArgs e)
        {
            NuevaVenta nv = new NuevaVenta();
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
            // Recargar proveedores
            _ = CargarProveedoresAsync();
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            Reportes rep = new Reportes();
            rep.Show();
            this.Hide();
        }
    }
}