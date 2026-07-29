using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_G4
{
    /// <summary>
    /// Capa de Presentación para la gestión gráfica de Proveedores.
    /// Desacoplada de las validaciones complejas de negocio.
    /// </summary>
    public partial class frmProveedores : Form
    {
        private readonly CN_Proveedor _cnProveedor = new CN_Proveedor();

        public frmProveedores()
        {
            InitializeComponent();
            txtdocumento.MaxLength = 14;
            txttelefono.MaxLength = 8;
            txtrazonsocial.MaxLength = 50;
            txtcorreo.MaxLength = 50;
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarProveedores();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            Proveedor obj = ObtenerProveedorDesdeFormulario();

            if (obj.IdProveedor == 0)
            {
                // REGISTRAR PROVEEDOR
                int idGenerado = _cnProveedor.Registrar(obj, out string mensaje);

                if (idGenerado != 0)
                {
                    CargarProveedores();
                    Limpiar();
                }
                else
                {
                    MostrarMensaje(mensaje, "Validación de Datos", MessageBoxIcon.Warning);
                }
            }
            else
            {
                // EDITAR PROVEEDOR
                bool resultado = _cnProveedor.Editar(obj, out string mensaje);

                if (resultado)
                {
                    CargarProveedores();
                    Limpiar();
                }
                else
                {
                    MostrarMensaje(mensaje, "Validación de Datos", MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name != "btnSeleccionar")
                return;

            int indice = e.RowIndex;
            if (indice < 0)
                return;

            txtIndice.Text = indice.ToString();
            txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
            txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
            txtrazonsocial.Text = dgvdata.Rows[indice].Cells["RazonSocial"].Value.ToString();
            txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
            txttelefono.Text = dgvdata.Rows[indice].Cells["Telefono"].Value.ToString();

            SeleccionarComboPorValor(cmbestado, dgvdata.Rows[indice].Cells["EstadoValor"].Value);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbbusqueda.SelectedItem).Valor.ToString();
            string textoBusqueda = txtbusqueda.Text.Trim().ToUpper();
            bool seEncontroCoincidencia = false;

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                string valorCelda = row.Cells[columnaFiltro].Value?.ToString().Trim().ToUpper() ?? "";

                if (valorCelda.Contains(textoBusqueda))
                {
                    row.Visible = true;
                    seEncontroCoincidencia = true;
                }
                else
                {
                    row.Visible = false;
                }
            }

            if (!seEncontroCoincidencia)
            {
                MostrarMensaje("No se encontraron resultados para su búsqueda.", "Sin Resultados", MessageBoxIcon.Information);
                MostrarTodasLasFilas();
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            MostrarTodasLasFilas();
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        #region Restricciones Teclado (KeyPress)

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

        #endregion

        #region Formato Visual

        private void dgvdata_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "Estado")
            {
                string estado = e.Value?.ToString();

                if (estado == "Activo")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(46, 204, 113);
                    e.CellStyle.SelectionForeColor = Color.White;
                }
                else if (estado == "No Activo")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(231, 76, 60);
                    e.CellStyle.SelectionForeColor = Color.White;
                }
            }
        }

        #endregion

        #region Métodos Auxiliares (Clean Code)

        private void CargarCombos()
        {
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible && columna.Name != "btnSeleccionar")
                {
                    cmbbusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cmbbusqueda.DisplayMember = "Texto";
            cmbbusqueda.ValueMember = "Valor";
            if (cmbbusqueda.Items.Count > 0) cmbbusqueda.SelectedIndex = 0;
        }

        private void CargarProveedores()
        {
            dgvdata.Rows.Clear();
            List<Proveedor> lista = _cnProveedor.Listar();

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
                    item.Estado ? 1 : 0,
                    item.Estado ? "Activo" : "No Activo"
                });
            }
        }

        private Proveedor ObtenerProveedorDesdeFormulario()
        {
            return new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtid.Text),
                RTN = txtdocumento.Text,
                RazonSocial = txtrazonsocial.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1
            };
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtrazonsocial.Text = "";
            txtcorreo.Text = "";
            txttelefono.Text = "";
            if (cmbestado.Items.Count > 0) cmbestado.SelectedIndex = 0;
            txtdocumento.Select();
        }

        private void SeleccionarComboPorValor(ComboBox combo, object valorBusqueda)
        {
            foreach (OpcionCombo oc in combo.Items)
            {
                if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(valorBusqueda))
                {
                    combo.SelectedIndex = combo.Items.IndexOf(oc);
                    break;
                }
            }
        }

        private void MostrarTodasLasFilas()
        {
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }

        #endregion
    }
}