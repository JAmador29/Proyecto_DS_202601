using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capa_Entidad.Utilidades;
using Capa_Entidad;
using Capa_Negocio;

namespace Proyecto_G4
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {

            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;


            List<Rol> listaRol = new CN_Rol().Listar();

            foreach(Rol item in listaRol) {
                cmbrol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            cmbrol.DisplayMember = "Texto";
            cmbrol.ValueMember = "Valor";
            cmbrol.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cmbbusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText});
                }
            }
            cmbbusqueda.DisplayMember = "Texto";
            cmbbusqueda.ValueMember = "Valor";
            cmbbusqueda.SelectedIndex = 0;
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            dgvdata.Rows.Add(new object[] {"",Text,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txtclave.Text,
               ((OpcionCombo)cmbrol.SelectedItem).Valor.ToString(),
               ((OpcionCombo)cmbrol.SelectedItem).Texto.ToString(),
               ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString(),
               ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString(),

            });

            Limpiar();
        }

        private void Limpiar()
        {
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
            txtcorreo.Text = "";
            txtclave.Text = "";
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            cmbrol.SelectedIndex = 0;
            cmbestado.SelectedIndex = 0;
        }
    }
}
