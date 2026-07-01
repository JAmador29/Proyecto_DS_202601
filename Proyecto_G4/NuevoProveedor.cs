using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_G4
{
    /*public partial class NuevoProveedor : Form
    {
        private Button btnGuardar;
        private Button btnCancelar;

        public NuevoProveedor()
        {
            InitializeComponent();
            CrearBotones();
            this.Load += NuevoProveedor_Load;
        }

        private void CrearBotones()
        {
            // Botón Guardar
            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.BackColor = System.Drawing.Color.Azure;
            btnGuardar.Location = new System.Drawing.Point(50, 300);
            btnGuardar.Size = new System.Drawing.Size(120, 40);
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            // Botón Cancelar
            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = System.Drawing.Color.LightSalmon;
            btnCancelar.Location = new System.Drawing.Point(200, 300);
            btnCancelar.Size = new System.Drawing.Size(120, 40);
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void NuevoProveedor_Load(object sender, EventArgs e)
        {
            // Opcional: poner fecha actual
            dateTimePicker1.Value = DateTime.Today;
            // Si el ID es autoincremental, puedes deshabilitar textBox1
            // textBox1.Enabled = false;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("El nombre del proveedor es obligatorio.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("El contacto es obligatorio.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ID del proveedor (opcional, si se deja vacío se genera automático)
            int idProveedor;
            bool idAutomatico = !int.TryParse(textBox1.Text.Trim(), out idProveedor);

            string rtn = textBox5.Text.Trim();
            string nombre = textBox2.Text.Trim();
            string contacto = textBox4.Text.Trim();
            string telefono = textBox9.Text.Trim();
            // Nota: No hay campo Email en la tabla Proveedores según el script original.
            // Si en el futuro agregas columna Email, deberás añadir el campo y la variable aquí.
            // Por ahora ignoramos el label12 (Email) que está en el diseñador pero sin textBox.

            string query;
            if (idAutomatico)
            {
                query = @"
                    INSERT INTO Proveedores (RTN, Nombre_Proveedor, Contacto, Telefono)
                    VALUES (@rtn, @nombre, @contacto, @telefono)";
            }
            else
            {
                query = @"
                    INSERT INTO Proveedores (ID_Proveedor, RTN, Nombre_Proveedor, Contacto, Telefono)
                    VALUES (@id, @rtn, @nombre, @contacto, @telefono)";
            }

            using (SqlConnection conn = Conexion.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (!idAutomatico)
                    cmd.Parameters.AddWithValue("@id", idProveedor);
                cmd.Parameters.AddWithValue("@rtn", string.IsNullOrEmpty(rtn) ? (object)DBNull.Value : rtn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@contacto", contacto);
                cmd.Parameters.AddWithValue("@telefono", telefono);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Violación de PK
                        MessageBox.Show("Ya existe un proveedor con ese ID. Use otro o déjelo vacío para autoincremental.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }*/
}