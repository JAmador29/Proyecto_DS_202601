using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.Y2022.FeaturePropertyBag;
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
    public partial class frmReporteCompras : Form
    {
        public frmReporteCompras()
        {
            InitializeComponent();


        }
        
        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            List<Proveedor> lista = new CN_Proveedor().Listar();

            cboprovedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "TODOS" });
            foreach(Proveedor item in lista)
            {
                cboprovedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial});
            }
            cboprovedor.DisplayMember = "Texto";
            cboprovedor.ValueMember = "Valor";
            cboprovedor.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            dgvdata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvdata.Columns["FechaRegistro"].FillWeight = 75;
            dgvdata.Columns["TipoDocumento"].FillWeight = 85;
            dgvdata.Columns["NumeroDocumento"].FillWeight = 95;
            dgvdata.Columns["MontoTotal"].FillWeight = 80;
            dgvdata.Columns["UsuarioRegistro"].FillWeight = 150;
            dgvdata.Columns["RTN"].FillWeight = 115;
            dgvdata.Columns["RazonSocial"].FillWeight = 125;
            dgvdata.Columns["CodigoProducto"].FillWeight = 90;
            dgvdata.Columns["NombreProducto"].FillWeight = 130;
            dgvdata.Columns["Categoria"].FillWeight = 90;
            dgvdata.Columns["PrecioCompra"].FillWeight = 80;
            dgvdata.Columns["PrecioVenta"].FillWeight = 80;
            dgvdata.Columns["Cantidad"].FillWeight = 60;
            dgvdata.Columns["SubTotal"].FillWeight = 90;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idproveedor = Convert.ToInt32(((OpcionCombo)cboprovedor.SelectedItem).Valor.ToString());

                List<ReporteCompra> lista = new CN_Reporte().Compra(txtfechainicio.Value, txtfechafin.Value, idproveedor);

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
                    MessageBox.Show("No se encontraron compras en el rango de fechas seleccionado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvdata.Columns)
                {
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString()
                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void cbobusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
