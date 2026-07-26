using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Proyecto_G4
{
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
            txtdocumento.MaxLength = 14;
        }

        private void CargarProveedores()
        {
            dgvdata.Rows.Clear();

            List<Proveedor> lista = new CN_Proveedor().Listar();

            foreach (Proveedor item in lista)
            {
                dgvdata.Rows.Add(new object[]
                {
                    "",
                    item.IdProveedor,
                    item.RTN,
                    item.RazonSocial,
                    item.Correo,
                    item.Telefono,
                    item.Estado ? 1 :0,
                    item.Estado ? "Activo" : "No Activo"
                });
            }
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cmbbusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cmbbusqueda.DisplayMember = "Texto";
            cmbbusqueda.ValueMember = "Valor";
            cmbbusqueda.SelectedIndex = 0;

            CargarProveedores();
        }

        private bool ValidarLongitudCampos()
        {

            if (txtdocumento.Text.Trim().Length > 14)
            {
                MessageBox.Show("El campo 'Documento' no puede superar los 14 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdocumento.Focus();
                return false;
            }

            if (txtrazonsocial.Text.Trim().Length > 50)
            {
                MessageBox.Show("El campo 'Razón Social' es demasiado largo. El máximo permitido son 50 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtrazonsocial.Focus();
                return false;
            }

            if (txtcorreo.Text.Trim().Length > 50)
            {
                MessageBox.Show("El correo electrónico no puede superar los 50 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcorreo.Focus();
                return false;
            }

            if (txttelefono.Text.Trim().Length > 8)
            {
                MessageBox.Show("El teléfono no puede superar los 8 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttelefono.Focus();
                return false;
            }

            return true; // Todos los campos cumplen con la longitud permitida
        }

        //Metodo de validacion de correo y dominio
        private bool ValidarCorreoYDominio(string correo)
        {
            //Expresión regular para validar formato general del correo
            string patronRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrWhiteSpace(correo) || !Regex.IsMatch(correo, patronRegex))
            {
                return false;
            }

            try
            {
                //Extraer el dominio limpio usando MailAddress
                var mail = new MailAddress(correo);
                string dominioUsuario = mail.Host.ToLower().Trim();

                //Lista blanca de dominios aceptados
                string[] dominiosValidos = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com" };

                //Comprobar si el dominio pertenece a la lista blanca
                return dominiosValidos.Contains(dominioUsuario);
            }
            catch
            {
                return false;
            }
        }


        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // Validación de Longitudes (Llamada a la nueva función externa)
            if (!ValidarLongitudCampos())
            {
                return; // Se detiene porque la función interna ya mostró el MessageBox y dio Focus
            }

            //validacion de correo
            string correo = txtcorreo.Text.Trim();

            // Ejecuta el formato Regex y la lista de dominios en un solo paso instantáneo
            if (!ValidarCorreoYDominio(correo))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.\n\nSolo se permiten dominios de: gmail.com, yahoo.com, outlook.com y hotmail.com.",
                                "Validación de Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Proveedor obj = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtid.Text),
                RTN = txtdocumento.Text,
                RazonSocial = txtrazonsocial.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProveedor == 0)
            {
                int idgenerado = new CN_Proveedor().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    CargarProveedores();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Proveedor().Editar(obj, out mensaje);

                if (resultado)
                {
                    CargarProveedores();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtrazonsocial.Text = "";
            txtcorreo.Text = "";
            txttelefono.Text = "";
            cmbestado.SelectedIndex = 0;
            txtdocumento.Select();
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    txtrazonsocial.Text = dgvdata.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    txttelefono.Text = dgvdata.Rows[indice].Cells["Telefono"].Value.ToString();

                    foreach (OpcionCombo oc in cmbestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cmbestado.Items.IndexOf(oc);
                            cmbestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbbusqueda.SelectedItem).Valor.ToString();
            bool seEncontroCoincidencia = false;

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                        seEncontroCoincidencia = true;
                    }
                    else
                        row.Visible = false;
                }

                if (!seEncontroCoincidencia)
                {
                    MessageBox.Show("No se encontraron resultados para su búsqueda.",
                                    "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Vuelve a mostrar todas las filas para que la grilla no quede vacía
                    foreach (DataGridViewRow row in dgvdata.Rows)
                    {
                        row.Visible = true;
                    }
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void txtrazonsocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '&' &&
                e.KeyChar != '.' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvdata_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "Estado")
            {
                string estado = e.Value?.ToString();

                if (estado == "Activo")
                {
                    //e.CellStyle.BackColor = Color.FromArgb(39, 174, 96);   // Verde
                    e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(46, 204, 113);
                    e.CellStyle.SelectionForeColor = Color.White;
                }
                else if (estado == "No Activo")
                {
                    //e.CellStyle.BackColor = Color.FromArgb(192, 57, 43);   // Rojo
                    e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(231, 76, 60);
                    e.CellStyle.SelectionForeColor = Color.White;
                }
            }
        }
    }
}