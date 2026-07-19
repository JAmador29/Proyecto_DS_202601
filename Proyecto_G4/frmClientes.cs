using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Proyecto_G4
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
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


            //MOSTRAR TODOS LOS USUARIOS
            List<Cliente> lista = new CN_Cliente().Listar();

            foreach (Cliente item in lista)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdCliente,item.Documento,item.NombreCompleto,item.Correo,item.Telefono,
                    item.Estado == true ? 1 :0,
                    item.Estado == true ? "Activo" : "No Activo" 
                });
            }
        }

        private bool ValidarLongitudCampos()
        {
            // Modifica los números (20, 150, 100, 50) según los tamaños reales de tu base de datos

            if (txtdocumento.Text.Trim().Length > 13)
            {
                MessageBox.Show("El campo 'Documento' no puede superar los 13 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdocumento.Focus();
                return false;
            }

            if (txtnombrecompleto.Text.Trim().Length > 50)
            {
                MessageBox.Show("El campo 'Nombre Completo' es demasiado largo. El máximo permitido son 50 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnombrecompleto.Focus();
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

            Cliente obj = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdCliente == 0)
            {
                int idgenerado = new CN_Cliente().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idgenerado,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txttelefono.Text,
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
                bool resultado = new CN_Cliente().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtnombrecompleto.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Telefono"].Value = txttelefono.Text;
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
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
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
                    txtnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
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
    }
    
}
