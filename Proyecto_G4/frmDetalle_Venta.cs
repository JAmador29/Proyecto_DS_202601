using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Proyecto_G4
{
    public partial class frmDetalle_Venta : Form
    {
        private const string FormatoMoneda = "0.00";

        private readonly CN_Venta ventaNegocio;
        private readonly CN_Negocio negocioNegocio;

        public frmDetalle_Venta()
        {
            InitializeComponent();

            ventaNegocio = new CN_Venta();
            negocioNegocio = new CN_Negocio();
        }

        // ================================================================
        // CARGA DEL FORMULARIO
        // ================================================================

        private void frmDetalle_Venta_Load(object sender, EventArgs e)
        {
            LimpiarDetalle();
            txtbusqueda.Focus();
        }

        // ================================================================
        // BÚSQUEDA DE VENTA
        // ================================================================

        private void btnbuscar_Click_1(object sender, EventArgs e)
        {
            BuscarVenta();
        }

        private void BuscarVenta()
        {
            string numeroDocumento = txtbusqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(numeroDocumento))
            {
                MostrarAdvertencia(
                    "Ingrese el número de documento de la venta.",
                    "Número requerido");

                txtbusqueda.Focus();
                return;
            }

            Venta venta = ventaNegocio.ObtenerVenta(numeroDocumento);

            if (!VentaValida(venta))
            {
                MostrarInformacion(
                    "No se encontró ninguna venta con ese número de documento.",
                    "Sin resultados");

                LimpiarDetalle();
                txtbusqueda.Focus();
                return;
            }

            MostrarVenta(venta);
        }

        private static bool VentaValida(Venta venta)
        {
            return venta != null &&
                   venta.IdVenta > 0;
        }

        private void MostrarVenta(Venta venta)
        {
            txtnumerodocumento.Text =
                venta.NumeroDocumento ?? string.Empty;

            txtfecha.Text =
                venta.FechaRegistro ?? string.Empty;

            txttipodocumento.Text =
                venta.TipoDocumento ?? string.Empty;

            txtusuario.Text =
                venta.oUsuario?.NombreCompleto ?? string.Empty;

            txtdoccliente.Text =
                venta.DocumentoCliente ?? string.Empty;

            txtnombrecliente.Text =
                venta.NombreCliente ?? string.Empty;

            CargarDetalleVenta(venta);

            txtmontototal.Text =
                venta.MontoTotal.ToString(FormatoMoneda);

            txtmontopago.Text =
                venta.MontoPago.ToString(FormatoMoneda);

            txtmontocambio.Text =
                venta.MontoCambio.ToString(FormatoMoneda);
        }

        private void CargarDetalleVenta(Venta venta)
        {
            dgvdata.Rows.Clear();

            if (venta.DetalleVenta == null ||
                venta.DetalleVenta.Count == 0)
            {
                MostrarAdvertencia(
                    "La venta fue encontrada, pero no tiene productos guardados en DETALLE_VENTA.",
                    "Venta sin detalle");

                return;
            }

            foreach (Detalle_Venta detalle in venta.DetalleVenta)
            {
                AgregarDetalleAlGrid(detalle);
            }
        }

        private void AgregarDetalleAlGrid(Detalle_Venta detalle)
        {
            dgvdata.Rows.Add(
                detalle.oProducto?.Nombre ?? string.Empty,
                detalle.PrecioVenta.ToString(FormatoMoneda),
                detalle.Cantidad,
                detalle.SubTotal.ToString(FormatoMoneda)
            );
        }

        // ================================================================
        // LIMPIEZA
        // ================================================================

        private void btnborrar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtbusqueda.Clear();
            LimpiarDetalle();
            txtbusqueda.Focus();
        }

        private void LimpiarDetalle()
        {
            txtnumerodocumento.Clear();
            txtfecha.Clear();
            txttipodocumento.Clear();
            txtusuario.Clear();
            txtdoccliente.Clear();
            txtnombrecliente.Clear();

            dgvdata.Rows.Clear();

            txtmontototal.Text = FormatoMoneda;
            txtmontopago.Text = FormatoMoneda;
            txtmontocambio.Text = FormatoMoneda;
        }

        // ================================================================
        // GENERACIÓN DEL PDF
        // ================================================================

        private void btndescargar_Click_1(object sender, EventArgs e)
        {
            if (!HayVentaCargada())
            {
                MostrarAdvertencia(
                    "Primero debe buscar una venta válida.",
                    "Venta requerida");

                txtbusqueda.Focus();
                return;
            }

            try
            {
                GenerarDocumentoPdf();
            }
            catch (Exception ex)
            {
                MostrarError(
                    "No se pudo generar el documento PDF.\n\n" +
                    ex.Message,
                    "Error al generar PDF");
            }
        }

        private bool HayVentaCargada()
        {
            return !string.IsNullOrWhiteSpace(txttipodocumento.Text) &&
                   !string.IsNullOrWhiteSpace(txtnumerodocumento.Text);
        }

        private void GenerarDocumentoPdf()
        {
            string contenidoHtml = CrearContenidoHtml();

            using (SaveFileDialog dialogoGuardar = CrearDialogoGuardar())
            {
                if (dialogoGuardar.ShowDialog() != DialogResult.OK)
                    return;

                CrearArchivoPdf(
                    dialogoGuardar.FileName,
                    contenidoHtml);
            }

            MostrarInformacion(
                "El documento PDF se generó correctamente.",
                "Documento generado");
        }

        private SaveFileDialog CrearDialogoGuardar()
        {
            string numeroDocumento =
                LimpiarNombreArchivo(txtnumerodocumento.Text);

            return new SaveFileDialog
            {
                FileName = $"Venta_{numeroDocumento}.pdf",
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                OverwritePrompt = true,
                Title = "Guardar detalle de venta"
            };
        }

        private string CrearContenidoHtml()
        {
            string contenidoHtml =
                Properties.Resources.PlantillaVenta.ToString();

            Negocio datosNegocio =
                negocioNegocio.ObtenerDatos();

            contenidoHtml = ReemplazarDatosNegocio(
                contenidoHtml,
                datosNegocio);

            contenidoHtml = ReemplazarDatosVenta(
                contenidoHtml);

            contenidoHtml = contenidoHtml.Replace(
                "@filas",
                CrearFilasHtml());

            contenidoHtml = contenidoHtml.Replace(
                "@montototal",
                CodificarHtml(txtmontototal.Text));

            contenidoHtml = contenidoHtml.Replace(
                "@pagocon",
                CodificarHtml(txtmontopago.Text));

            contenidoHtml = contenidoHtml.Replace(
                "@cambio",
                CodificarHtml(txtmontocambio.Text));

            return contenidoHtml;
        }

        private static string ReemplazarDatosNegocio(
            string contenidoHtml,
            Negocio negocio)
        {
            string nombre =
                negocio?.Nombre ?? string.Empty;

            string rtn =
                negocio?.RTN ?? string.Empty;

            string direccion =
                negocio?.Direccion ?? string.Empty;

            return contenidoHtml
                .Replace(
                    "@nombrenegocio",
                    CodificarHtml(nombre.ToUpperInvariant()))
                .Replace(
                    "@docnegocio",
                    CodificarHtml(rtn))
                .Replace(
                    "@direcnegocio",
                    CodificarHtml(direccion));
        }

        private string ReemplazarDatosVenta(string contenidoHtml)
        {
            return contenidoHtml
                .Replace(
                    "@tipodocumento",
                    CodificarHtml(
                        txttipodocumento.Text.ToUpperInvariant()))
                .Replace(
                    "@numerodocumento",
                    CodificarHtml(txtnumerodocumento.Text))
                .Replace(
                    "@docproveedor",
                    CodificarHtml(txtdoccliente.Text))
                .Replace(
                    "@nombreproveedor",
                    CodificarHtml(txtnombrecliente.Text))
                .Replace(
                    "@fecharegistro",
                    CodificarHtml(txtfecha.Text))
                .Replace(
                    "@usuarioregistro",
                    CodificarHtml(txtusuario.Text));
        }

        private string CrearFilasHtml()
        {
            StringBuilder filasHtml = new StringBuilder();

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                filasHtml.AppendLine("<tr>");

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "Producto"));

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "Precio"));

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "Cantidad"));

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "SubTotal"));

                filasHtml.AppendLine("</tr>");
            }

            return filasHtml.ToString();
        }

        private static string ObtenerValorHtml(
            DataGridViewRow fila,
            string nombreColumna)
        {
            string valor =
                fila.Cells[nombreColumna].Value?.ToString()
                ?? string.Empty;

            return CodificarHtml(valor);
        }

        private void CrearArchivoPdf(
            string rutaArchivo,
            string contenidoHtml)
        {
            using (FileStream stream = new FileStream(
                       rutaArchivo,
                       FileMode.Create,
                       FileAccess.Write,
                       FileShare.None))
            {
                using (Document documento = new Document(
                           PageSize.A4,
                           25,
                           25,
                           25,
                           25))
                {
                    PdfWriter escritor =
                        PdfWriter.GetInstance(
                            documento,
                            stream);

                    documento.Open();

                    AgregarLogo(documento);

                    using (StringReader lectorHtml =
                           new StringReader(contenidoHtml))
                    {
                        XMLWorkerHelper
                            .GetInstance()
                            .ParseXHtml(
                                escritor,
                                documento,
                                lectorHtml);
                    }
                }
            }
        }

        private void AgregarLogo(Document documento)
        {
            byte[] logo =
                negocioNegocio.ObtenerLogo(
                    out bool obtenido);

            if (!obtenido ||
                logo == null ||
                logo.Length == 0)
            {
                return;
            }

            iTextSharp.text.Image imagen =
                iTextSharp.text.Image.GetInstance(logo);

            imagen.ScaleToFit(60, 60);
            imagen.Alignment =
                iTextSharp.text.Image.UNDERLYING;

            imagen.SetAbsolutePosition(
                documento.Left,
                documento.GetTop(51));

            documento.Add(imagen);
        }

        private static string CodificarHtml(string valor)
        {
            return WebUtility.HtmlEncode(
                valor ?? string.Empty);
        }

        private static string LimpiarNombreArchivo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "SinNumero";

            foreach (char caracterInvalido
                     in Path.GetInvalidFileNameChars())
            {
                nombre = nombre.Replace(
                    caracterInvalido,
                    '_');
            }

            return nombre.Trim();
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