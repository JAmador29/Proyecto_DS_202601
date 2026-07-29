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
    public partial class frmReporteCompras : Form
    {
        private readonly CN_Reporte _cnReporte = new CN_Reporte();
        private readonly CN_Proveedor _cnProveedor = new CN_Proveedor();

        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarComboBusqueda();
            ConfigurarFormatoGrilla();
            ConfigurarLimitesFechas();
        }

        private void txtfechainicio_ValueChanged(object sender, EventArgs e)
        {
            txtfechafin.MinDate = txtfechainicio.Value.Date;

            if (txtfechafin.Value.Date < txtfechainicio.Value.Date)
            {
                txtfechafin.Value = txtfechainicio.Value.Date;
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idproveedor = Convert.ToInt32(((OpcionCombo)cboprovedor.SelectedItem).Valor);

                List<ReporteCompra> lista = _cnReporte.Compra(txtfechainicio.Value, txtfechafin.Value, idproveedor);

                dgvdata.Rows.Clear();

                foreach (ReporteCompra rc in lista)
                {
                    dgvdata.Rows.Add(new object[]
                    {
                        rc.FechaRegistro,
                        rc.TipoDocumento,
                        rc.NumeroDocumento,
                        rc.MontoTotal,
                        rc.UsuarioRegistro,
                        rc.RTN,
                        rc.RazonSocial,
                        rc.CodigoProducto,
                        rc.NombreProducto,
                        rc.Categoria,
                        rc.PrecioCompra,
                        rc.PrecioVenta,
                        rc.Cantidad,
                        rc.SubTotal
                    });
                }

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron compras en el rango de fechas seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void iconButton1_Click(object sender, EventArgs e)
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
                FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")),
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
                    MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region Métodos Auxiliares

        private void CargarProveedores()
        {
            List<Proveedor> lista = _cnProveedor.Listar();

            cboprovedor.Items.Clear();
            cboprovedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "TODOS" });
            foreach (Proveedor item in lista)
            {
                cboprovedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
            }
            cboprovedor.DisplayMember = "Texto";
            cboprovedor.ValueMember = "Valor";
            cboprovedor.SelectedIndex = 0;
        }

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

        private void ConfigurarFormatoGrilla()
        {
            dgvdata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvdata.Columns.Contains("FechaRegistro")) dgvdata.Columns["FechaRegistro"].FillWeight = 75;
            if (dgvdata.Columns.Contains("TipoDocumento")) dgvdata.Columns["TipoDocumento"].FillWeight = 85;
            if (dgvdata.Columns.Contains("NumeroDocumento")) dgvdata.Columns["NumeroDocumento"].FillWeight = 95;
            if (dgvdata.Columns.Contains("MontoTotal")) dgvdata.Columns["MontoTotal"].FillWeight = 80;
            if (dgvdata.Columns.Contains("UsuarioRegistro")) dgvdata.Columns["UsuarioRegistro"].FillWeight = 150;
            if (dgvdata.Columns.Contains("RTN")) dgvdata.Columns["RTN"].FillWeight = 115;
            if (dgvdata.Columns.Contains("RazonSocial")) dgvdata.Columns["RazonSocial"].FillWeight = 125;
            if (dgvdata.Columns.Contains("CodigoProducto")) dgvdata.Columns["CodigoProducto"].FillWeight = 90;
            if (dgvdata.Columns.Contains("NombreProducto")) dgvdata.Columns["NombreProducto"].FillWeight = 130;
            if (dgvdata.Columns.Contains("Categoria")) dgvdata.Columns["Categoria"].FillWeight = 90;
            if (dgvdata.Columns.Contains("PrecioCompra")) dgvdata.Columns["PrecioCompra"].FillWeight = 80;
            if (dgvdata.Columns.Contains("PrecioVenta")) dgvdata.Columns["PrecioVenta"].FillWeight = 80;
            if (dgvdata.Columns.Contains("Cantidad")) dgvdata.Columns["Cantidad"].FillWeight = 60;
            if (dgvdata.Columns.Contains("SubTotal")) dgvdata.Columns["SubTotal"].FillWeight = 90;
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