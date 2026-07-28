using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;

namespace Proyecto_G4
{
    public partial class frmClientes : Form
    {
        private const int LongitudMaximaDocumento = 13;
        private const int LongitudMaximaNombre = 50;
        private const int LongitudMaximaCorreo = 50;
        private const int LongitudMaximaTelefono = 8;

        private readonly CN_Cliente clienteNegocio;

        private static readonly string[] DominiosPermitidos =
        {
            "gmail.com",
            "yahoo.com",
            "outlook.com",
            "hotmail.com"
        };

        public frmClientes()
        {
            InitializeComponent();
            clienteNegocio = new CN_Cliente();
        }

        // ================================================================
        // CARGA DEL FORMULARIO
        // ================================================================

        private void frmClientes_Load(object sender, EventArgs e)
        {
            ConfigurarComboEstado();
            ConfigurarComboBusqueda();
            CargarClientes();
            LimpiarFormulario();
        }

        private void ConfigurarComboEstado()
        {
            cmbestado.Items.Clear();

            cmbestado.Items.Add(new OpcionCombo
            {
                Valor = 1,
                Texto = "Activo"
            });

            cmbestado.Items.Add(new OpcionCombo
            {
                Valor = 0,
                Texto = "No Activo"
            });

            ConfigurarCombo(cmbestado);
        }

        private void ConfigurarComboBusqueda()
        {
            cmbbusqueda.Items.Clear();

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                bool sePuedeBuscar =
                    columna.Visible &&
                    columna.Name != "btnSeleccionar";

                if (!sePuedeBuscar)
                    continue;

                cmbbusqueda.Items.Add(new OpcionCombo
                {
                    Valor = columna.Name,
                    Texto = columna.HeaderText
                });
            }

            ConfigurarCombo(cmbbusqueda);
        }

        private static void ConfigurarCombo(ComboBox combo)
        {
            combo.DisplayMember = "Texto";
            combo.ValueMember = "Valor";

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        // ================================================================
        // CARGA DE CLIENTES
        // ================================================================

        private void CargarClientes()
        {
            dgvdata.Rows.Clear();

            List<Cliente> clientes = clienteNegocio.Listar();

            foreach (Cliente cliente in clientes)
                AgregarClienteAlGrid(cliente);
        }

        private void AgregarClienteAlGrid(Cliente cliente)
        {
            dgvdata.Rows.Add(
                "",
                cliente.IdCliente,
                cliente.Documento,
                cliente.NombreCompleto,
                cliente.Correo,
                cliente.Telefono,
                cliente.Estado ? 1 : 0,
                cliente.Estado ? "Activo" : "No Activo"
            );
        }

        // ================================================================
        // GUARDAR Y EDITAR
        // ================================================================

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            Cliente cliente = CrearClienteDesdeFormulario();

            if (cliente.IdCliente == 0)
                RegistrarCliente(cliente);
            else
                EditarCliente(cliente);
        }

        private Cliente CrearClienteDesdeFormulario()
        {
            return new Cliente
            {
                IdCliente = ObtenerIdCliente(),
                Documento = txtdocumento.Text.Trim(),
                NombreCompleto = txtnombrecompleto.Text.Trim(),
                Correo = txtcorreo.Text.Trim(),
                Telefono = txttelefono.Text.Trim(),
                Estado = ObtenerValorEstado() == 1
            };
        }

        private int ObtenerIdCliente()
        {
            return int.TryParse(txtid.Text, out int idCliente)
                ? idCliente
                : 0;
        }

        private int ObtenerValorEstado()
        {
            if (cmbestado.SelectedItem is OpcionCombo opcion)
                return Convert.ToInt32(opcion.Valor);

            return 0;
        }

        private string ObtenerTextoEstado()
        {
            if (cmbestado.SelectedItem is OpcionCombo opcion)
                return opcion.Texto?.ToString() ?? string.Empty;

            return string.Empty;
        }

        private void RegistrarCliente(Cliente cliente)
        {
            int idCliente = clienteNegocio.Registrar(
                cliente,
                out string mensaje);

            if (idCliente == 0)
            {
                MostrarError(mensaje, "Error al registrar");
                return;
            }

            cliente.IdCliente = idCliente;

            AgregarClienteAlGrid(cliente);

            MostrarInformacion(
                "El cliente se registró correctamente.",
                "Cliente registrado");

            LimpiarFormulario();
        }

        private void EditarCliente(Cliente cliente)
        {
            bool resultado = clienteNegocio.Editar(
                cliente,
                out string mensaje);

            if (!resultado)
            {
                MostrarError(mensaje, "Error al editar");
                return;
            }

            ActualizarFilaSeleccionada(cliente);

            MostrarInformacion(
                "El cliente se editó correctamente.",
                "Cliente editado");

            LimpiarFormulario();
        }

        private void ActualizarFilaSeleccionada(Cliente cliente)
        {
            if (!int.TryParse(txtIndice.Text, out int indice))
                return;

            if (indice < 0 || indice >= dgvdata.Rows.Count)
                return;

            DataGridViewRow fila = dgvdata.Rows[indice];

            fila.Cells["Id"].Value = cliente.IdCliente;
            fila.Cells["Documento"].Value = cliente.Documento;
            fila.Cells["NombreCompleto"].Value = cliente.NombreCompleto;
            fila.Cells["Correo"].Value = cliente.Correo;
            fila.Cells["Telefono"].Value = cliente.Telefono;
            fila.Cells["EstadoValor"].Value = cliente.Estado ? 1 : 0;
            fila.Cells["Estado"].Value = ObtenerTextoEstado();
        }

        // ================================================================
        // VALIDACIONES
        // ================================================================

        private bool ValidarFormulario()
        {
            if (!ValidarCamposObligatorios())
                return false;

            if (!ValidarEspaciosIniciales())
                return false;

            if (!ValidarLongitudCampos())
                return false;

            if (!ValidarCorreo())
                return false;

            return true;
        }

        private bool ValidarCamposObligatorios()
        {
            if (string.IsNullOrWhiteSpace(txtdocumento.Text))
            {
                MostrarAdvertencia(
                    "El campo documento es obligatorio.",
                    "Campo obligatorio");

                txtdocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtnombrecompleto.Text))
            {
                MostrarAdvertencia(
                    "El campo nombre completo es obligatorio.",
                    "Campo obligatorio");

                txtnombrecompleto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtcorreo.Text))
            {
                MostrarAdvertencia(
                    "El campo correo es obligatorio.",
                    "Campo obligatorio");

                txtcorreo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txttelefono.Text))
            {
                MostrarAdvertencia(
                    "El campo teléfono es obligatorio.",
                    "Campo obligatorio");

                txttelefono.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarEspaciosIniciales()
        {
            if (TieneEspacioInicial(txtdocumento.Text))
            {
                MostrarAdvertencia(
                    "El documento no puede comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txtdocumento.Focus();
                return false;
            }

            if (TieneEspacioInicial(txtnombrecompleto.Text))
            {
                MostrarAdvertencia(
                    "El nombre completo no puede comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txtnombrecompleto.Focus();
                return false;
            }

            if (TieneEspacioInicial(txtcorreo.Text))
            {
                MostrarAdvertencia(
                    "El correo no puede comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txtcorreo.Focus();
                return false;
            }

            if (TieneEspacioInicial(txttelefono.Text))
            {
                MostrarAdvertencia(
                    "El teléfono no puede comenzar con espacios en blanco.",
                    "Espacios no permitidos");

                txttelefono.Focus();
                return false;
            }

            return true;
        }

        private static bool TieneEspacioInicial(string texto)
        {
            return !string.IsNullOrEmpty(texto) &&
                   char.IsWhiteSpace(texto[0]);
        }

        private bool ValidarLongitudCampos()
        {
            if (!ValidarLongitud(
                    txtdocumento,
                    LongitudMaximaDocumento,
                    "El documento no puede superar los 13 caracteres."))
            {
                return false;
            }

            if (!ValidarLongitud(
                    txtnombrecompleto,
                    LongitudMaximaNombre,
                    "El nombre completo no puede superar los 50 caracteres."))
            {
                return false;
            }

            if (!ValidarLongitud(
                    txtcorreo,
                    LongitudMaximaCorreo,
                    "El correo electrónico no puede superar los 50 caracteres."))
            {
                return false;
            }

            if (!ValidarLongitud(
                    txttelefono,
                    LongitudMaximaTelefono,
                    "El teléfono no puede superar los 8 caracteres."))
            {
                return false;
            }

            return true;
        }

        private bool ValidarLongitud(
            TextBox campo,
            int longitudMaxima,
            string mensaje)
        {
            if (campo.Text.Trim().Length <= longitudMaxima)
                return true;

            MostrarAdvertencia(
                mensaje,
                "Validación de longitud");

            campo.Focus();
            return false;
        }

        private bool ValidarCorreo()
        {
            string correo = txtcorreo.Text.Trim();

            if (CorreoValido(correo))
                return true;

            MostrarAdvertencia(
                "Ingrese un correo electrónico válido.\n\n" +
                "Solo se permiten los dominios gmail.com, yahoo.com, " +
                "outlook.com y hotmail.com.",
                "Validación de correo");

            txtcorreo.Focus();
            return false;
        }

        private static bool CorreoValido(string correo)
        {
            const string PatronCorreo =
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrWhiteSpace(correo))
                return false;

            if (!Regex.IsMatch(correo, PatronCorreo))
                return false;

            try
            {
                MailAddress direccionCorreo = new MailAddress(correo);

                string dominio =
                    direccionCorreo.Host.ToLowerInvariant().Trim();

                return DominiosPermitidos.Contains(dominio);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        // ================================================================
        // SELECCIÓN DEL DATAGRIDVIEW
        // ================================================================

        private void dgvdata_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "btnSeleccionar")
                return;

            SeleccionarCliente(e.RowIndex);
        }

        private void SeleccionarCliente(int indice)
        {
            DataGridViewRow fila = dgvdata.Rows[indice];

            txtIndice.Text = indice.ToString();
            txtid.Text = ObtenerValorCelda(fila, "Id");
            txtdocumento.Text = ObtenerValorCelda(fila, "Documento");
            txtnombrecompleto.Text =
                ObtenerValorCelda(fila, "NombreCompleto");
            txtcorreo.Text = ObtenerValorCelda(fila, "Correo");
            txttelefono.Text = ObtenerValorCelda(fila, "Telefono");

            SeleccionarEstado(
                ObtenerValorCelda(fila, "EstadoValor"));
        }

        private static string ObtenerValorCelda(
            DataGridViewRow fila,
            string nombreColumna)
        {
            return fila.Cells[nombreColumna].Value?.ToString()
                   ?? string.Empty;
        }

        private void SeleccionarEstado(string valorEstado)
        {
            foreach (OpcionCombo opcion in cmbestado.Items)
            {
                if (opcion.Valor.ToString() != valorEstado)
                    continue;

                cmbestado.SelectedIndex =
                    cmbestado.Items.IndexOf(opcion);

                return;
            }
        }

        // ================================================================
        // BÚSQUEDA
        // ================================================================

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (!(cmbbusqueda.SelectedItem is OpcionCombo opcionBusqueda))
                return;

            string columnaFiltro =
                opcionBusqueda.Valor.ToString();

            string textoBusqueda =
                txtbusqueda.Text.Trim();

            bool encontrado = FiltrarClientes(
                columnaFiltro,
                textoBusqueda);

            if (encontrado)
                return;

            MostrarInformacion(
                "No se encontraron resultados para su búsqueda.",
                "Sin resultados");

            MostrarTodasLasFilas();
        }

        private bool FiltrarClientes(
            string columnaFiltro,
            string textoBusqueda)
        {
            bool encontrado = false;

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string valor =
                    fila.Cells[columnaFiltro].Value?.ToString()
                    ?? string.Empty;

                bool coincide = valor.IndexOf(
                    textoBusqueda,
                    StringComparison.OrdinalIgnoreCase) >= 0;

                fila.Visible = coincide;

                if (coincide)
                    encontrado = true;
            }

            return encontrado;
        }

        private void btnlimpiarbuscador_Click(
            object sender,
            EventArgs e)
        {
            txtbusqueda.Clear();
            MostrarTodasLasFilas();
        }

        private void MostrarTodasLasFilas()
        {
            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (!fila.IsNewRow)
                    fila.Visible = true;
            }
        }

        // ================================================================
        // LIMPIEZA
        // ================================================================

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Clear();
            txtnombrecompleto.Clear();
            txtcorreo.Clear();
            txttelefono.Clear();

            if (cmbestado.Items.Count > 0)
                cmbestado.SelectedIndex = 0;

            txtdocumento.Focus();
        }

        // ================================================================
        // VALIDACIÓN DE TECLADO
        // ================================================================

        private void SoloNumeros_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            bool esNumero = char.IsDigit(e.KeyChar);
            bool esTeclaControl = char.IsControl(e.KeyChar);

            if (!esNumero && !esTeclaControl)
                e.Handled = true;
        }

        // ================================================================
        // FORMATO DEL DATAGRIDVIEW
        // ================================================================

        private void dgvdata_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "Estado")
                return;

            string estado = e.Value?.ToString();

            FormatearEstado(e, estado);
        }

        private static void FormatearEstado(
            DataGridViewCellFormattingEventArgs e,
            string estado)
        {
            if (estado == "Activo")
            {
                AplicarFormatoEstado(
                    e,
                    Color.FromArgb(39, 174, 96),
                    Color.FromArgb(46, 204, 113));

                return;
            }

            if (estado == "No Activo")
            {
                AplicarFormatoEstado(
                    e,
                    Color.FromArgb(192, 57, 43),
                    Color.FromArgb(231, 76, 60));
            }
        }

        private static void AplicarFormatoEstado(
            DataGridViewCellFormattingEventArgs e,
            Color colorTexto,
            Color colorSeleccion)
        {
            e.CellStyle.ForeColor = colorTexto;
            e.CellStyle.SelectionBackColor = colorSeleccion;
            e.CellStyle.SelectionForeColor = Color.White;
        }

        // ================================================================
        // MENSAJES
        // ================================================================

        private static void MostrarAdvertencia(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private static void MostrarInformacion(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static void MostrarError(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}