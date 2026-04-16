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
            // Validar campos obligatorios (sin ID)
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox12.Text))
            {
                MessageBox.Show("Complete todos los campos: Nombre, RTN, Teléfono, Dirección y Correo.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = textBox2.Text.Trim();
            string rtn = textBox5.Text.Trim();
            string telefono = textBox9.Text.Trim();
            string direccion = textBox6.Text.Trim();
            string email = textBox12.Text.Trim();

            string query = @"INSERT INTO Clientes (Nombre, RTN_DNI, Telefono, Direccion, Correo)
                     VALUES (@nombre, @rtn, @telefono, @direccion, @correo)";

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@rtn", rtn);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@correo", email);

                try
                {
                    conn.Open();
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    string mensaje = "Error de SQL:\n" + ex.Message;
                    if (ex.Number == 2627)
                        mensaje += "\nPosible duplicado (RTN o Correo ya existe).";
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}