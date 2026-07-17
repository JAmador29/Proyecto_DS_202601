using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmReporteVentas : Form
    {
        public frmReporteVentas()
        {
            InitializeComponent();
        }
        // evita que la fecha final sea anterior a la fecha inicial
        private void txtfechainicio_ValueChanged_1(object sender, EventArgs e)
        {
            txtfechafin.MinDate = txtfechainicio.Value.Date;
            if (txtfechafin.Value.Date < txtfechainicio.Value.Date)
            {
                txtfechafin.Value = txtfechainicio.Value.Date;
            }

        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            txtfechainicio.MaxDate = DateTime.Today;
            txtfechafin.MaxDate = DateTime.Today;

            //La fecha final no puede ser anterior a la fecha inicial
            txtfechafin.MinDate = txtfechainicio.Value.Date;
        }

        private void btnbuscarreporte_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReporteVenta> lista = new CN_Reporte().Venta(txtfechainicio.Value, txtfechafin.Value);

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
                    MessageBox.Show("No se encontraron ventas en el rango de fechas seleccionado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    string valorCelda = Convert.ToString(row.Cells[columnaFiltro].Value);

                    if (valorCelda.Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
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

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    valores[i] = Convert.ToString(row.Cells[i].Value);
                }
                dt.Rows.Add(valores);
            }

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("ReporteVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
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
                    }
                    MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar reporte: " + ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

       
    }
}