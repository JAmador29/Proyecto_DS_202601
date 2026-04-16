using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class NuevoCliente : Form
    {
        public NuevoCliente()
        {
            InitializeComponent();
            CrearBotones();
        }

        private void CrearBotones()
        {
            // Botón Guardar
            Button btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.BackColor = System.Drawing.Color.Azure;
            btnGuardar.Size = new System.Drawing.Size(120, 40);
            btnGuardar.Location = new System.Drawing.Point(50, 520);
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            // Botón Cancelar
            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = System.Drawing.Color.LightSalmon;
            btnCancelar.Size = new System.Drawing.Size(120, 40);
            btnCancelar.Location = new System.Drawing.Point(200, 520);
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios (ID, Nombre, RTN, Teléfono, Dirección, Email)
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox12.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el ID sea numérico
            if (!int.TryParse(textBox1.Text.Trim(), out int idCliente))
            {
                MessageBox.Show("El ID debe ser un número entero.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nombre = textBox2.Text.Trim();
            string rtn = textBox5.Text.Trim();
            string telefono = textBox9.Text.Trim();
            string direccion = textBox6.Text.Trim();
            string email = textBox12.Text.Trim();
            DateTime fecha = dateTimePicker1.Value;

            string query = @"
                INSERT INTO Clientes (ID_cliente, Nombre, RTN_DNI, Telefono, Direccion, Correo)
                VALUES (@id, @nombre, @rtn, @telefono, @direccion, @correo)";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", idCliente);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@rtn", rtn);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@correo", email);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Violación de PK
                        MessageBox.Show("Ya existe un cliente con ese ID. Use otro ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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