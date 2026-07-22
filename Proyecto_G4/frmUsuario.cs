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

using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Proyecto_G4
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        CN_Usuario objCNUsuario = new CN_Usuario();

        private void frmUsuario_Load(object sender, EventArgs e)
        {

            cmbestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cmbestado.DisplayMember = "Texto";
            cmbestado.ValueMember = "Valor";
            cmbestado.SelectedIndex = 0;


            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                cmbrol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            cmbrol.DisplayMember = "Texto";
            cmbrol.ValueMember = "Valor";
            cmbrol.SelectedIndex = 0;



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
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado == true ? 1 :0,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.Bloqueado == true ? "Sí" : "No"
                });
            }
        }

        private void CargarUsuarios()
        {
            dgvdata.Rows.Clear();

            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvdata.Rows.Add(new object[]
                {
                    "",
                    item.IdUsuario,
                    item.Documento,
                    item.NombreCompleto,
                    item.Correo,
                    item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado ? 1 : 0,
                    item.Estado ? "Activo" : "No Activo",
                    item.Bloqueado ? "Sí" : "No"
                });
            }
        }

        //Metodo de validacion de longitudes
        private bool ValidarLongitudCampos()
        {
            // Modifica los números (20, 150, 100, 50) según los tamaños reales de tu base de datos

            if (txtdocumento.Text.Trim().Length > 13)
            {
                MessageBox.Show("El campo 'No. Documento' no puede superar los 13 caracteres.",
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

            if (txtclave.Text.Trim().Length > 255)
            {
                MessageBox.Show("La contraseña no puede superar los 255 caracteres.",
                                "Validación de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtclave.Focus();
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
            string Contraseña = txtclave.Text;
            string confirmarContraseña = txtconfirmarclave.Text;


            // 2. Validación de Longitudes (Llamada a la nueva función externa)
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

            if (!objCNUsuario.Validar_Contraseña(Contraseña, out mensaje))
            {
                MessageBox.Show(mensaje, "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Contraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Usuario objusuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Correo = txtcorreo.Text,
                Clave = txtclave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cmbrol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objusuario.IdUsuario == 0)
            {
                int idusuariogenerado = new CN_Usuario().Registrar(objusuario, out mensaje);

                if (idusuariogenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] 
                    {
                        "",idusuariogenerado,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txtclave.Text,
                        ((OpcionCombo)cmbrol.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cmbrol.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString(),
                        "No"
                    });
                    MessageBox.Show("Se registró el usuario con éxito.", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Usuario().Editar(objusuario, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtnombrecompleto.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Clave"].Value = txtclave.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cmbrol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cmbrol.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).Texto.ToString();
                    MessageBox.Show("Se editó el usuario con éxito.", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtclave.Text = "";
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            cmbrol.SelectedIndex = 0;
            cmbestado.SelectedIndex = 0;

            txtdocumento.Select();
        }

        /*private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.gettyimages_1696263143_612x612.Width;
                var h = Properties.Resources.gettyimages_1696263143_612x612.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Left + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.gettyimages_1696263143_612x612, new Rectangle(x, y, w, h));
                e.Handled = true;
            }


        }*/

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                txtclave.Enabled = false;
                txtconfirmarclave.Enabled = false;

                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    txtIndice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    txtnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();

                    foreach(OpcionCombo oc in cmbrol.Items)
                    {
                        if(Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cmbrol.Items.IndexOf(oc);
                            cmbrol.SelectedIndex = indice_combo;
                            break;

                        }
                    }

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (dgvdata.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario.");
                return;
            }

            bool bloqueado = dgvdata.CurrentRow.Cells["Bloqueado"].Value.ToString() == "Sí";

            if(!bloqueado) 
            {
                MessageBox.Show("El usuario seleccionado no está bloqueado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idUsuario = Convert.ToInt32(
                dgvdata.CurrentRow.Cells["Id"].Value
            );


            string nombre = dgvdata.CurrentRow.Cells["NombreCompleto"].Value.ToString();


            string mensaje;


            CN_Usuario objCN = new CN_Usuario();


            if (objCN.Desbloquear_Usuario(idUsuario, out mensaje))
            {

                objCN.Registrar_Bitacora(idUsuario, "DESBLOQUEO", $"IdUsuario={idUsuario}, Nombre={nombre}", out mensaje);


                MessageBox.Show("Usuario desbloqueado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
