using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class NuevaVenta : Form
    {
        private int idUsuarioLogueado;
        private DataTable detalleVenta;
        private decimal subtotal = 0, impuesto = 0, total = 0;

        // Controles dinámicos
        private TextBox txtCodigoProducto;
        private TextBox txtCantidad;
        private Button btnGuardar;

        public NuevaVenta(int idUsuario)
        {
            InitializeComponent();
            idUsuarioLogueado = idUsuario;
            InicializarDetalle();
            CargarCombos();
            CrearControlesDinamicos();
            this.Load += NuevaVenta_Load;
        }

        private void InicializarDetalle()
        {
            detalleVenta = new DataTable();
            detalleVenta.Columns.Add("ID_Producto", typeof(int));
            detalleVenta.Columns.Add("Producto", typeof(string));
            detalleVenta.Columns.Add("Cantidad", typeof(int));
            detalleVenta.Columns.Add("Precio", typeof(decimal));
            detalleVenta.Columns.Add("Descuento", typeof(decimal));
            detalleVenta.Columns.Add("Total", typeof(decimal));
            dataGridView1.DataSource = detalleVenta;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Ocultar columna ID_Producto
            dataGridView1.Columns["ID_Producto"].Visible = false;
        }

        private void CargarCombos()
        {
            // Cargar clientes
            using (SqlConnection conn = Conexion.GetConnection())
            {
                string query = "SELECT ID_cliente, Nombre FROM Clientes ORDER BY Nombre";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "ID_cliente";
                comboBox1.DataSource = dt;
            }

            // Métodos de pago (fijos)
            comboBox2.Items.Clear();
            comboBox2.Items.Add(new { Text = "Efectivo", Value = 1 });
            comboBox2.Items.Add(new { Text = "Tarjeta Débito/Crédito", Value = 2 });
            comboBox2.Items.Add(new { Text = "Transferencia", Value = 3 });
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            comboBox2.SelectedIndex = 0;

            // Estados (fijos)
            comboBox3.Items.Clear();
            comboBox3.Items.Add(new { Text = "Pagado", Value = 1 });
            comboBox3.Items.Add(new { Text = "Pago Parcial", Value = 2 });
            comboBox3.Items.Add(new { Text = "Crédito", Value = 3 });
            comboBox3.Items.Add(new { Text = "Vencido", Value = 4 });
            comboBox3.DisplayMember = "Text";
            comboBox3.ValueMember = "Value";
            comboBox3.SelectedIndex = 0;
        }

        private void CrearControlesDinamicos()
        {
            // Label y TextBox para código de producto
            Label lblCodigo = new Label();
            lblCodigo.Text = "Código Producto:";
            lblCodigo.Location = new System.Drawing.Point(22, 280);
            lblCodigo.Size = new System.Drawing.Size(120, 22);
            this.Controls.Add(lblCodigo);

            txtCodigoProducto = new TextBox();
            txtCodigoProducto.Location = new System.Drawing.Point(150, 275);
            txtCodigoProducto.Size = new System.Drawing.Size(120, 29);
            this.Controls.Add(txtCodigoProducto);

            // Label y TextBox para cantidad
            Label lblCantidad = new Label();
            lblCantidad.Text = "Cantidad:";
            lblCantidad.Location = new System.Drawing.Point(22, 320);
            lblCantidad.Size = new System.Drawing.Size(120, 22);
            this.Controls.Add(lblCantidad);

            txtCantidad = new TextBox();
            txtCantidad.Text = "1";
            txtCantidad.Location = new System.Drawing.Point(150, 315);
            txtCantidad.Size = new System.Drawing.Size(120, 29);
            this.Controls.Add(txtCantidad);

            // Botón Guardar (fuera del groupBox1)
            btnGuardar = new Button();
            btnGuardar.Text = "Guardar Venta";
            btnGuardar.BackColor = System.Drawing.Color.Azure;
            btnGuardar.Location = new System.Drawing.Point(678, 330);
            btnGuardar.Size = new System.Drawing.Size(150, 40);
            btnGuardar.Click += BtnGuardarVenta_Click;
            this.Controls.Add(btnGuardar);

            // Asignar eventos a los botones existentes
            button1.Click += BtnAgregarProducto_Click;
            button2.Click += BtnEliminarProducto_Click;
            button3.Click += BtnLimpiar_Click;
        }

        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCodigoProducto.Text, out int idProducto))
            {
                MessageBox.Show("Ingrese un código de producto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida (>0).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                SELECT p.Nombre, p.Precio_Venta, i.Stock_actual
                FROM Productos p
                INNER JOIN Inventario i ON p.ID_Producto = i.ID_Producto
                WHERE p.ID_Producto = @id";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", idProducto);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string nombre = reader["Nombre"].ToString();
                    decimal precio = Convert.ToDecimal(reader["Precio_Venta"]);
                    int stock = Convert.ToInt32(reader["Stock_actual"]);

                    if (cantidad > stock)
                    {
                        MessageBox.Show($"Stock insuficiente. Solo hay {stock} unidades.", "Sin stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Verificar si ya existe en el detalle
                    DataRow[] rows = detalleVenta.Select($"ID_Producto = {idProducto}");
                    if (rows.Length > 0)
                    {
                        int nuevaCant = Convert.ToInt32(rows[0]["Cantidad"]) + cantidad;
                        if (nuevaCant > stock)
                        {
                            MessageBox.Show($"No se puede agregar más. Stock máximo: {stock}.", "Límite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        rows[0]["Cantidad"] = nuevaCant;
                        rows[0]["Total"] = nuevaCant * precio;
                    }
                    else
                    {
                        detalleVenta.Rows.Add(idProducto, nombre, cantidad, precio, 0, cantidad * precio);
                    }
                    CalcularTotales();
                    txtCodigoProducto.Clear();
                    txtCantidad.Text = "1";
                    txtCodigoProducto.Focus();
                }
                else
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CalcularTotales()
        {
            subtotal = 0;
            foreach (DataRow row in detalleVenta.Rows)
                subtotal += Convert.ToDecimal(row["Total"]);
            impuesto = subtotal * 0.15m;
            total = subtotal + impuesto;

            label6.Text = "SubTotal: " + subtotal.ToString("N2");
            label7.Text = "Impuesto: " + impuesto.ToString("N2");
            label8.Text = "Total: " + total.ToString("N2");
        }

        private void BtnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                detalleVenta.Rows.Remove(dataGridView1.CurrentRow.DataBoundItem as DataRow);
                CalcularTotales();
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            detalleVenta.Clear();
            CalcularTotales();
            txtCodigoProducto.Clear();
            txtCantidad.Text = "1";
        }

        private void BtnGuardarVenta_Click(object sender, EventArgs e)
        {
            if (detalleVenta.Rows.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Detalle vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox1.Text.Trim(), out int numFactura))
            {
                MessageBox.Show("Número de factura inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = (int)comboBox1.SelectedValue;
            int metodoPago = (int)((dynamic)comboBox2.SelectedItem).Value;
            int estado = (int)((dynamic)comboBox3.SelectedItem).Value;
            DateTime fecha = dateTimePicker1.Value;

            using (SqlConnection conn = Conexion.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Insertar cabecera
                    string sqlVenta = @"
                        INSERT INTO Ventas (Num_factura, ID_Usuario, ID_Cliente, Fecha, Subtotal, Impuesto, Total, Metodo_pago, Estado)
                        VALUES (@num, @idUser, @idCli, @fecha, @sub, @imp, @tot, @met, @est);
                        SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sqlVenta, conn, trans);
                    cmd.Parameters.AddWithValue("@num", numFactura);
                    cmd.Parameters.AddWithValue("@idUser", idUsuarioLogueado);
                    cmd.Parameters.AddWithValue("@idCli", idCliente);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@sub", subtotal);
                    cmd.Parameters.AddWithValue("@imp", impuesto);
                    cmd.Parameters.AddWithValue("@tot", total);
                    cmd.Parameters.AddWithValue("@met", metodoPago);
                    cmd.Parameters.AddWithValue("@est", estado);
                    int idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insertar detalle y actualizar stock
                    foreach (DataRow row in detalleVenta.Rows)
                    {
                        int idProd = Convert.ToInt32(row["ID_Producto"]);
                        int cant = Convert.ToInt32(row["Cantidad"]);
                        decimal precioUnit = Convert.ToDecimal(row["Precio"]);
                        decimal totalDet = Convert.ToDecimal(row["Total"]);

                        string sqlDet = @"
                            INSERT INTO Detalle_Venta (ID_Venta, ID_Producto, Cantidad, Precio_unitario, Total)
                            VALUES (@idVen, @idProd, @cant, @prec, @tot)";
                        SqlCommand cmdDet = new SqlCommand(sqlDet, conn, trans);
                        cmdDet.Parameters.AddWithValue("@idVen", idVenta);
                        cmdDet.Parameters.AddWithValue("@idProd", idProd);
                        cmdDet.Parameters.AddWithValue("@cant", cant);
                        cmdDet.Parameters.AddWithValue("@prec", precioUnit);
                        cmdDet.Parameters.AddWithValue("@tot", totalDet);
                        cmdDet.ExecuteNonQuery();

                        string sqlStock = "UPDATE Inventario SET Stock_actual = Stock_actual - @cant WHERE ID_Producto = @idProd";
                        SqlCommand cmdStock = new SqlCommand(sqlStock, conn, trans);
                        cmdStock.Parameters.AddWithValue("@cant", cant);
                        cmdStock.Parameters.AddWithValue("@idProd", idProd);
                        cmdStock.ExecuteNonQuery();
                    }

                    trans.Commit();
                    MessageBox.Show("Venta registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    trans.Rollback();
                    if (ex.Number == 2627)
                        MessageBox.Show("Número de factura ya existe. Use otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error SQL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NuevaVenta_Load(object sender, EventArgs e)
        {
            // Ajustes adicionales si son necesarios
        }
    }
}