using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class NuevoProducto : Form
    {
        private Button btnGuardar;
        private Button btnCancelar;

        public NuevoProducto()
        {
            InitializeComponent();
            CargarCategorias();
            CrearBotones();
            this.Load += NuevoProducto_Load;
        }

        private void CargarCategorias()
        {
            using (SqlConnection conn = Conexion.GetConnection())
            {
                string query = "SELECT ID_categoria, Descripcion FROM Categoria ORDER BY Descripcion";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox2.DisplayMember = "Descripcion";
                comboBox2.ValueMember = "ID_categoria";
                comboBox2.DataSource = dt;
                comboBox2.SelectedIndex = -1;
            }
        }

        private void CrearBotones()
        {
            // Botón Guardar
            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.BackColor = System.Drawing.Color.Azure;
            btnGuardar.Location = new System.Drawing.Point(50, 430);
            btnGuardar.Size = new System.Drawing.Size(120, 40);
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            // Botón Cancelar
            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = System.Drawing.Color.LightSalmon;
            btnCancelar.Location = new System.Drawing.Point(200, 430);
            btnCancelar.Size = new System.Drawing.Size(120, 40);
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void NuevoProducto_Load(object sender, EventArgs e)
        {
            // Opcional: poner fecha actual
            dateTimePicker1.Value = DateTime.Today;
            // Si el ID es autoincremental, puedes deshabilitar el textBox1
            // textBox1.Enabled = false;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una categoría.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(textBox6.Text, out decimal precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("Ingrese un precio de venta válido (mayor a 0).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(textBox5.Text, out decimal precioCosto) || precioCosto <= 0)
            {
                MessageBox.Show("Ingrese un precio de costo válido (mayor a 0).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ID del producto (opcional, si se deja vacío se genera automático)
            int idProducto;
            bool idAutomatico = !int.TryParse(textBox1.Text.Trim(), out idProducto);

            string nombre = textBox3.Text.Trim();
            int idCategoria = (int)comboBox2.SelectedValue;
            string descripcion = textBox2.Text.Trim();
            DateTime fecha = dateTimePicker1.Value; // (No se guarda en tabla Productos, solo informativo)

            string query;
            if (idAutomatico)
            {
                query = @"
                    INSERT INTO Productos (Nombre, ID_categoria, Precio_Venta, Precio_costo, Descripcion)
                    VALUES (@nombre, @idCat, @precioVenta, @precioCosto, @descripcion)";
            }
            else
            {
                query = @"
                    INSERT INTO Productos (ID_Producto, Nombre, ID_categoria, Precio_Venta, Precio_costo, Descripcion)
                    VALUES (@id, @nombre, @idCat, @precioVenta, @precioCosto, @descripcion)";
            }

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (!idAutomatico)
                    cmd.Parameters.AddWithValue("@id", idProducto);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@idCat", idCategoria);
                cmd.Parameters.AddWithValue("@precioVenta", precioVenta);
                cmd.Parameters.AddWithValue("@precioCosto", precioCosto);
                cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(descripcion) ? (object)DBNull.Value : descripcion);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Violación de PK
                        MessageBox.Show("Ya existe un producto con ese ID. Use otro o déjelo vacío para autoincremental.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (ex.Message.Contains("FK_Prod_Cat"))
                        MessageBox.Show("La categoría seleccionada no existe.", "Error de clave foránea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}