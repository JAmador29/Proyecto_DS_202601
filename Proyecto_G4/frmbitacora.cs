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
    public partial class frmbitacora : Form
    {
        private readonly CN_Bitacora _cnBitacora = new CN_Bitacora();

        public frmbitacora()
        {
            InitializeComponent();

            // Suscribir eventos principales
            this.Load += new EventHandler(frmbitacora_Load);
            this.iconButton1.Click += new EventHandler(btnbuscar_Click);
            this.btnlimpiarbuscador.Click += new EventHandler(btnlimpiarbuscador_Click);
            this.btnexportar.Click += new EventHandler(btnexportar_Click);
        }

        private void frmbitacora_Load(object sender, EventArgs e)
        {
            CargarComboBusqueda();
            CargarBitacoraEnGrilla();
        }

        #region Carga de Datos y Combos

        private void CargarComboBusqueda()
        {
            cbobusqueda.Items.Clear();

            // Mapea las columnas de dgvdata dinámicamente para el filtro
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible)
                {
                    cbobusqueda.Items.Add(new OpcionCombo()
                    {
                        Valor = columna.Name,
                        Texto = columna.HeaderText
                    });
                }
            }

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";

            if (cbobusqueda.Items.Count > 0)
                cbobusqueda.SelectedIndex = 0;
        }

        private void CargarBitacoraEnGrilla()
        {
            dgvdata.Rows.Clear();
            List<Bitacora> lista = _cnBitacora.Listar();

            foreach (Bitacora item in lista)
            {
                // Respeta estrictamente el orden de columnas definido en el Designer:
                // 1. Fecha | 2. ID_Bitacora | 3. Tablaafectada | 4. Usuario | 5. Accion | 6. detalle
                dgvdata.Rows.Add(new object[] {
                    item.FechaRegistro,
                    item.IdBitacora,
                    item.TablaAfectada,
                    item.oUsuario != null && !string.IsNullOrEmpty(item.oUsuario.NombreCompleto)
                        ? item.oUsuario.NombreCompleto
                        : (item.IdUsuario.HasValue ? $"ID: {item.IdUsuario.Value}" : "SISTEMA"),
                    item.Accion,
                    item.Detalle
                });
            }
        }

        #endregion

        #region Búsqueda y Filtros

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (cbobusqueda.SelectedItem == null) return;

            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();
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
                MessageBox.Show("No se encontraron resultados para la búsqueda especificada.", "Sin Coincidencias", (MessageBoxButtons)MessageBoxIcon.Information);
                MostrarTodasLasFilas();
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            MostrarTodasLasFilas();
        }

        private void MostrarTodasLasFilas()
        {
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        #endregion

        #region Exportar a Excel

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos en la bitácora para exportar.", "Mensaje", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();

            // Agregar nombres de encabezado de columnas visibles
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible)
                    dt.Columns.Add(columna.HeaderText, typeof(string));
            }

            // Agregar los valores de las filas visibles
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                if (row.Visible)
                {
                    List<object> valoresCelda = new List<object>();

                    foreach (DataGridViewColumn columna in dgvdata.Columns)
                    {
                        if (columna.Visible)
                        {
                            valoresCelda.Add(row.Cells[columna.Index].Value?.ToString() ?? "");
                        }
                    }

                    dt.Rows.Add(valoresCelda.ToArray());
                }
            }

            using (SaveFileDialog savefile = new SaveFileDialog())
            {
                savefile.FileName = $"Reporte_Bitacora_{DateTime.Now:ddMMyyyyHHmmss}.xlsx";
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add(dt, "Bitacora");
                            hoja.ColumnsUsed().AdjustToContents();
                            wb.SaveAs(savefile.FileName);
                            MessageBox.Show("Reporte de bitácora exportado exitosamente.", "Éxito", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al generar el archivo de Excel: " + ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion
    }
}