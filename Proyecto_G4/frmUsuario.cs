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
    public partial class frmUsuario : Form
    {
        private const int LongitudMaximaDocumento = 13;
        private const int LongitudMaximaNombre = 50;
        private const int LongitudMaximaCorreo = 50;
        private const int LongitudMaximaClave = 255;

        private readonly CN_Usuario usuarioNegocio;
        private readonly CN_Rol rolNegocio;

        private static readonly string[] DominiosPermitidos =
        {
            "gmail.com",
            "yahoo.com",
            "outlook.com",
            "hotmail.com"
        };

        public frmUsuario()
        {
            InitializeComponent();

            usuarioNegocio = new CN_Usuario();
            rolNegocio = new CN_Rol();
        }

        // ================================================================
        // CARGA INICIAL
        // ================================================================

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            ConfigurarComboEstado();
            CargarRoles();
            ConfigurarComboBusqueda();
            CargarUsuarios();
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

        private void CargarRoles()
        {
            cmbrol.Items.Clear();

            List<Rol> roles = rolNegocio.Listar();

            foreach (Rol rol in roles)
            {
                cmbrol.Items.Add(new OpcionCombo
                {
                    Valor = rol.IdRol,
                    Texto = rol.Descripcion
                });
            }

            ConfigurarCombo(cmbrol);
        }

        private void ConfigurarComboBusqueda()
        {
            cmbbusqueda.Items.Clear();

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                bool sePuedeBuscar =
                    columna.Visible &&
                    columna.Name != "btnSeleccionar" &&
                    columna.Name != "Clave";

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
        // USUARIOS
        // ================================================================

        private void CargarUsuarios()
        {
            dgvdata.Rows.Clear();

            List<Usuario> usuarios = usuarioNegocio.Listar();

            foreach (Usuario usuario in usuarios)
                AgregarUsuarioAlGrid(usuario);
        }

        private void AgregarUsuarioAlGrid(Usuario usuario)
        {
            dgvdata.Rows.Add(
                "",
                usuario.IdUsuario,
                usuario.Documento,
                usuario.NombreCompleto,
                usuario.Correo,
                usuario.Clave,
                usuario.oRol?.IdRol ?? 0,
                usuario.oRol?.Descripcion ?? string.Empty,
                usuario.Estado ? 1 : 0,
                usuario.Estado ? "Activo" : "No Activo",
                usuario.Bloqueado ? "Sí" : "No"
            );
        }

        private Usuario CrearUsuarioDesdeFormulario()
        {
            return new Usuario
            {
                IdUsuario = ObtenerIdUsuario(),
                Documento = txtdocumento.Text.Trim(),
                NombreCompleto = txtnombrecompleto.Text.Trim(),
                Correo = txtcorreo.Text.Trim(),
                Clave = txtclave.Text,
                oRol = new Rol
                {
                    IdRol = ObtenerValorCombo(cmbrol)
                },
                Estado = ObtenerValorCombo(cmbestado) == 1
            };
        }

        private int ObtenerIdUsuario()
        {
            return int.TryParse(txtid.Text, out int idUsuario)
                ? idUsuario
                : 0;
        }

        private static int ObtenerValorCombo(ComboBox combo)
        {
            if (combo.SelectedItem is OpcionCombo opcion)
                return Convert.ToInt32(opcion.Valor);

            return 0;
        }

        private static string ObtenerTextoCombo(ComboBox combo)
        {
            if (combo.SelectedItem is OpcionCombo opcion)
                return opcion.Texto?.ToString() ?? string.Empty;

            return string.Empty;
        }

        // ================================================================
        // GUARDAR Y EDITAR
        // ================================================================

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            Usuario usuario = CrearUsuarioDesdeFormulario();

            if (usuario.IdUsuario == 0)
                RegistrarUsuario(usuario);
            else
                EditarUsuario(usuario);
        }

        private void RegistrarUsuario(Usuario usuario)
        {
            int idUsuario = usuarioNegocio.Registrar(
                usuario,
                out string mensaje);

            if (idUsuario == 0)
            {
                MostrarError(mensaje, "Error al registrar");
                return;
            }

            usuario.IdUsuario = idUsuario;
            usuario.oRol.Descripcion = ObtenerTextoCombo(cmbrol);
            usuario.Bloqueado = false;

            AgregarUsuarioAlGrid(usuario);

            MostrarInformacion(
                "Se registró el usuario con éxito.",
                "Usuario registrado");

            LimpiarFormulario();
        }

        private void EditarUsuario(Usuario usuario)
        {
            bool resultado = usuarioNegocio.Editar(
                usuario,
                out string mensaje);

            if (!resultado)
            {
                MostrarError(mensaje, "Error al editar");
                return;
            }

            ActualizarFilaSeleccionada(usuario);

            MostrarInformacion(
                "Se editó el usuario con éxito.",
                "Usuario editado");

            LimpiarFormulario();
        }

        private void ActualizarFilaSeleccionada(Usuario usuario)
        {
            if (!int.TryParse(txtIndice.Text, out int indice))
                return;

            if (indice < 0 || indice >= dgvdata.Rows.Count)
                return;

            DataGridViewRow fila = dgvdata.Rows[indice];

            fila.Cells["Id"].Value = usuario.IdUsuario;
            fila.Cells["Documento"].Value = usuario.Documento;
            fila.Cells["NombreCompleto"].Value = usuario.NombreCompleto;
            fila.Cells["Correo"].Value = usuario.Correo;
            fila.Cells["Clave"].Value = usuario.Clave;
            fila.Cells["IdRol"].Value = usuario.oRol.IdRol;
            fila.Cells["Rol"].Value = ObtenerTextoCombo(cmbrol);
            fila.Cells["EstadoValor"].Value = usuario.Estado ? 1 : 0;
            fila.Cells["Estado"].Value =
                usuario.Estado ? "Activo" : "No Activo";
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

            if (!ValidarClave())
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

            if (ObtenerIdUsuario() == 0 &&
                string.IsNullOrWhiteSpace(txtclave.Text))
            {
                MostrarAdvertencia(
                    "El campo contraseña es obligatorio.",
                    "Campo obligatorio");

                txtclave.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarEspaciosIniciales()
        {
            if (TieneEspacioInicial(txtdocumento.Text))
            {
                MostrarAdvertencia(
                    "El documento no puede comenzar con espacios.",
                    "Espacios no permitidos");

                txtdocumento.Focus();
                return false;
            }

            if (TieneEspacioInicial(txtnombrecompleto.Text))
            {
                MostrarAdvertencia(
                    "El nombre no puede comenzar con espacios.",
                    "Espacios no permitidos");

                txtnombrecompleto.Focus();
                return false;
            }

            if (TieneEspacioInicial(txtcorreo.Text))
            {
                MostrarAdvertencia(
                    "El correo no puede comenzar con espacios.",
                    "Espacios no permitidos");

                txtcorreo.Focus();
                return false;
            }

            if (TieneEspacioInicial(txtclave.Text))
            {
                MostrarAdvertencia(
                    "La contraseña no puede comenzar con espacios.",
                    "Espacios no permitidos");

                txtclave.Focus();
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
                    "El correo no puede superar los 50 caracteres."))
            {
                return false;
            }

            if (!ValidarLongitud(
                    txtclave,
                    LongitudMaximaClave,
                    "La contraseña no puede superar los 255 caracteres."))
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
                MailAddress direccion = new MailAddress(correo);
                string dominio = direccion.Host.ToLowerInvariant();

                return DominiosPermitidos.Contains(dominio);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool ValidarClave()
        {
            // Si está editando, la contraseña actual se conserva
            // y será procesada por el procedimiento almacenado.
            if (!txtclave.Enabled)
                return true;

            string clave = txtclave.Text;
            string confirmarClave = txtconfirmarclave.Text;
            string mensaje;

            if (!usuarioNegocio.Validar_Contraseña(clave, out mensaje))
            {
                MostrarAdvertencia(
                    mensaje,
                    "Contraseña inválida");

                txtclave.Focus();
                return false;
            }

            if (clave != confirmarClave)
            {
                MostrarError(
                    "Las contraseñas no coinciden. Por favor, inténtelo nuevamente.",
                    "Contraseñas diferentes");

                txtconfirmarclave.Focus();
                return false;
            }

            return true;
        }

        // ================================================================
        // SELECCIÓN DEL GRID
        // ================================================================

        private void dgvdata_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "btnSeleccionar")
                return;

            SeleccionarUsuario(e.RowIndex);
        }

        private void SeleccionarUsuario(int indice)
        {
            DataGridViewRow fila = dgvdata.Rows[indice];

            txtIndice.Text = indice.ToString();
            txtid.Text = ObtenerValorCelda(fila, "Id");
            txtdocumento.Text = ObtenerValorCelda(fila, "Documento");
            txtnombrecompleto.Text = ObtenerValorCelda(fila, "NombreCompleto");
            txtcorreo.Text = ObtenerValorCelda(fila, "Correo");

            // Se mantiene la contraseña porque es utilizada
            // por el procedimiento almacenado durante la edición.
            txtclave.Text = ObtenerValorCelda(fila, "Clave");
            txtconfirmarclave.Text = txtclave.Text;

            txtclave.Enabled = false;
            txtconfirmarclave.Enabled = false;

            SeleccionarOpcionCombo(
                cmbrol,
                ObtenerValorCelda(fila, "IdRol"));

            SeleccionarOpcionCombo(
                cmbestado,
                ObtenerValorCelda(fila, "EstadoValor"));
        }

        private static string ObtenerValorCelda(
            DataGridViewRow fila,
            string nombreColumna)
        {
            return fila.Cells[nombreColumna].Value?.ToString()
                   ?? string.Empty;
        }

        private static void SeleccionarOpcionCombo(
            ComboBox combo,
            string valor)
        {
            foreach (OpcionCombo opcion in combo.Items)
            {
                if (opcion.Valor.ToString() != valor)
                    continue;

                combo.SelectedIndex = combo.Items.IndexOf(opcion);
                return;
            }
        }

        // ================================================================
        // BÚSQUEDA
        // ================================================================

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (!(cmbbusqueda.SelectedItem is OpcionCombo opcion))
                return;

            string columna = opcion.Valor.ToString();
            string textoBusqueda = txtbusqueda.Text.Trim();

            bool encontrado = FiltrarUsuarios(
                columna,
                textoBusqueda);

            if (encontrado)
                return;

            MostrarInformacion(
                "No se encontraron resultados para su búsqueda.",
                "Sin resultados");

            MostrarTodasLasFilas();
        }

        private bool FiltrarUsuarios(
            string columna,
            string textoBusqueda)
        {
            bool encontrado = false;

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string valor =
                    fila.Cells[columna].Value?.ToString()
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
        // DESBLOQUEO
        // ================================================================

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = dgvdata.CurrentRow;

            if (fila == null || fila.IsNewRow)
            {
                MostrarAdvertencia(
                    "Seleccione un usuario.",
                    "Usuario no seleccionado");

                return;
            }

            if (!UsuarioFilaBloqueado(fila))
            {
                MostrarInformacion(
                    "El usuario seleccionado no está bloqueado.",
                    "Información");

                return;
            }

            DesbloquearUsuario(fila);
        }

        private static bool UsuarioFilaBloqueado(
            DataGridViewRow fila)
        {
            string valor =
                fila.Cells["Bloqueado"].Value?.ToString();

            return valor == "Sí";
        }

        private void DesbloquearUsuario(DataGridViewRow fila)
        {
            int idUsuario = Convert.ToInt32(
                fila.Cells["Id"].Value);

            string nombre = fila.Cells["NombreCompleto"]
                .Value?.ToString() ?? string.Empty;

            bool resultado = usuarioNegocio.Desbloquear_Usuario(
                idUsuario,
                out string mensaje);

            if (!resultado)
            {
                MostrarError(
                    mensaje,
                    "Error al desbloquear");

                return;
            }

            RegistrarDesbloqueo(
                idUsuario,
                nombre);

            MostrarInformacion(
                "Usuario desbloqueado correctamente.",
                "Usuario desbloqueado");

            CargarUsuarios();
        }

        private void RegistrarDesbloqueo(
            int idUsuario,
            string nombre)
        {
            usuarioNegocio.Registrar_Bitacora(
                idUsuario,
                "DESBLOQUEO",
                $"IdUsuario={idUsuario}, Nombre={nombre}",
                out _);
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
            txtclave.Clear();
            txtconfirmarclave.Clear();

            if (cmbrol.Items.Count > 0)
                cmbrol.SelectedIndex = 0;

            if (cmbestado.Items.Count > 0)
                cmbestado.SelectedIndex = 0;

            txtclave.Enabled = true;
            txtconfirmarclave.Enabled = true;

            txtdocumento.Focus();
        }

        // ================================================================
        // VALIDACIÓN DE TECLADO
        // ================================================================

        private void txtdocumento_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            bool esNumero = char.IsDigit(e.KeyChar);
            bool esControl = char.IsControl(e.KeyChar);

            if (!esNumero && !esControl)
                e.Handled = true;
        }

        // ================================================================
        // FORMATO DEL GRID
        // ================================================================

        private void dgvdata_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string nombreColumna =
                dgvdata.Columns[e.ColumnIndex].Name;

            string valor = e.Value?.ToString();

            if (nombreColumna == "Estado")
                FormatearEstado(e, valor);

            if (nombreColumna == "Bloqueado")
                FormatearBloqueo(e, valor);
        }

        private static void FormatearEstado(
            DataGridViewCellFormattingEventArgs e,
            string estado)
        {
            if (estado == "Activo")
            {
                AplicarFormatoCelda(
                    e,
                    Color.FromArgb(39, 174, 96),
                    Color.FromArgb(46, 204, 113));

                return;
            }

            if (estado == "No Activo")
            {
                AplicarFormatoCelda(
                    e,
                    Color.FromArgb(192, 57, 43),
                    Color.FromArgb(231, 76, 60));
            }
        }

        private static void FormatearBloqueo(
            DataGridViewCellFormattingEventArgs e,
            string bloqueado)
        {
            if (bloqueado == "Sí")
            {
                AplicarFormatoCelda(
                    e,
                    Color.FromArgb(255, 128, 0),
                    Color.FromArgb(255, 170, 0));

                return;
            }

            if (bloqueado == "No")
            {
                AplicarFormatoCelda(
                    e,
                    Color.FromArgb(0, 128, 255),
                    Color.FromArgb(0, 170, 255));
            }
        }

        private static void AplicarFormatoCelda(
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