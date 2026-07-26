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

namespace Proyecto_G4
{
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
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


            //MOSTRAR TODOS LAS CATEGORIAS
            List<Categoria> lista = new CN_Categoria().Listar();

            foreach (Categoria item in lista)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdCategoria,
                    item.Descripcion,
                    item.Estado == true ? 1 :0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private bool ValidarLongitudCampos()
        {

            if (txtdescripcion.Text.Trim().Length > 100)
            {
                MessageBox.Show("El campo 'Descripción' no puede superar los 100 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdescripcion.Focus();
                return false;
            }
            return true; // Todos los campos cumplen con la longitud permitida
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // Validación de Longitudes (Llamada a la nueva función externa)
            if (!ValidarLongitudCampos())
            {
                return; // Se detiene porque la función interna ya mostró el MessageBox y dio Focus
            }

            Categoria obj = new Categoria()
            {
                IdCategoria = Convert.ToInt32(txtid.Text),
                Descripcion = txtdescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdCategoria == 0)
            {
                int idgenerada = new CN_Categoria().Registrar(obj, out mensaje);

                if (idgenerada != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idgenerada,txtdescripcion.Text,
                   ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString(),
                   ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString(),

                });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Categoria().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();
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
            txtdescripcion.Text = "";
            txtIndice.Text = "-1";
            txtid.Text = "0";
            cmbestado.SelectedIndex = 0;

            txtdescripcion.Select();
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
                    txtdescripcion.Text = dgvdata.Rows[indice].Cells["Descripcion"].Value.ToString();

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
