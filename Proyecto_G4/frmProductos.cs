using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmProductos : Form
    {
        private readonly CN_Producto _cnProducto = new CN_Producto();
        private readonly CN_Categoria _cnCategoria = new CN_Categoria();
        private readonly Usuario _usuarioActual;

        public frmProductos(Usuario oUsuario = null)
        {
            InitializeComponent();
            _usuarioActual = oUsuario;
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarProductosEnGrilla();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            Producto objProducto = ObtenerProductoDesdeFormulario();

            if (objProducto.IdProducto == 0)
            {
                // REGISTRAR PRODUCTO
                int idGenerado = _cnProducto.Registrar(objProducto, out string mensaje);

                if (idGenerado != 0)
                {
                    AgregarFilaAGrilla(idGenerado, objProducto);
                    Limpiar();
                }
                else
                {
                    MostrarMensaje(mensaje, "Validación", MessageBoxIcon.Warning);
                }
            }
            else
            {
                // EDITAR PRODUCTO
                bool resultado = _cnProducto.Editar(objProducto, out string mensaje);

                if (resultado)
                {
                    ActualizarFilaGrilla(objProducto);
                    Limpiar();
                }
                else
                {
                    MostrarMensaje(mensaje, "Validación", MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvdata.Columns[e.ColumnIndex].Name != "btnSeleccionar")
                return;

            int indice = e.RowIndex;

            txtIndice.Text = indice.ToString();
            txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value?.ToString() ?? "0";
            txtcodigo.Text = dgvdata.Rows[indice].Cells["Codigo"].Value?.ToString() ?? "";
            txtnombre.Text = dgvdata.Rows[indice].Cells["Nombre"].Value?.ToString() ?? "";
            txtdescripcion.Text = dgvdata.Rows[indice].Cells["Descripcion"].Value?.ToString() ?? "";

            SeleccionarComboPorValor(cmbcategoria, dgvdata.Rows[indice].Cells["IdCategoria"].Value);
            SeleccionarComboPorValor(cmbestado, dgvdata.Rows[indice].Cells["EstadoValor"].Value);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (cmbbusqueda.SelectedItem == null) return;

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

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MostrarMensaje("No hay datos para exportar", "Mensaje", MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();

            // Agregar columnas visibles que tengan título
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (!string.IsNullOrEmpty(columna.HeaderText) && columna.Visible && columna.Name != "btnSeleccionar")
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string));
                }
            }

            // Mapeo dinámico por NOMBRE de columna (evita desfasamiento de datos por índices estáticos)
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                if (row.Visible)
                {
                    dt.Rows.Add(new object[]
                    {
                        row.Cells["Codigo"].Value?.ToString() ?? "",
                        row.Cells["Nombre"].Value?.ToString() ?? "",
                        row.Cells["Descripcion"].Value?.ToString() ?? "",
                        row.Cells["Categoria"].Value?.ToString() ?? "",
                        row.Cells["Stock"].Value?.ToString() ?? "0",
                        row.Cells["PrecioCompra"].Value?.ToString() ?? "0.00",
                        row.Cells["PrecioVenta"].Value?.ToString() ?? "0.00",
                        row.Cells["Estado"].Value?.ToString() ?? ""
                    });
                }
            }

            using (SaveFileDialog savefile = new SaveFileDialog())
            {
                savefile.FileName = $"ReporteProducto_{DateTime.Now:ddMMyyyyHHmmss}.xlsx";
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add(dt, "Informe");
                            hoja.ColumnsUsed().AdjustToContents();
                            wb.SaveAs(savefile.FileName);
                            MostrarMensaje("Reporte Generado con éxito", "Mensaje", MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje("Error al generar el reporte: " + ex.Message, "Mensaje", MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Métodos Auxiliares de Interfaz

        private void CargarCombos()
        {
            cmbestado.Items.Clear();
            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;

            cmbcategoria.Items.Clear();
            List<Categoria> listaCategoria = _cnCategoria.Listar();
            foreach (Categoria item in listaCategoria)
            {
                cmbcategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
            }
            cmbcategoria.DisplayMember = "Texto";
            cmbcategoria.ValueMember = "Valor";
            if (cmbcategoria.Items.Count > 0) cmbcategoria.SelectedIndex = 0;

            cmbbusqueda.Items.Clear();
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

        private void CargarProductosEnGrilla()
        {
            dgvdata.Rows.Clear();
            List<Producto> listaProducto = _cnProducto.Listar();

            foreach (Producto item in listaProducto)
            {
                dgvdata.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado ? 1 : 0,
                    item.Estado ? "Activo" : "No Activo"
                });
            }
        }

        private Producto ObtenerProductoDesdeFormulario()
        {
            OpcionCombo catSeleccionada = (OpcionCombo)cmbcategoria.SelectedItem;
            OpcionCombo estSeleccionado = (OpcionCombo)cmbestado.SelectedItem;

            int.TryParse(txtid.Text, out int idProducto);

            return new Producto()
            {
                IdProducto = idProducto,
                Codigo = txtcodigo.Text.Trim(),
                Nombre = txtnombre.Text.Trim(),
                Descripcion = txtdescripcion.Text.Trim(),
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(catSeleccionada?.Valor ?? 0) },
                Estado = Convert.ToInt32(estSeleccionado?.Valor ?? 0) == 1,
                oUsuario = _usuarioActual // Preserva la auditoría de sesión para la Bitácora
            };
        }

        private void AgregarFilaAGrilla(int idGenerado, Producto prod)
        {
            OpcionCombo catSeleccionada = (OpcionCombo)cmbcategoria.SelectedItem;
            OpcionCombo estSeleccionado = (OpcionCombo)cmbestado.SelectedItem;

            dgvdata.Rows.Add(new object[] {
                "",
                idGenerado,
                prod.Codigo,
                prod.Nombre,
                prod.Descripcion,
                catSeleccionada?.Valor.ToString(),
                catSeleccionada?.Texto,
                "0",
                "0.00",
                "0.00",
                estSeleccionado?.Valor.ToString(),
                estSeleccionado?.Texto
            });
        }

        private void ActualizarFilaGrilla(Producto prod)
        {
            if (!int.TryParse(txtIndice.Text, out int indice) || indice < 0) return;

            OpcionCombo catSeleccionada = (OpcionCombo)cmbcategoria.SelectedItem;
            OpcionCombo estSeleccionado = (OpcionCombo)cmbestado.SelectedItem;

            DataGridViewRow row = dgvdata.Rows[indice];
            row.Cells["Id"].Value = prod.IdProducto;
            row.Cells["Codigo"].Value = prod.Codigo;
            row.Cells["Nombre"].Value = prod.Nombre;
            row.Cells["Descripcion"].Value = prod.Descripcion;
            row.Cells["IdCategoria"].Value = catSeleccionada?.Valor.ToString();
            row.Cells["Categoria"].Value = catSeleccionada?.Texto;
            row.Cells["EstadoValor"].Value = estSeleccionado?.Valor.ToString();
            row.Cells["Estado"].Value = estSeleccionado?.Texto;
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtcodigo.Text = "";
            txtnombre.Text = "";
            txtdescripcion.Text = "";
            if (cmbcategoria.Items.Count > 0) cmbcategoria.SelectedIndex = 0;
            if (cmbestado.Items.Count > 0) cmbestado.SelectedIndex = 0;

            txtcodigo.Select();
        }

        private void SeleccionarComboPorValor(ComboBox combo, object valorBusqueda)
        {
            if (valorBusqueda == null) return;

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