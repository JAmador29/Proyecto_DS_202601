using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class Clientes : Form
    {
        private const string connectionString = "Server=DESKTOP-RL8BNUQ\\SQLEXPRESS;Database=BD__LAROBU_SUMBLIMA;Integrated Security=True;";

        public Clientes()
        {
            InitializeComponent();
            this.Load += Clientes_Load;
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnNuevoCliente.Click += btnNuevoCliente_Click;

            // Botones del menú
            btnVentas.Click += btnVentas_Click;
            btnInventario.Click += btnInventario_Click;
            btnProductos.Click += btnProductos_Click;
            btnClientes.Click += btnClientes_Click;
            BtnProveedores.Click += btnProveedores_Click;
            btnReportes.Click += btnReportes_Click;
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes(string whereClause = "")
        {
            string query = @"SELECT 
                                ID_cliente AS ID,
                                Nombre,
                                RTN_DNI AS RTN,
                                Telefono,
                                Correo AS Email,
                                Direccion
                            FROM Clientes";
            if (!string.IsNullOrWhiteSpace(whereClause))
                query += " WHERE " + whereClause;
            query += " ORDER BY ID_cliente";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string where = "";

            // Filtro por ID
            if (int.TryParse(txtxId.Text.Trim(), out int id))
                where = $"ID_cliente = {id}";

            // Filtro por nombre (coincidencia parcial)
            string nombre = txtNombre.Text.Trim();
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                if (!string.IsNullOrEmpty(where)) where += " AND ";
                where += $"Nombre LIKE '%{nombre.Replace("'", "''")}%'";
            }

            if (string.IsNullOrWhiteSpace(where))
                CargarClientes();
            else
                CargarClientes(where);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtxId.Clear();
            txtNombre.Clear();
            CargarClientes();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            NuevoCliente nuevo = new NuevoCliente();
            nuevo.ShowDialog();
            CargarClientes(); 
        }

        // ================== Navegación ==================
        private void btnVentas_Click(object sender, EventArgs e)
        {
            Ventas ventas = new Ventas();
            ventas.Show();
            this.Hide(); // Opcional: ocultar actual
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            Inventario inventario = new Inventario();
            inventario.Show();
            this.Hide();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            Productos productos = new Productos();
            productos.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            // solo refresca la lista
            CargarClientes();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Proveedores proveedores = new Proveedores();
            proveedores.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Reportes reportes = new Reportes();
            reportes.Show();
            this.Hide();
        }

        private void Clientes_Load_1(object sender, EventArgs e)
        {

        }
    }
}