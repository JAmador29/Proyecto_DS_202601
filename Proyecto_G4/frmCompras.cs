using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using Proyecto_G4.Modales;

namespace Proyecto_G4
{
    public partial class frmCompras : Form
    {
        private const string FormatoMoneda = "0.00";

        private readonly Usuario usuarioActual;
        private readonly CN_Producto productoNegocio;
        private readonly CN_Compra compraNegocio;

        public frmCompras(Usuario oUsuario = null)
        {
            InitializeComponent();

            usuarioActual = oUsuario;
            productoNegocio = new CN_Producto();
            compraNegocio = new CN_Compra();
        }

        // ================================================================
        // CARGA DEL FORMULARIO
        // ================================================================

        private void frmCompras_Load(object sender, EventArgs e)
        {
            ConfigurarComboTipoDocumento();
            ConfigurarFormulario();
        }

        private void ConfigurarComboTipoDocumento()
        {
            cbTipoDocumento.Items.Clear();

            cbTipoDocumento.Items.Add(new OpcionCombo
            {
                Valor = "Boleta",
                Texto = "Boleta"
            });

            cbTipoDocumento.Items.Add(new OpcionCombo
            {
                Valor = "Factura",
                Texto = "Factura"
            });

            cbTipoDocumento.DisplayMember = "Texto";
            cbTipoDocumento.ValueMember = "Valor";

            if (cbTipoDocumento.Items.Count > 0)
                cbTipoDocumento.SelectedIndex = 0;
        }

        private void ConfigurarFormulario()
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProveedor.Text = "0";
            txtIdProducto.Text = "0";
            txtTotalaPagar.Text = "0.00";

            txtCantidad.Minimum = 1;
            txtCantidad.Value = 1;
        }

        // ================================================================
        // SELECCIÓN DE PROVEEDOR
        // ================================================================

        private void btnBuscarProveedor_Click(
            object sender,
            EventArgs e)
        {
            using (mdProveedor modalProveedor = new mdProveedor())
            {
                DialogResult resultado = modalProveedor.ShowDialog();

                if (resultado != DialogResult.OK)
                {
                    txtDocProveedor.Focus();
                    return;
                }

                if (modalProveedor._Proveedor == null)
                {
                    MostrarAdvertencia(
                        "No se pudo obtener la información del proveedor.",
                        "Proveedor no válido");

                    return;
                }

                CargarProveedor(modalProveedor._Proveedor);
            }
        }

        private void CargarProveedor(Proveedor proveedor)
        {
            txtIdProveedor.Text = proveedor.IdProveedor.ToString();
            txtDocProveedor.Text = proveedor.RTN;
            txtNombreProveedor.Text = proveedor.RazonSocial;
        }

        // ================================================================
        // SELECCIÓN DE PRODUCTO
        // ================================================================

        private void btnBuscarProducto_Click(
            object sender,
            EventArgs e)
        {
            using (mdProducto modalProducto = new mdProducto())
            {
                DialogResult resultado = modalProducto.ShowDialog();

                if (resultado != DialogResult.OK)
                {
                    txtCodProducto.Focus();
                    return;
                }

                if (modalProducto._Producto == null)
                {
                    MostrarAdvertencia(
                        "No se pudo obtener la información del producto.",
                        "Producto no válido");

                    return;
                }

                CargarProducto(modalProducto._Producto);
            }
        }

        private void txtCodProducto_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            BuscarProductoPorCodigo();
        }

        private void BuscarProductoPorCodigo()
        {
            string codigoProducto = txtCodProducto.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigoProducto))
            {
                LimpiarProducto();
                txtCodProducto.Focus();
                return;
            }

            Producto producto = productoNegocio
                .Listar()
                .Find(item =>
                    string.Equals(
                        item.Codigo?.Trim(),
                        codigoProducto,
                        StringComparison.OrdinalIgnoreCase) &&
                    item.Estado);

            if (producto == null)
            {
                MostrarProductoNoEncontrado();
                return;
            }

            CargarProducto(producto);
        }

        private void CargarProducto(Producto producto)
        {
            txtCodProducto.BackColor = Color.Honeydew;
            txtIdProducto.Text = producto.IdProducto.ToString();
            txtCodProducto.Text = producto.Codigo;
            txtProducto.Text = producto.Nombre;

            txtPrecioCompra.Focus();
        }

        private void MostrarProductoNoEncontrado()
        {
            txtCodProducto.BackColor = Color.MistyRose;
            txtIdProducto.Text = "0";
            txtProducto.Clear();

            MostrarAdvertencia(
                "No se encontró un producto activo con el código ingresado.",
                "Producto no encontrado");

            txtCodProducto.Focus();
        }

        // ================================================================
        // AGREGAR PRODUCTO
        // ================================================================

        private void btnAgregarProducto_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidarProductoSeleccionado())
                return;

            if (!ObtenerPrecios(
                    out decimal precioCompra,
                    out decimal precioVenta))
            {
                return;
            }

            if (!ValidarPrecios(precioCompra, precioVenta))
                return;

            int idProducto = ObtenerIdProducto();

            if (ProductoExisteEnDetalle(idProducto))
            {
                MostrarAdvertencia(
                    "El producto seleccionado ya fue agregado a la compra.",
                    "Producto duplicado");

                txtCodProducto.Focus();
                return;
            }

            AgregarProductoAlDetalle(
                idProducto,
                precioCompra,
                precioVenta);

            CalcularTotal();
            LimpiarProducto();
            txtCodProducto.Focus();
        }

        private bool ValidarProductoSeleccionado()
        {
            if (ObtenerIdProducto() > 0)
                return true;

            MostrarAdvertencia(
                "Debe seleccionar un producto.",
                "Producto requerido");

            txtCodProducto.Focus();
            return false;
        }

        private int ObtenerIdProducto()
        {
            return int.TryParse(
                txtIdProducto.Text,
                out int idProducto)
                ? idProducto
                : 0;
        }

        private bool ObtenerPrecios(
            out decimal precioCompra,
            out decimal precioVenta)
        {
            precioCompra = 0;
            precioVenta = 0;

            if (!IntentarConvertirDecimal(
                    txtPrecioCompra.Text,
                    out precioCompra))
            {
                MostrarAdvertencia(
                    "El precio de compra tiene un formato incorrecto.",
                    "Precio inválido");

                txtPrecioCompra.Focus();
                return false;
            }

            if (!IntentarConvertirDecimal(
                    txtPrecioVenta.Text,
                    out precioVenta))
            {
                MostrarAdvertencia(
                    "El precio de venta tiene un formato incorrecto.",
                    "Precio inválido");

                txtPrecioVenta.Focus();
                return false;
            }

            return true;
        }

        private static bool IntentarConvertirDecimal(
            string texto,
            out decimal valor)
        {
            return decimal.TryParse(
                       texto,
                       NumberStyles.Number,
                       CultureInfo.CurrentCulture,
                       out valor)
                   ||
                   decimal.TryParse(
                       texto,
                       NumberStyles.Number,
                       CultureInfo.InvariantCulture,
                       out valor);
        }

        private bool ValidarPrecios(
            decimal precioCompra,
            decimal precioVenta)
        {
            if (precioCompra <= 0)
            {
                MostrarAdvertencia(
                    "El precio de compra debe ser mayor que cero.",
                    "Precio inválido");

                txtPrecioCompra.Focus();
                return false;
            }

            if (precioVenta <= 0)
            {
                MostrarAdvertencia(
                    "El precio de venta debe ser mayor que cero.",
                    "Precio inválido");

                txtPrecioVenta.Focus();
                return false;
            }

            if (precioVenta <= precioCompra)
            {
                MostrarAdvertencia(
                    "El precio de venta debe ser mayor que el precio de compra.",
                    "Precios inválidos");

                txtPrecioVenta.Focus();
                return false;
            }

            return true;
        }

        private bool ProductoExisteEnDetalle(int idProducto)
        {
            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                int idProductoFila = ConvertirEntero(
                    fila.Cells["IdProducto"].Value);

                if (idProductoFila == idProducto)
                    return true;
            }

            return false;
        }

        private void AgregarProductoAlDetalle(
            int idProducto,
            decimal precioCompra,
            decimal precioVenta)
        {
            int cantidad = Convert.ToInt32(txtCantidad.Value);
            decimal subtotal = cantidad * precioCompra;

            dgvdata.Rows.Add(
                idProducto,
                txtProducto.Text.Trim(),
                precioCompra.ToString(FormatoMoneda),
                precioVenta.ToString(FormatoMoneda),
                cantidad,
                subtotal.ToString(FormatoMoneda)
            );
        }

        // ================================================================
        // LIMPIAR PRODUCTO
        // ================================================================

        private void LimpiarProducto()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Clear();
            txtCodProducto.BackColor = Color.White;
            txtProducto.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtCantidad.Value = 1;
        }

        // ================================================================
        // CÁLCULO DEL TOTAL
        // ================================================================

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                if (IntentarConvertirDecimal(
                        fila.Cells["SubTotal"].Value?.ToString(),
                        out decimal subtotal))
                {
                    total += subtotal;
                }
            }

            txtTotalaPagar.Text = total.ToString(FormatoMoneda);
        }

        // ================================================================
        // ELIMINAR PRODUCTO
        // ================================================================

        private void dgvdata_CellPainting(
            object sender,
            DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "btnEliminar")
                return;

            e.Paint(
                e.CellBounds,
                DataGridViewPaintParts.All);

            Image imagenBasurero =
                Properties.Resources.Basurero25;

            int ancho = imagenBasurero.Width;
            int alto = imagenBasurero.Height;

            int posicionX =
                e.CellBounds.Left +
                (e.CellBounds.Width - ancho) / 2;

            int posicionY =
                e.CellBounds.Top +
                (e.CellBounds.Height - alto) / 2;

            e.Graphics.DrawImage(
                imagenBasurero,
                new Rectangle(
                    posicionX,
                    posicionY,
                    ancho,
                    alto));

            e.Handled = true;
        }

        private void dgvdata_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dgvdata.Columns[e.ColumnIndex].Name != "btnEliminar")
                return;

            dgvdata.Rows.RemoveAt(e.RowIndex);
            CalcularTotal();
        }

        // ================================================================
        // REGISTRAR COMPRA
        // ================================================================

        private void btnRegistrar_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidarCompra())
                return;

            DataTable detalleCompra = CrearDetalleCompra();

            string numeroDocumento =
                GenerarNumeroDocumento();

            Compras compra =
                CrearCompra(numeroDocumento);

            bool respuesta = compraNegocio.Registrar(
                compra,
                detalleCompra,
                out string mensaje);

            if (!respuesta)
            {
                MostrarAdvertencia(
                    mensaje,
                    "No se pudo registrar la compra");

                return;
            }

            MostrarCompraRegistrada(numeroDocumento);
            LimpiarCompra();
        }

        private bool ValidarCompra()
        {
            if (usuarioActual == null ||
                usuarioActual.IdUsuario <= 0)
            {
                MostrarError(
                    "No se pudo identificar al usuario que realiza la compra.",
                    "Usuario no válido");

                return false;
            }

            if (ObtenerIdProveedor() == 0)
            {
                MostrarAdvertencia(
                    "Debe seleccionar un proveedor.",
                    "Proveedor requerido");

                txtDocProveedor.Focus();
                return false;
            }

            if (!ExistenProductosEnDetalle())
            {
                MostrarAdvertencia(
                    "Debe ingresar al menos un producto en la compra.",
                    "Productos requeridos");

                txtCodProducto.Focus();
                return false;
            }

            if (!IntentarConvertirDecimal(
                    txtTotalaPagar.Text,
                    out decimal total) ||
                total <= 0)
            {
                MostrarAdvertencia(
                    "El monto total de la compra no es válido.",
                    "Total inválido");

                return false;
            }

            return true;
        }

        private int ObtenerIdProveedor()
        {
            return int.TryParse(
                txtIdProveedor.Text,
                out int idProveedor)
                ? idProveedor
                : 0;
        }

        private bool ExistenProductosEnDetalle()
        {
            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (!fila.IsNewRow)
                    return true;
            }

            return false;
        }

        private DataTable CrearDetalleCompra()
        {
            DataTable detalleCompra = new DataTable();

            detalleCompra.Columns.Add(
                "IdProducto",
                typeof(int));

            detalleCompra.Columns.Add(
                "PrecioCompra",
                typeof(decimal));

            detalleCompra.Columns.Add(
                "PrecioVenta",
                typeof(decimal));

            detalleCompra.Columns.Add(
                "Cantidad",
                typeof(int));

            detalleCompra.Columns.Add(
                "MontoTotal",
                typeof(decimal));

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                detalleCompra.Rows.Add(
                    ConvertirEntero(
                        fila.Cells["IdProducto"].Value),

                    ConvertirDecimal(
                        fila.Cells["PrecioCompra"].Value),

                    ConvertirDecimal(
                        fila.Cells["PrecioVenta"].Value),

                    ConvertirEntero(
                        fila.Cells["Cantidad"].Value),

                    ConvertirDecimal(
                        fila.Cells["SubTotal"].Value)
                );
            }

            return detalleCompra;
        }

        private string GenerarNumeroDocumento()
        {
            int correlativo =
                compraNegocio.ObtenerCorrelativo();

            return correlativo.ToString("00000");
        }

        private Compras CrearCompra(string numeroDocumento)
        {
            decimal montoTotal =
                ConvertirDecimal(txtTotalaPagar.Text);

            return new Compras
            {
                oUsuario = new Usuario
                {
                    IdUsuario = usuarioActual.IdUsuario
                },

                oProveedor = new Proveedor
                {
                    IdProveedor = ObtenerIdProveedor()
                },

                TipoDocumento = ObtenerTipoDocumento(),
                NumeroDocumento = numeroDocumento,
                MontoTotal = montoTotal
            };
        }

        private string ObtenerTipoDocumento()
        {
            if (cbTipoDocumento.SelectedItem is OpcionCombo opcion)
                return opcion.Texto?.ToString() ?? string.Empty;

            return string.Empty;
        }

        private void MostrarCompraRegistrada(
            string numeroDocumento)
        {
            DialogResult resultado = MessageBox.Show(
                "Número de compra generado:\n" +
                numeroDocumento +
                "\n\n¿Desea copiarlo al portapapeles?",
                "Compra registrada",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (resultado == DialogResult.Yes)
                Clipboard.SetText(numeroDocumento);
        }

        // ================================================================
        // LIMPIAR COMPRA
        // ================================================================

        private void LimpiarCompra()
        {
            txtIdProveedor.Text = "0";
            txtDocProveedor.Clear();
            txtNombreProveedor.Clear();

            dgvdata.Rows.Clear();

            LimpiarProducto();
            CalcularTotal();

            if (cbTipoDocumento.Items.Count > 0)
                cbTipoDocumento.SelectedIndex = 0;

            txtFecha.Text =
                DateTime.Now.ToString("dd/MM/yyyy");

            txtDocProveedor.Focus();
        }

        // ================================================================
        // VALIDACIÓN DE TECLADO
        // ================================================================

        private void txtPrecioCompra_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            ValidarEntradaDecimal(
                txtPrecioCompra,
                e);
        }

        private void txtPrecioVenta_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            ValidarEntradaDecimal(
                txtPrecioVenta,
                e);
        }

        private static void ValidarEntradaDecimal(
            TextBox campo,
            KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            char separadorDecimal =
                Convert.ToChar(
                    CultureInfo.CurrentCulture
                        .NumberFormat
                        .NumberDecimalSeparator);

            bool esSeparadorValido =
                e.KeyChar == '.' ||
                e.KeyChar == ',';

            if (!esSeparadorValido)
            {
                e.Handled = true;
                return;
            }

            if (campo.SelectionStart == 0 &&
                campo.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            bool yaTieneSeparador =
                campo.Text.Contains(".") ||
                campo.Text.Contains(",");

            if (yaTieneSeparador)
                e.Handled = true;
        }

        private void SoloNumeros_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            bool esNumero = char.IsDigit(e.KeyChar);
            bool esControl = char.IsControl(e.KeyChar);

            if (!esNumero && !esControl)
                e.Handled = true;
        }

        // ================================================================
        // CONVERSIONES
        // ================================================================

        private static int ConvertirEntero(object valor)
        {
            return int.TryParse(
                valor?.ToString(),
                out int resultado)
                ? resultado
                : 0;
        }

        private static decimal ConvertirDecimal(object valor)
        {
            return IntentarConvertirDecimal(
                valor?.ToString(),
                out decimal resultado)
                ? resultado
                : 0;
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