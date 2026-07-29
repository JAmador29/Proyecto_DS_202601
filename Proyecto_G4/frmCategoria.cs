using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;

namespace Proyecto_G4
{
    /// <summary>
    /// Formulario de gestión y mantenimiento de Categorías.
    /// Aplica principios SOLID, encapsulamiento defensivo y validación visual.
    /// </summary>
    public partial class frmCategoria : Form
    {
        private const int LongitudMaximaDescripcion = 100;

        private readonly CN_Categoria _categoriaNegocio;
        private readonly Usuario _usuarioActual;

        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmCategoria"/>.
        /// Compatible con el diseñador visual de WinForms y soporta inyección de sesión de usuario.
        /// </summary>
        /// <param name="usuario">Usuario con la sesión activa.</param>
        public frmCategoria(Usuario usuario = null)
        {
            InitializeComponent();
            _categoriaNegocio = new CN_Categoria();
            _usuarioActual = usuario;
        }

        #endregion

        #region Carga del Formulario

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            ConfigurarComboEstado();
            ConfigurarComboBusqueda();
            CargarCategorias();
            LimpiarFormulario();
        }

        private void ConfigurarComboEstado()
        {
            cmbestado.Items.Clear();

            cmbestado.Items.Add(new OpcionCombo { Valor = 1, Texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo { Valor = 0, Texto = "No Activo" });

            ConfigurarCombo(cmbestado);
        }

        private void ConfigurarComboBusqueda()
        {
            cmbbusqueda.Items.Clear();

            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                bool sePuedeBuscar = columna.Visible && columna.Name != "btnSeleccionar";

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

        #endregion

        #region Carga de Categorías

        private void CargarCategorias()
        {
            dgvdata.Rows.Clear();

            List<Categoria> categorias = _categoriaNegocio.Listar();

            foreach (Categoria categoria in categorias)
            {
                AgregarCategoriaAlGrid(categoria);
            }
        }

        private void AgregarCategoriaAlGrid(Categoria categoria)
        {
            dgvdata.Rows.Add(
                "",
                categoria.IdCategoria,
                categoria.Descripcion,
                categoria.Estado ? 1 : 0,
                categoria.Estado ? "Activo" : "No Activo"
            );
        }

        #endregion

        #region Guardar y Editar

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            Categoria categoria = CrearCategoriaDesdeFormulario();

            if (categoria.IdCategoria == 0)
            {
                RegistrarCategoria(categoria);
            }
            else
            {
                EditarCategoria(categoria);
            }
        }

        private Categoria CrearCategoriaDesdeFormulario()
        {
            return new Categoria
            {
                IdCategoria = ObtenerIdCategoria(),
                Descripcion = txtdescripcion.Text.Trim(),
                Estado = ObtenerValorEstado() == 1,
                oUsuario = _usuarioActual // Se adjunta la sesión para el registro de auditoría/bitácora
            };
        }

        private int ObtenerIdCategoria()
        {
            return int.TryParse(txtid.Text, out int idCategoria) ? idCategoria : 0;
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

        private void RegistrarCategoria(Categoria categoria)
        {
            int idCategoria = _categoriaNegocio.Registrar(categoria, out string mensaje);

            if (idCategoria == 0)
            {
                MostrarError(mensaje, "Error al registrar");
                return;
            }

            categoria.IdCategoria = idCategoria;
            AgregarCategoriaAlGrid(categoria);

            MostrarInformacion("La categoría se registró correctamente.", "Categoría registrada");
            LimpiarFormulario();
        }

        private void EditarCategoria(Categoria categoria)
        {
            bool resultado = _categoriaNegocio.Editar(categoria, out string mensaje);

            if (!resultado)
            {
                MostrarError(mensaje, "Error al editar");
                return;
            }

            ActualizarFilaSeleccionada(categoria);

            MostrarInformacion("La categoría se editó correctamente.", "Categoría editada");
            LimpiarFormulario();
        }

        private void ActualizarFilaSeleccionada(Categoria categoria)
        {
            if (!int.TryParse(txtIndice.Text, out int indice))
                return;

            if (indice < 0 || indice >= dgvdata.Rows.Count)
                return;

            DataGridViewRow fila = dgvdata.Rows[indice];

            fila.Cells["Id"].Value = categoria.IdCategoria;
            fila.Cells["Descripcion"].Value = categoria.Descripcion;
            fila.Cells["EstadoValor"].Value = categoria.Estado ? 1 : 0;
            fila.Cells["Estado"].Value = ObtenerTextoEstado();
        }

        #endregion

        #region Validaciones

        private bool ValidarFormulario()
        {
            if (!ValidarUsuarioSesion())
                return false;

            if (!ValidarDescripcionObligatoria())
                return false;

            if (!ValidarEspacioInicial())
                return false;

            if (!ValidarLongitudDescripcion())
                return false;

            return true;
        }

        private bool ValidarUsuarioSesion()
        {
            if (_usuarioActual != null && _usuarioActual.IdUsuario > 0)
                return true;

            MostrarAdvertencia("No se pudo identificar una sesión de usuario válida para realizar la operación.", "Sesión requerida");
            return false;
        }

        private bool ValidarDescripcionObligatoria()
        {
            if (!string.IsNullOrWhiteSpace(txtdescripcion.Text))
                return true;

            MostrarAdvertencia("El campo descripción es obligatorio.", "Campo obligatorio");
            txtdescripcion.Focus();
            return false;
        }

        private bool ValidarEspacioInicial()
        {
            if (!TieneEspacioInicial(txtdescripcion.Text))
                return true;

            MostrarAdvertencia("La descripción no puede comenzar con espacios en blanco.", "Espacios no permitidos");
            txtdescripcion.Focus();
            return false;
        }

        private static bool TieneEspacioInicial(string texto)
        {
            return !string.IsNullOrEmpty(texto) && char.IsWhiteSpace(texto[0]);
        }

        private bool ValidarLongitudDescripcion()
        {
            if (txtdescripcion.Text.Trim().Length <= LongitudMaximaDescripcion)
                return true;

            MostrarAdvertencia($"El campo descripción no puede superar los {LongitudMaximaDescripcion} caracteres.", "Validación de longitud");
            txtdescripcion.Focus();
            return false;
        }

        #endregion

        #region Eventos de DataGridView y Selección

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "btnSeleccionar")
                return;

            SeleccionarCategoria(e.RowIndex);
        }

        private void SeleccionarCategoria(int indice)
        {
            DataGridViewRow fila = dgvdata.Rows[indice];

            txtIndice.Text = indice.ToString();
            txtid.Text = ObtenerValorCelda(fila, "Id");
            txtdescripcion.Text = ObtenerValorCelda(fila, "Descripcion");

            SeleccionarEstado(ObtenerValorCelda(fila, "EstadoValor"));
        }

        private static string ObtenerValorCelda(DataGridViewRow fila, string nombreColumna)
        {
            return fila.Cells[nombreColumna].Value?.ToString() ?? string.Empty;
        }

        private void SeleccionarEstado(string valorEstado)
        {
            foreach (OpcionCombo opcion in cmbestado.Items)
            {
                if (opcion.Valor.ToString() != valorEstado)
                    continue;

                cmbestado.SelectedIndex = cmbestado.Items.IndexOf(opcion);
                return;
            }
        }

        #endregion

        #region Búsqueda y Filtros

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (!(cmbbusqueda.SelectedItem is OpcionCombo opcionBusqueda))
                return;

            string columnaFiltro = opcionBusqueda.Valor.ToString();
            string textoBusqueda = txtbusqueda.Text.Trim();

            bool encontrado = FiltrarCategorias(columnaFiltro, textoBusqueda);

            if (encontrado)
                return;

            MostrarInformacion("No se encontraron resultados para su búsqueda.", "Sin resultados");
            MostrarTodasLasFilas();
        }

        private bool FiltrarCategorias(string columnaFiltro, string textoBusqueda)
        {
            bool encontrado = false;

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string valorCelda = fila.Cells[columnaFiltro].Value?.ToString() ?? string.Empty;

                bool coincide = valorCelda.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0;

                fila.Visible = coincide;

                if (coincide)
                    encontrado = true;
            }

            return encontrado;
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
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

        #endregion

        #region Limpiar Formulario

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtIndice.Text = "-1";
            txtid.Text = "0";
            txtdescripcion.Clear();

            if (cmbestado.Items.Count > 0)
                cmbestado.SelectedIndex = 0;

            txtdescripcion.Focus();
        }

        #endregion

        #region Formato Visual de DataGridView

        private void dgvdata_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "Estado")
                return;

            string estado = e.Value?.ToString();

            FormatearEstado(e, estado);
        }

        private static void FormatearEstado(DataGridViewCellFormattingEventArgs e, string estado)
        {
            if (estado == "Activo")
            {
                AplicarFormatoEstado(e, Color.FromArgb(39, 174, 96), Color.FromArgb(46, 204, 113));
                return;
            }

            if (estado == "No Activo")
            {
                AplicarFormatoEstado(e, Color.FromArgb(192, 57, 43), Color.FromArgb(231, 76, 60));
            }
        }

        private static void AplicarFormatoEstado(DataGridViewCellFormattingEventArgs e, Color colorTexto, Color colorSeleccion)
        {
            e.CellStyle.ForeColor = colorTexto;
            e.CellStyle.SelectionBackColor = colorSeleccion;
            e.CellStyle.SelectionForeColor = Color.White;
        }

        #endregion

        #region Notificaciones y Diálogos

        private static void MostrarAdvertencia(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void MostrarInformacion(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void MostrarError(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}