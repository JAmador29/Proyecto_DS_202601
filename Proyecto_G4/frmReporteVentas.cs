using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmReporteVentas : Form
    {
        private readonly CN_Reporte _cnReporte = new CN_Reporte();

        public frmReporteVentas()
        {
            InitializeComponent();
        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            CargarComboBusqueda();
            ConfigurarLimitesFechas();
        }

        private void txtfechainicio_ValueChanged_1(object sender, EventArgs e)
        {
            txtfechafin.MinDate = txtfechainicio.Value.Date;
            if (txtfechafin.Value.Date < txtfechainicio.Value.Date)
            {
                txtfechafin.Value = txtfechainicio.Value.Date;
            }
        }

        private void btnbuscarreporte_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReporteVenta> lista = _cnReporte.Venta(txtfechainicio.Value, txtfechafin.Value);

                dgvdata.Rows.Clear();

                foreach (ReporteVenta rv in lista)
                {
                    dgvdata.Rows.Add(new object[]
                    {
                        rv.FechaRegistro,
                        rv.TipoDocumento,
                        rv.NumeroDocumento,
                        rv.MontoTotal,
                        rv.UsuarioRegistro,
                        rv.DocumentoCliente,
                        rv.NombreCliente,
                        rv.CodigoProducto,
                        rv.NombreProducto,
                        rv.Categoria,
                        rv.PrecioVenta,
                        rv.Cantidad,
                        rv.SubTotal
                    });
                }

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron ventas en el rango de fechas seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException exArg)
            {
                MessageBox.Show(exArg.Message, "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            FiltrarGrilla();
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = string.Empty;
            MostrarTodasLasFilas();
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                dt.Columns.Add(columna.HeaderText, typeof(string));
            }

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                if (!row.Visible) continue;

                object[] valores = new object[dgvdata.Columns.Count];
                for (int i = 0; i < dgvdata.Columns.Count; i++)
                {
                    valores[i] = Convert.ToString(row.Cells[i].Value ?? string.Empty);
                }
                dt.Rows.Add(valores);
            }

            SaveFileDialog savefile = new SaveFileDialog
            {
                FileName = string.Format("ReporteVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")),
                Filter = "Excel Files | *.xlsx"
            };

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                    }
                    MessageBox.Show("Reporte Generado con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region Métodos Auxiliares

        private void CargarComboBusqueda()
        {
            cbobusqueda.Items.Clear();
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            if (cbobusqueda.Items.Count > 0) cbobusqueda.SelectedIndex = 0;
        }

        private void ConfigurarLimitesFechas()
        {
            txtfechainicio.MaxDate = DateTime.Today;
            txtfechafin.MaxDate = DateTime.Today;
            txtfechafin.MinDate = txtfechainicio.Value.Date;
        }

        private void FiltrarGrilla()
        {
            if (dgvdata.Rows.Count == 0) return;

            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();
            string busqueda = txtbusqueda.Text.Trim().ToUpper();
            bool seEncontroCoincidencia = false;

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                string valorCelda = Convert.ToString(row.Cells[columnaFiltro].Value ?? string.Empty).Trim().ToUpper();

                if (valorCelda.Contains(busqueda))
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
                MessageBox.Show("No se encontraron resultados para su búsqueda.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarTodasLasFilas();
            }
        }

        private void MostrarTodasLasFilas()
        {
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        #endregion
    }
}